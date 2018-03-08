﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class LayoutBodyController : NetworkBehaviour {

	// Use this for initialization
    void Start () 
    {
        Debug.Log(gameObject.name + ": isLocalPlayer " + isLocalPlayer + 
            " isServer " + isServer + " isClient " + isClient + " hasAuthority " + hasAuthority);
        if (isServer)
        {
            if (gameObject.transform.position.z > 0)
            {
               // gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.tag = "Untagged";
                gameObject.name = "Mockup(client)";

                StartCoroutine(DelayStart());
            }
            else
            {
                gameObject.name = "Mockup(server)";
            }
        }
        else
        {
            if (gameObject.transform.position.z < 0)
            {
               // gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.tag = "Untagged";
                gameObject.name = "Mockup(server)";
                StartCoroutine(DelayStart());
            }
            else
            {
                gameObject.name = "Mockup(client)";
            }
        }
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1.0f);
        LayoutController l = GameObject.FindGameObjectWithTag("Player").GetComponent<LayoutController>();
        l.setStartPoint();
    }
            
    
}
