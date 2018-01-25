using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LayoutController : NetworkBehaviour{

	// Use this for initialization
    GameObject[] mockUpPrefab;
    GameObject[] innerComponentPrefab;
    Rigidbody mockUpRB;
    BoxCollider mockupBoxCollider;
    Collision col;
    BoxCollider componentCollider;

	// Update is called once per frame
	void Update () 
    {
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectsWithTag("Layout");
            innerComponentPrefab = GameObject.FindGameObjectsWithTag("Components");
        }
	}

    [Command]
    public void CmdFrontView()
    {
     
        Debug.Log("Front View Mode");
        for (int i = 0; i < mockUpPrefab.Length; i++)
        {
            mockUpRB =  mockUpPrefab[i].GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab[i].GetComponent<BoxCollider>();


            mockUpPrefab[i].transform.position = new Vector3(0.0f, 0.20f, -0.2f);
            mockUpPrefab[i].transform.LookAt(Camera.main.transform);


            mockupBoxCollider.enabled = false;
            mockUpRB.isKinematic = true;


        }

        for (int i = 0; i < innerComponentPrefab.Length; i++)
        {
            componentCollider = innerComponentPrefab[i].GetComponent<BoxCollider>();

            componentCollider.enabled = true;
        }
    }

    [Command]
    public void CmdFreeFall()
    {
        Debug.Log("Free View Mode");
        for (int i = 0; i < mockUpPrefab.Length; i++)
        {
            mockUpRB =  mockUpPrefab[i].GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab[i].GetComponent<BoxCollider>();

            mockupBoxCollider.enabled = true;
            mockUpRB.isKinematic = false;
            mockUpRB.useGravity = true;
        }

    }


}
