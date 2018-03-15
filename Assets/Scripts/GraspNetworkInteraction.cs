using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using UnityEngine.Networking;

[RequireComponent(typeof(InteractionBehaviour))]
public class GraspNetworkInteraction : NetworkBehaviour {

    private InteractionBehaviour _intObj;
    private Rigidbody rb;
    private BoxCollider bc;
    private GlowObject _glowObj;
    Quaternion lockrotation;

    void Start() {
        _intObj = GetComponent<InteractionBehaviour>();
        _glowObj = GetComponent<GlowObject>();
        _intObj.OnGraspedMovement += OnGraspedMovement;
        _intObj.OnPrimaryHoverBegin += OnHoverStart;
        _intObj.OnPrimaryHoverEnd += OnHoverEnd;
        _intObj.OnGraspBegin += OnGraspStart;
        _intObj.OnGraspEnd += OnGraspEnd;

        bc = GetComponent < BoxCollider>();

        rb = GetComponent<Rigidbody>();

    }

    private void OnHoverStart()
    {
        _glowObj.OnHoverStart();
    }

    private void OnHoverEnd()
    {
        _glowObj.OnHoverEnd();
    }

    private void OnGraspStart()
    {
        lockrotation = transform.rotation;
        _glowObj.OnGraspBegin();
    }

    private void OnGraspEnd()
    {
        _glowObj.OnGraspEnd();
    }


    private void OnGraspedMovement(Vector3 presolvedPos, Quaternion presolvedRot,
        Vector3 solvedPos,    Quaternion solvedRot,
        List<InteractionController> graspingController) 
    {
        Quaternion relative = solvedRot * Quaternion.Inverse(lockrotation);

        float angles = relative.eulerAngles.y;
        if (angles > 180 && angles < 360)
            angles = Mathf.Max(-180, -(360 - angles) * 3.0f);
        if (angles > 0 && angles < 180)
            angles = Mathf.Min(180, angles * 3.0f);

        Quaternion rot = Quaternion.Euler(0, lockrotation.eulerAngles.y + angles, 0);


        Vector3 movementDueToGrasp = solvedPos - presolvedPos;
        float xAxisMovement = movementDueToGrasp.x;
        float zAxisMovement = movementDueToGrasp.z;

        _intObj.rigidbody.position = presolvedPos;
        _intObj.rigidbody.position += Vector3.right * xAxisMovement + Vector3.forward * zAxisMovement;
        _intObj.rigidbody.rotation = rot;

        CmdGraspedMovement(_intObj.rigidbody.position, rot);




    }


    [Command]
    void CmdGraspedMovement(Vector3 solvedPos, Quaternion solvedRot)
    {
        _intObj.transform.position = solvedPos;
        _intObj.transform.rotation = solvedRot;
    }
}
