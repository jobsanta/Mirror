using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LayoutController : NetworkBehaviour{

	// Use this for initialization
    GameObject[] mockUpPrefab;
    Rigidbody mockUpRB;
    BoxCollider mockupBoxCollider;
    Collision col;
    BoxCollider componentCollider;
    GameObject interiorView;
    GameObject exteriorView;
    bool isFix = false;


	// Update is called once per frame
	void Start () 
    {
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectsWithTag("Layout");

        }
	}
       

    public void fixView()
    {

        Debug.Log("here is ok");
        CmdFrontView();
    }





    [Command]
    public void CmdFrontView()
    {
        isFix = true;

        Debug.Log("Front View Mode");
        for (int i = 0; i < mockUpPrefab.Length; i++)
        {
            mockUpRB =  mockUpPrefab[i].GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab[i].GetComponent<BoxCollider>();


            mockUpPrefab[i].transform.position = new Vector3(0.0f, 0.20f, -0.2f);
            mockUpPrefab[i].transform.RotateAround(Vector3.zero, Vector3.up, 0);


            mockupBoxCollider.enabled = false;
            mockUpRB.isKinematic = true;

            AttachObjectManager attachObjects =  mockUpPrefab[i].GetComponent<AttachObjectManager>();

            if (attachObjects != null)
            {
                foreach(GameObject g in attachObjects.childList)
                {
                    g.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }

    }

    [ClientRpc]
    public void RpcFrontView()
    {
        isFix = true;

        Debug.Log("Front View Mode");
        for (int i = 0; i < mockUpPrefab.Length; i++)
        {
            mockUpRB =  mockUpPrefab[i].GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab[i].GetComponent<BoxCollider>();


            mockUpPrefab[i].transform.position = new Vector3(0.0f, 0.20f, -0.2f);
            mockUpPrefab[i].transform.RotateAround(Vector3.zero, Vector3.up, 0);


            mockupBoxCollider.enabled = false;
            mockUpRB.isKinematic = true;

            AttachObjectManager attachObjects =  mockUpPrefab[i].GetComponent<AttachObjectManager>();

            if (attachObjects != null)
            {
                foreach(GameObject g in attachObjects.childList)
                {
                    g.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }

    }

    [Command]
    public void CmdFreeFall()
    {
        isFix = false;
        Debug.Log("Free View Mode");
        for (int i = 0; i < mockUpPrefab.Length; i++)
        {
            mockUpRB =  mockUpPrefab[i].GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab[i].GetComponent<BoxCollider>();

            mockupBoxCollider.enabled = true;
            mockUpRB.isKinematic = false;
            mockUpRB.useGravity = true;

            AttachObjectManager attachObjects =  mockUpPrefab[i].GetComponent<AttachObjectManager>();

            if (attachObjects != null)
            {
                foreach(GameObject g in attachObjects.childList)
                {
                    g.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }


    }

    [ClientRpc]
    public void RpcFreeFall()
    {
        isFix = false;
        Debug.Log("Free View Mode");
        for (int i = 0; i < mockUpPrefab.Length; i++)
        {
            mockUpRB =  mockUpPrefab[i].GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab[i].GetComponent<BoxCollider>();

            mockupBoxCollider.enabled = true;
            mockUpRB.isKinematic = false;
            mockUpRB.useGravity = true;

            AttachObjectManager attachObjects =  mockUpPrefab[i].GetComponent<AttachObjectManager>();

            if (attachObjects != null)
            {
                foreach(GameObject g in attachObjects.childList)
                {
                    g.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }


    }

    [Command] 
    public void CmdExteriorView()
    {
        for (int i = 0; i < mockUpPrefab.Length; i++)
        {
            mockUpPrefab[i].transform.GetChild(0).gameObject.SetActive(true);
                 

        }
    }

    [Command]
    public void CmdInteriorView()
    {
        for (int i = 0; i < mockUpPrefab.Length; i++)
        {
            mockUpPrefab[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    [Command]
    public void CmdRotate(float Single)
    {
        Debug.Log(Single);
        if (isFix)
        {
            for (int i = 0; i < mockUpPrefab.Length; i++)
            {

                mockUpPrefab[i].transform.position = new Vector3(0.0f, 0.20f, -0.2f);
                mockUpPrefab[i].transform.RotateAround(Vector3.zero, Vector3.up, Single);



            }
        }
    }


}
