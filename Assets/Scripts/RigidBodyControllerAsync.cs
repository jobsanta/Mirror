using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Leap.Unity.Interaction;

public class RigidBodyControllerAsync : NetworkBehaviour {

    // Use this for initialization
    private AnchorableBehaviour _anchObj;
    void Start () 
    {
        Debug.Log(gameObject.name + ": isLocalPlayer " + isLocalPlayer +
    " isServer " + isServer + " isClient " + isClient + " hasAuthority " + hasAuthority);
        if (isServer)
        {
            if (gameObject.transform.position.z > 0)
            {
                _anchObj = GetComponent<AnchorableBehaviour>();

                    Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rends.Length; i++)
                        rends[i].enabled = false;

                
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //gameObject.tag = "Untagged";
            }
        }
        else
        {   
            if (gameObject.transform.position.z < 0)
            {
                _anchObj = GetComponent<AnchorableBehaviour>();

                    Debug.Log("disable render");
                    Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rends.Length; i++)
                        rends[i].enabled = false;

                
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //gameObject.tag = "Untagged";

            }

        }

	}


	

}
