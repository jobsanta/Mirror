using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LayoutController : NetworkBehaviour{

	// Use this for initialization
    GameObject mockUpPrefab;
    Rigidbody mockUpRB;
    BoxCollider mockupBoxCollider;
    Collision col;
    BoxCollider componentCollider;
    GameObject interiorView;
    GameObject exteriorView;
    bool isFix = false;


    public void SetPrefab(GameObject o)
    {

        Debug.Log(o);
        mockUpPrefab = o;
    }

    public void SetFrontView()
    {
        Debug.Log(gameObject.name + ": isLocalPlayer " + isLocalPlayer + 
            " isServer " + isServer + " isClient " + isClient + " hasAuthority " + hasAuthority);
        if (isServer)
        {
            CmdFrontView();
        }
        else
        {
            FrontView();
        }
    }

    public void SetFreeFall()
    {
        Debug.Log(gameObject.name + ": isLocalPlayer " + isLocalPlayer + 
            " isServer " + isServer + " isClient " + isClient + " hasAuthority " + hasAuthority);
        if (isServer)
        {
            CmdFreeFall();
        }
        else
        {
            FreeFall();
        }
    }

    public void FrontView()
    {
        isFix = true;

        Debug.Log("Front View Mode");

            Debug.Log("Front View Mode activated correctly");
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }

        if (mockUpPrefab != null)
        {
            mockUpRB =  mockUpPrefab.GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab.GetComponent<BoxCollider>();


            mockUpPrefab.transform.position = new Vector3(0.0f, 0.20f, 0.2f);
            mockUpPrefab.transform.RotateAround(Vector3.zero, Vector3.up, 0);


            mockupBoxCollider.enabled = false;
            mockUpRB.isKinematic = true;

            AttachObjectManager attachObjects =  mockUpPrefab.GetComponent<AttachObjectManager>();

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
    public void CmdFrontView()
    {
        isFix = true;


        Debug.Log("Front View Mode");
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }

        if (mockUpPrefab != null)
        {
            mockUpRB =  mockUpPrefab.GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab.GetComponent<BoxCollider>();


            mockUpPrefab.transform.position = new Vector3(0.0f, 0.20f, -0.2f);
            mockUpPrefab.transform.RotateAround(Vector3.zero, Vector3.up, 0);


            mockupBoxCollider.enabled = false;
            mockUpRB.isKinematic = true;

            AttachObjectManager attachObjects =  mockUpPrefab.GetComponent<AttachObjectManager>();

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
        if (mockUpPrefab != null)
        {
            mockUpRB =  mockUpPrefab.GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab.GetComponent<BoxCollider>();


            mockUpPrefab.transform.position = new Vector3(0.0f, 0.20f, -0.2f);
            mockUpPrefab.transform.RotateAround(Vector3.zero, Vector3.up, 0);


            mockupBoxCollider.enabled = false;
            mockUpRB.isKinematic = true;

            AttachObjectManager attachObjects =  mockUpPrefab.GetComponent<AttachObjectManager>();

            if (attachObjects != null)
            {
                foreach(GameObject g in attachObjects.childList)
                {
                    g.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }

    }
    public void FreeFall()
    {
        isFix = false;
        Debug.Log("Free View Mode");
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }

        if (mockUpPrefab != null)
        {
            mockUpRB =  mockUpPrefab.GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab.GetComponent<BoxCollider>();

            mockupBoxCollider.enabled = true;
            mockUpRB.isKinematic = false;
            mockUpRB.useGravity = true;

            AttachObjectManager attachObjects =  mockUpPrefab.GetComponent<AttachObjectManager>();

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
    public void CmdFreeFall()
    {
        isFix = false;
        Debug.Log("Free View Mode");
        if (mockUpPrefab != null)
        {
            mockUpRB =  mockUpPrefab.GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab.GetComponent<BoxCollider>();

            mockupBoxCollider.enabled = true;
            mockUpRB.isKinematic = false;
            mockUpRB.useGravity = true;

            AttachObjectManager attachObjects =  mockUpPrefab.GetComponent<AttachObjectManager>();

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
        if (mockUpPrefab != null)
        {
            mockUpRB =  mockUpPrefab.GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab.GetComponent<BoxCollider>();

            mockupBoxCollider.enabled = true;
            mockUpRB.isKinematic = false;
            mockUpRB.useGravity = true;

            AttachObjectManager attachObjects =  mockUpPrefab.GetComponent<AttachObjectManager>();

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
        if (mockUpPrefab != null)
        {
            mockUpPrefab.transform.GetChild(0).gameObject.SetActive(true);
                 

        }
    }

    [Command]
    public void CmdInteriorView()
    {
        if (mockUpPrefab != null)
        {
            mockUpPrefab.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    [Command]
    public void CmdRotate(float Single)
    {
        Debug.Log(Single);
        if (isFix)
        {
            if (mockUpPrefab != null)
            {

                mockUpPrefab.transform.position = new Vector3(0.0f, 0.20f, -0.2f);
                mockUpPrefab.transform.RotateAround(Vector3.zero, Vector3.up, Single);



            }
        }
    }


}
