﻿using System.Collections;
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
        Vector3 angle = solvedRot.eulerAngles;
        
        solvedRot = Quaternion.Euler(0, angle.y, 0);


        Vector3 movementDueToGrasp = solvedPos - presolvedPos;
        float xAxisMovement = movementDueToGrasp.x;
        float zAxisMovement = movementDueToGrasp.z;

        _intObj.rigidbody.position = presolvedPos;
        _intObj.rigidbody.position += Vector3.right * xAxisMovement + Vector3.forward * zAxisMovement;
        _intObj.rigidbody.rotation = solvedRot;

        CmdGraspedMovement(_intObj.rigidbody.position, solvedRot);




    }


    [Command]
    void CmdGraspedMovement(Vector3 solvedPos, Quaternion solvedRot)
    {
        _intObj.transform.position = solvedPos;
        _intObj.transform.rotation = solvedRot;
    }
}
