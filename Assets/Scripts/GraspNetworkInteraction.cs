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
        Vector3 angle = solvedRot.eulerAngles;
        
        solvedRot = Quaternion.Euler(0, angle.y, 0);


        if (solvedPos.y < 0.1f)
        {
            Vector3 movementDueToGrasp = solvedPos - presolvedPos;
            float xAxisMovement = movementDueToGrasp.x;
            float zAxisMovement = movementDueToGrasp.z;

            _intObj.rigidbody.position = presolvedPos;
            _intObj.rigidbody.position += Vector3.right * xAxisMovement + Vector3.forward * zAxisMovement;
            CmdGraspedMovement(_intObj.rigidbody.position, solvedRot);

        }
        else
        {
            CmdGraspedMovement(solvedPos, solvedRot);
        }


    }


    [Command]
    void CmdGraspedMovement(Vector3 solvedPos, Quaternion solvedRot)
    {
        _intObj.transform.position = solvedPos;
        _intObj.transform.rotation = solvedRot;
    }
}
