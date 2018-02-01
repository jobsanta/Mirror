using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class RigidBodyController : NetworkBehaviour {

	// Use this for initialization

	void Start () 
    {
        Debug.Log(gameObject.name + ": isLocalPlayer " + isLocalPlayer + 
            " isServer " + isServer + " isClient " + isClient + " hasAuthority " + hasAuthority);
        if (!hasAuthority && !isServer)
            gameObject.GetComponent<BoxCollider>().enabled = false;
	}
	

}
