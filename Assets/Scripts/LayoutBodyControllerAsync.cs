using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LayoutBodyControllerAsync : NetworkBehaviour {

	// Use this for initialization
    void Start () 
    {
        Debug.Log(gameObject.name + ": isLocalPlayer " + isLocalPlayer + 
            " isServer " + isServer + " isClient " + isClient + " hasAuthority " + hasAuthority);
        if (isServer)
        {
            if (gameObject.transform.position.z > 0)
            {
                Renderer[] rends =  gameObject.GetComponentsInChildren<Renderer>();
                for (int i = 0; i < rends.Length; i++)
                    rends[i].enabled = false;

                RawImage[] ris= gameObject.GetComponentsInChildren<RawImage>();
                for (int i = 0; i < ris.Length; i++)
                    ris[i].enabled = false;


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
                Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
                for (int i = 0; i < rends.Length; i++)
                    rends[i].enabled = false;

                RawImage[] ris = gameObject.GetComponentsInChildren<RawImage>();
                for (int i = 0; i < ris.Length; i++)
                    ris[i].enabled = false;

                //gameObject.GetComponent<BoxCollider>().enabled = false;

                
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
        LayoutController l = GameObject.FindGameObjectWithTag("Player").GetComponent<LayoutController>();
        return l.setBillboardStartPoint();
    }


}
