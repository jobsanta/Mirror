using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(InteractionBehaviour))]
public class NetworkInteraction : NetworkBehaviour {

    private InteractionBehaviour _intObj;

    void Start() {
        _intObj = GetComponent<InteractionBehaviour>();
        _intObj.OnGraspedMovement += onGraspedMovement;


        Debug.Log("get interaction behavior");
    }

   
    private void onGraspedMovement(Vector3 presolvedPos, Quaternion presolvedRot,
                                   Vector3 solvedPos,    Quaternion solvedRot,
        List<InteractionController> graspingController) 
    {

         // Project the vector of the motion of the object due to grasping along the world X axis.

        // Move the object back to its position before the grasp solve this frame,
        // then add just its movement along the world X axis.

        Debug.Log("isClient " + isClient + " isServer " + isServer + " islocalplayer" + isLocalPlayer);
        Debug.Log(" localPlayerAuthroity " + localPlayerAuthority + " hasAuthority " + hasAuthority);

        if (!isClient)
            return;

        Debug.Log("sending command");
        CmdGraspedMovement(solvedPos, solvedRot);
    }

    [Command]
    void CmdGraspedMovement(Vector3 solvedPos, Quaternion solvedRot)
    {
        _intObj.rigidbody.position = solvedPos;
        _intObj.rigidbody.rotation = solvedRot;
    }

}
