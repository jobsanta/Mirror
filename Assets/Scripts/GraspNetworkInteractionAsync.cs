using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using UnityEngine.Networking;

[RequireComponent(typeof(InteractionBehaviour))]
public class GraspNetworkInteractionAsync : NetworkBehaviour {

    private InteractionBehaviour _intObj;
    private Rigidbody rb;
    private BoxCollider bc;
    private GlowObject _glowObj;
    private bool isHovering;
    Quaternion lockrotation;
    void Start() {
        _intObj = GetComponent<InteractionBehaviour>();
        _glowObj = GetComponent<GlowObject>();
        _intObj.OnGraspedMovement += onGraspedMovement;
        _intObj.OnGraspEnd += onGraspedEnd;
        _intObj.OnGraspBegin += OnGraspedStart;

        _intObj.OnPrimaryHoverBegin += OnHoverStart;
        _intObj.OnPrimaryHoverEnd += OnHoverEnd;

        bc = GetComponent<BoxCollider>();

        rb = GetComponent<Rigidbody>();

    }

    private void OnHoverStart()
    {
        _glowObj.OnHoverStart();
        //StartCoroutine(RotateObjects());
    }

    private void OnHoverEnd()
    {
        _glowObj.OnHoverEnd();
    }


    IEnumerator RotateObjects()
    {
        while(isHovering)
        {
            yield return new WaitForSeconds(1.0f);
            gameObject.transform.Rotate(new Vector3(0, 90, 0));
        }
    }

    private void onGraspedEnd()
    {
        _glowObj.OnGraspEnd();
        gameObject.GetComponent<AnchorNetworkInteractionAsync>().OnGraspEndCheckAnchor(gameObject);
    }

    private void OnGraspedStart()
    {
        lockrotation = transform.rotation;
        _glowObj.OnGraspBegin();
        gameObject.GetComponent<AnchorNetworkInteractionAsync>().OnGraspBeginCheck(gameObject);
    }


    private void onGraspedMovement(Vector3 presolvedPos, Quaternion presolvedRot,
        Vector3 solvedPos, Quaternion solvedRot,
        List<InteractionController> graspingController)
    {

        Quaternion relative = solvedRot * Quaternion.Inverse(lockrotation);

        float angles = relative.eulerAngles.y;
        if (angles > 180 && angles < 360)
            angles = Mathf.Max(-180, -(360 - angles) * 3.0f);
        if (angles > 0 && angles < 180)
            angles = Mathf.Min(180, angles * 3.0f);

        Debug.Log(lockrotation.eulerAngles.y + " " +angles);

        Vector3 movementDueToGrasp = solvedPos - presolvedPos;
        float xAxisMovement = movementDueToGrasp.x;
        float zAxisMovement = movementDueToGrasp.z;

        _intObj.rigidbody.position = presolvedPos;
        _intObj.rigidbody.position += Vector3.right * xAxisMovement + Vector3.forward * zAxisMovement;
        _intObj.rigidbody.rotation = Quaternion.Euler(0, lockrotation.eulerAngles.y + angles, 0);

        // CmdGraspedMovement(_intObj.rigidbody.position, solvedRot);



    }
    [Command]
    void CmdGraspedMovement(Vector3 solvedPos, Quaternion solvedRot)
    {
        _intObj.transform.position = solvedPos;
        _intObj.transform.rotation = solvedRot;
    }

}


