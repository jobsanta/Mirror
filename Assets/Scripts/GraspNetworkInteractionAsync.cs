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


    void Start() {
        _intObj = GetComponent<InteractionBehaviour>();
        _intObj.OnGraspedMovement += onGraspedMovement;

        bc = GetComponent < BoxCollider>();

        rb = GetComponent<Rigidbody>();

    }



    private void onGraspedMovement(Vector3 presolvedPos, Quaternion presolvedRot,
        Vector3 solvedPos,    Quaternion solvedRot,
        List<InteractionController> graspingController) 
    {


        if (solvedPos.y < 0.1f)
        {
            Vector3 movementDueToGrasp = solvedPos - presolvedPos;
            float xAxisMovement = movementDueToGrasp.x;
            float zAxisMovement = movementDueToGrasp.z;

            _intObj.rigidbody.position = presolvedPos;
            _intObj.rigidbody.position += Vector3.right * xAxisMovement + Vector3.forward * zAxisMovement;

        }


    }
}
