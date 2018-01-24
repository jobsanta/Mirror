using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

	static Matrix4x4 MatrixLookAtLH(Vector3 eye, Vector3 at, Vector3 up)
	{
		Matrix4x4 matrix = new Matrix4x4();

		// Get axes
		Vector3 zAxis = (at - eye).normalized;
		Vector3 xAxis = Vector3.Cross(up, zAxis).normalized;
		Vector3 yAxis = Vector3.Cross(zAxis, xAxis);

		// Set up the matrix
		matrix.m00 = xAxis.x;   matrix.m01 = yAxis.x;   matrix.m02 = zAxis.x;   matrix.m03 = 0;
		matrix.m10 = xAxis.y;   matrix.m11 = yAxis.y;   matrix.m12 = zAxis.y;   matrix.m13 = 0;
		matrix.m20 = xAxis.z;   matrix.m21 = yAxis.z;   matrix.m22 = zAxis.z;   matrix.m23 = 0;
		matrix.m30 = -Vector3.Dot(xAxis, eye);  matrix.m31 = -Vector3.Dot(yAxis, eye);  matrix.m32 = -Vector3.Dot(zAxis, eye);  matrix.m33 = 1;

		// Return the calculated matrix
		return matrix;
	}


	static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
	{
		float x = 2.0F * near / (right - left);
		float y = 2.0F * near / (top - bottom);
		float a = (right + left) / (right - left);
		float b = (top + bottom) / (top - bottom);
		float c = -(far + near) / (far - near);
		float d = -(2.0F * far * near) / (far - near);
		float e = -1.0F;
		Matrix4x4 m = new Matrix4x4();
		m[0, 0] = x;
		m[0, 1] = 0;
		m[0, 2] = a;
		m[0, 3] = 0;
		m[1, 0] = 0;
		m[1, 1] = y;
		m[1, 2] = b;
		m[1, 3] = 0;
		m[2, 0] = 0;
		m[2, 1] = 0;
		m[2, 2] = c;
		m[2, 3] = d;
		m[3, 0] = 0;
		m[3, 1] = 0;
		m[3, 2] = e;
		m[3, 3] = 0;
		return m;
	}

	public static Matrix4x4 GeneralizedPerspectiveProjection(Vector3 pa, Vector3 pb, Vector3 pc, Vector3 pe, float near, float far)
	{
		Vector3 va, vb, vc;
		Vector3 vr, vu, vn;

		float left, right, bottom, top, eyedistance;

		Matrix4x4 projectionM;
		Matrix4x4 finalProjection;

		///Calculate the orthonormal for the screen (the screen coordinate system
		vr = pb - pa;
		vr.Normalize();
		vu = pc - pa;
		vu.Normalize();
		vn = Vector3.Cross(vr, vu);
		vn.Normalize();

		//Calculate the vector from eye (pe) to screen corners (pa, pb, pc)
		va = pa-pe;
		vb = pb-pe;
		vc = pc-pe;

		//Get the distance;; from the eye to the screen plane
		eyedistance = (Vector3.Dot(va, vn));
		//Get the varaibles for the off center projection
		left = (Vector3.Dot(vr, va)*near)/eyedistance;
		right  = (Vector3.Dot(vr, vb)*near)/eyedistance;
		bottom  = (Vector3.Dot(vu, va)*near)/eyedistance;
		top = (Vector3.Dot(vu, vc)*near)/eyedistance;

		//Get this projection
		projectionM = PerspectiveOffCenter(left, right, bottom, top, near, far);
		//Fill in the transform matrix

		//Multiply all together
		finalProjection = new Matrix4x4();
		finalProjection = projectionM;
		//finally return
		return finalProjection;
	}

	// Update is called once per frame
	public void LateUpdate () {
		
		//calculate projection

		Vector3 trackerPosition = cam.transform.position;
        Vector3 BottomLeftCorner = new Vector3(-0.30f,0.0f,0.0f);
        Vector3 BottomRightCorner = new Vector3(0.30f,0.0f,0.0f);
        Vector3 TopLeftCorner = new Vector3 (-0.30f, 0.33f, 0.0f);
        if (trackerPosition.z > 0)
        {
             BottomLeftCorner = new Vector3(0.30f,0.0f,0.0f);
             BottomRightCorner = new Vector3(-0.30f,0.0f,0.0f);
             TopLeftCorner = new Vector3 (0.30f, 0.33f, 0.0f);
        }



		//TopLeftCorner = TopLeftCorner - trackerPosition;

		Matrix4x4 genProjection = GeneralizedPerspectiveProjection(
			BottomLeftCorner, BottomRightCorner,
			TopLeftCorner, trackerPosition,
			cam.nearClipPlane, cam.farClipPlane);
		
		cam.projectionMatrix = genProjection; 

		//Debug.Log (genProjection);


		Matrix4x4 eyeTranslateM;
		eyeTranslateM = new Matrix4x4();
		eyeTranslateM[0, 0] = 1;
		eyeTranslateM[0, 1] = 0;
		eyeTranslateM[0, 2] = 0;
		eyeTranslateM[0, 3] = 0;

		eyeTranslateM[1, 0] = 0;
		eyeTranslateM[1, 1] = 1;
		eyeTranslateM[1, 2] = 0;
		eyeTranslateM[1, 3] = 0;

		eyeTranslateM[2, 0] = 0;
		eyeTranslateM[2, 1] = 0;
		eyeTranslateM[2, 2] = -1;
		eyeTranslateM[2, 3] = 0;

		eyeTranslateM[3, 0] = -trackerPosition.x;
		eyeTranslateM[3, 1] = -trackerPosition.y;
		eyeTranslateM[3, 2] = trackerPosition.z;
		eyeTranslateM[3, 3] = 1;

        if (trackerPosition.z > 0)
        {
            eyeTranslateM[0, 0] = -1;
            eyeTranslateM[0, 1] = 0;
            eyeTranslateM[0, 2] = 0;
            eyeTranslateM[0, 3] = 0;

            eyeTranslateM[1, 0] = 0;
            eyeTranslateM[1, 1] = 1;
            eyeTranslateM[1, 2] = 0;
            eyeTranslateM[1, 3] = 0;

            eyeTranslateM[2, 0] = 0;
            eyeTranslateM[2, 1] = 0;
            eyeTranslateM[2, 2] = 1;
            eyeTranslateM[2, 3] = 0;

            eyeTranslateM[3, 0] = trackerPosition.x;
            eyeTranslateM[3, 1] = -trackerPosition.y;
            eyeTranslateM[3, 2] = -trackerPosition.z;
            eyeTranslateM[3, 3] = 1;

            eyeTranslateM[2, 0] = 0;
            eyeTranslateM[2, 1] = 0;
            eyeTranslateM[2, 2] = 1;
            eyeTranslateM[2, 3] = 0;
        }


		cam.worldToCameraMatrix = eyeTranslateM.transpose;
       // cam.stereoConvergence = Mathf.Sqrt( trackerPosition.x*trackerPosition.x + ((trackerPosition.y-0.17f)*(trackerPosition.y-0.17f))+ trackerPosition.z*trackerPosition.z);
	//	Debug.Log (cam.worldToCameraMatrix);

	}

}
