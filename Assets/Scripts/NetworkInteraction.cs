using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(InteractionBehaviour))]
public class NetworkInteraction : NetworkBehaviour {

    private InteractionBehaviour _intObj;
    private AnchorableBehaviour _anchObj;
    private Rigidbody rb;
    private BoxCollider bc;


    void Start() {
        _intObj = GetComponent<InteractionBehaviour>();
        _intObj.OnGraspedMovement += onGraspedMovement;

        _anchObj = GetComponent<AnchorableBehaviour>();
        _anchObj.WhileAttachedToAnchor += whileAttachedToAnchor;

        _anchObj.OnAttachedToAnchor += onAttachedToAnchor;


        bc = GetComponent < BoxCollider>();

        rb = GetComponent<Rigidbody>();
       
    }

    void onAttachedToAnchor(AnchorableBehaviour anbobj, Anchor anchor)
    {
        AttachObjectManager attachObjectList =  anchor.GetComponentInParent<AttachObjectManager>();
        if (attachObjectList == null)
        {
            Debug.Log("no attach list");
        }
        else
        {
            attachObjectList.addObject(gameObject);
        }
    }

   
    private void onGraspedMovement(Vector3 presolvedPos, Quaternion presolvedRot,
                                   Vector3 solvedPos,    Quaternion solvedRot,
        List<InteractionController> graspingController) 
    {

         // Project the vector of the motion of the object due to grasping along the world X axis.

        // Move the object back to its position before the grasp solve this frame,
        // then add just its movement along the world X axis.

        CmdGraspedMovement(solvedPos, solvedRot);
    }


    private void whileAttachedToAnchor(AnchorableBehaviour anbobj, Anchor anchor)
    {

       // Debug.Log("Transform object");
        Transform t = anchor.transform;

        CmdAnchorMovement(t.position, t.rotation);
    }

    [Command]
    void CmdAnchorMovement(Vector3 pos, Quaternion rot)
    {
        rb.position = pos;
        rb.rotation = rot;
    }

    [Command]
    void CmdGraspedMovement(Vector3 solvedPos, Quaternion solvedRot)
    {
        _intObj.transform.position = solvedPos;
        _intObj.transform.rotation = solvedRot;
    }

}
