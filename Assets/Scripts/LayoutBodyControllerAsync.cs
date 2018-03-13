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
                //Renderer[] rends =  gameObject.GetComponentsInChildren<Renderer>();
                //for (int i = 0; i < rends.Length; i++)
                //    rends[i].enabled = false;

                //RawImage[] ris= gameObject.GetComponentsInChildren<RawImage>();
                //for (int i = 0; i < ris.Length; i++)
                //    ris[i].enabled = false;
                StartCoroutine(DelayStart(isServer));
                
                // gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.tag = "Untagged";
                gameObject.name = "Mockup(client)";
                gameObject.SetActive(false);


            }
            else
            {
                gameObject.name = "Mockup(server)";
                StartCoroutine(DelayStart(isServer));
            }
        }
        else
        {
            if (gameObject.transform.position.z < 0)
            {
                //Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
                //for (int i = 0; i < rends.Length; i++)
                //    rends[i].enabled = false;

                //RawImage[] ris = gameObject.GetComponentsInChildren<RawImage>();
                //for (int i = 0; i < ris.Length; i++)
                //    ris[i].enabled = false;

                //gameObject.GetComponent<BoxCollider>().enabled = false;

                StartCoroutine(DelayStart(isServer));
               
                gameObject.tag = "Untagged";
                gameObject.name = "Mockup(server)";
                gameObject.SetActive(false);

            }
            else
            {
                gameObject.name = "Mockup(client)";
                StartCoroutine(DelayStart(isServer));
            }
        }
    }

    IEnumerator DelayStart(bool thisIsServer)
    {

        yield return new WaitForSeconds(0.5f);
        //if (thisIsServer)
        //{
        //    GameObject[] exteriorObject = GameObject.FindGameObjectsWithTag("Exterior");
        //    for (int i = 0; i < exteriorObject.Length; i++)
        //    {
        //        if (exteriorObject[i].layer == 10 && exteriorObject[i].transform.position.z > 0)
        //            exteriorObject[i].SetActive(false);
        //    }


        //}
        //else
        //{
        //    GameObject[] interiors = GameObject.FindGameObjectsWithTag("Interior");
        //    for (int i = 0; i < interiors.Length; i++)
        //    {
        //        if (interiors[i].layer == 8 && interiors[i].transform.position.z < 0)
        //            interiors[i].SetActive(false);
        //    }
        //}

        LayoutController l = GameObject.FindGameObjectWithTag("Player").GetComponent<LayoutController>();
        l.setBillboardStartPoint();


    }


}
