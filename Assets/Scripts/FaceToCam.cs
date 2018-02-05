using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using Microsoft.Kinect.Face;
using System.IO;

public class FaceToCam : MonoBehaviour
{
	private KinectSensor kinectSensor;
	private int bodyCount;
	private Body[] bodies;
	private FaceFrameSource[] faceFrameSources;
	private FaceFrameReader[] faceFrameReaders;
	private CoordinateMapper _Mapper;
	public GameObject bodyManager;
	public GameObject MultiSourceManager;

	//this is our game camera
	public GameObject camera;

    private static bool isClientPos = false;

    private Transform tf;

//	StreamWriter sx;
//	StreamWriter sy;
//	StreamWriter kx;

	//my value 0.005
	public float qq;

	//my value 0.01
	public float rr;

	//my value 0.0001
	public float init_state;

	//bigger values faster rotations and more noise. my value 10
	public float headSmooth;

	private float last_x = 0;

	private float last_y = 0;

	private float last_z = 0;

	private float last_mod = 0;
	private MultiSourceManager _MultiManager;

	private int updateFrame;

	KalmanFilterSimple1D kalman_X;
	KalmanFilterSimple1D kalman_Y;
	KalmanFilterSimple1D kalman_Z;
	KalmanFilterSimple1D kalman_mod;

    float cos_deg;
    float sin_deg;

    public static void setClientPos()
    {
        isClientPos = true;
    }


	void Start()
	{
        tf = GetComponent<Transform>();

        cos_deg = Mathf.Cos(tf.eulerAngles.x*Mathf.PI/180.0f);
        sin_deg = Mathf.Sin(tf.eulerAngles.x*Mathf.PI/180.0f);

        Debug.Log(tf.eulerAngles.x);
		updateFrame = 0;
		kalman_X = new KalmanFilterSimple1D(f: 1, h: 1, q: qq, r: rr);
		kalman_Y = new KalmanFilterSimple1D(f: 1, h: 1, q: qq, r: rr);
		kalman_Z= new KalmanFilterSimple1D(f: 1, h: 1, q: qq, r: rr);
		kalman_mod = new KalmanFilterSimple1D(f: 1, h: 1, q: qq, r: rr);

		//sx = new StreamWriter("coords_X.txt");
		//kx = new StreamWriter("coords_KX.txt");


		// one sensor is currently supported
		kinectSensor = KinectSensor.GetDefault();

		if (kinectSensor != null) 
		{
			_Mapper = kinectSensor.CoordinateMapper;

		}


		// set the maximum number of bodies that would be tracked by Kinect
		bodyCount = kinectSensor.BodyFrameSource.BodyCount;

		// allocate storage to store body objects
		bodies = new Body[bodyCount];

		// specify the required face frame results
		FaceFrameFeatures faceFrameFeatures =
			FaceFrameFeatures.BoundingBoxInColorSpace
			| FaceFrameFeatures.PointsInColorSpace
			| FaceFrameFeatures.BoundingBoxInInfraredSpace
			| FaceFrameFeatures.PointsInInfraredSpace
			| FaceFrameFeatures.RotationOrientation
			| FaceFrameFeatures.FaceEngagement
			| FaceFrameFeatures.Glasses
			| FaceFrameFeatures.Happy
			| FaceFrameFeatures.LeftEyeClosed
			| FaceFrameFeatures.RightEyeClosed
			| FaceFrameFeatures.LookingAway
			| FaceFrameFeatures.MouthMoved
			| FaceFrameFeatures.MouthOpen;

		// create a face frame source + reader to track each face in the FOV
		faceFrameSources = new FaceFrameSource[bodyCount];
		faceFrameReaders = new FaceFrameReader[bodyCount];
		for (int i = 0; i < bodyCount; i++)
		{
			// create the face frame source with the required face frame features and an initial tracking Id of 0
			faceFrameSources[i] = FaceFrameSource.Create(kinectSensor, 0, faceFrameFeatures);

			// open the corresponding reader
			faceFrameReaders[i] = faceFrameSources[i].OpenReader();
		}
	}

    public 

	void LateUpdate()
	{


		if(updateFrame < 1)
		{
			updateFrame++;
			return;
		}
		updateFrame = 0;
		// get bodies either from BodySourceManager object get them from a BodyReader
		var bodySourceManager = bodyManager.GetComponent<BodySourceManager>();
		bodies = bodySourceManager.GetData();
		if (bodies == null)
		{
			return;
		}


		if (MultiSourceManager == null) 
		{
			return;
		}
		_MultiManager = MultiSourceManager.GetComponent<MultiSourceManager> ();
		if (_MultiManager == null) 
		{
			return;
		}

		ushort[] depthdata = _MultiManager.GetDepthData();
		CameraSpacePoint[] camSpace = new CameraSpacePoint[1920*1080];
		_Mapper.MapColorFrameToCameraSpace(depthdata, camSpace);

		// iterate through each body and update face source
		for (int i = 0; i < bodyCount; i++)
		{
			// check if a valid face is tracked in this face source				
			if (faceFrameSources[i].IsTrackingIdValid)
			{
				using (FaceFrame frame = faceFrameReaders[i].AcquireLatestFrame())
				{
					if (frame != null)
					{
						if (frame.TrackingId == 0)
						{
							continue;
						}

						// do something with result
						//var result = frame.FaceFrameResult.FaceRotationQuaternion;
						var faceResult = frame.FaceFrameResult;
						var eyeLeft = faceResult.FacePointsInColorSpace [FacePointType.EyeLeft];
						var eyeRight = faceResult.FacePointsInColorSpace [FacePointType.EyeRight];

						var result = new Vector2 ((eyeLeft.X + eyeRight.X) / 2.0f, (eyeLeft.Y + eyeRight.Y) / 2.0f);

						CameraSpacePoint eye = camSpace[(int)(result.x+0.5f) + 1920 * (int)(result.y+0.5f)];

						if (!float.IsNegativeInfinity(eye.X) && !float.IsInfinity(eye.X) && !float.IsNaN(eye.X)&&
							!float.IsNegativeInfinity(eye.Y) && !float.IsInfinity(eye.Y) && !float.IsNaN(eye.Y)&&
							!float.IsNegativeInfinity(eye.Z) && !float.IsInfinity(eye.Z) && !float.IsNaN(eye.Z))
						{



							if (last_x == 0)
							{
								//last_x = face.transform.rotation.x;
								last_x = camera.transform.position.x;
							}
							if (last_y == 0)
							{
								//last_y = face.transform.rotation.y;
                                last_y = camera.transform.position.y;
							}
							if (last_z== 0)
							{
								//last_y = face.transform.rotation.y;
                                last_z = camera.transform.position.z;
							}

//							sx.WriteLine((-eye.X).ToString("0.000000"), true);
//							sx.Flush();

							kalman_Z.SetState(last_z, init_state);
							kalman_Z.Correct(eye.Z);

							kalman_X.SetState(last_x, init_state);
							kalman_X.Correct(eye.X);

							kalman_Y.SetState(last_y, init_state);
							kalman_Y.Correct(eye.Y);
							float mod_x = last_x % -eye.Y;

							kalman_mod.SetState(last_mod, init_state);
							kalman_mod.Correct(mod_x);


							//small shakes
							if (mod_x > 0.01f && mod_x < 0.08f)
							{

								mod_x = mod_x / 2;
							}

                            float x = (float)kalman_X.State;
                            float y = cos_deg * (float)kalman_Y.State - sin_deg * (float)kalman_Z.State + tf.position.y;
                            float z = sin_deg * (float)kalman_Y.State + cos_deg * (float)kalman_Z.State - tf.position.z;

                            //Debug.Log("Y " + kalman_Y.State + " Z " + kalman_Z.State);

                            if (!isClientPos)
                            {
                                camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(x, y, -z), Time.deltaTime * headSmooth * mod_x + 0.3f);
                            }
                            else
                            {
                                camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(-x, y, z), Time.deltaTime * headSmooth * mod_x + 0.3f);
                            }
                              

							//face.transform.localRotation = Quaternion.Lerp(face.transform.localRotation, new Quaternion((float)kalman_X.State, (float)kalman_Y.State, face.transform.localRotation.z, face.transform.localRotation.w), Time.deltaTime * headSmooth * mod_x + 0.3f);

                           // camera.transform.position =  new Vector3(x,y,-z);
//							kx.WriteLine(kalman_X.State.ToString("0.000000"), true);
//							kx.Flush();
							last_y = (float)kalman_Y.State;
							last_x = (float)kalman_X.State;
							last_z = (float)kalman_Z.State;
							last_mod = (float)kalman_mod.State;
						}

						//updateFrame = !updateFrame;
					}
				}
			}
			else
			{
				// check if the corresponding body is tracked 
				if (bodies[i].IsTracked)
				{
					// update the face frame source to track this body
					faceFrameSources[i].TrackingId = bodies[i].TrackingId;
				}
			}
		}

	}
}