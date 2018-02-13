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
    bool isInteriorView = false;


    public void SetPrefab(GameObject o)
    {
        mockUpPrefab = o;
    }

    public void SetFrontView()
    {
        Debug.Log(gameObject.name + ": isLocalPlayer " + isLocalPlayer + 
            " isServer " + isServer + " isClient " + isClient + " hasAuthority " + hasAuthority);

        if (!isFix)
        {
            if (isServer)
            {
                CmdFrontView();
            }
            else
            {
                FrontView();
            }
        }
        else
        {
            if (isServer)
            {
                CmdFreeFall();
            }
            else
            {
                FreeFall();
            }
        }

    }

    public void SetSkeletonView()
    {
        if (!isInteriorView)
        {
            if (isServer)
            {
                CmdInteriorView();
            }
            else
            {
                InteriorView();
            }
        }
        else
        {
            if (isServer)
            {
                CmdExteriorView();
            }
            else
            {
                ExteriorView();
            }
        }
    }


    public void FrontView()
    {
  
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
            isFix = true;

        }



    }



    [Command]
    public void CmdFrontView()
    {
  

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
            isFix = true;

        }

    }

    public void FreeFall()
    {
        
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
            isFix = false;
        }


    }


    [Command]
    public void CmdFreeFall()
    {
   
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }
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
            isFix = false;
        }


    }
        
    public void ExteriorView()
    {
        GameObject[] exteriorObject = GameObject.FindGameObjectsWithTag("Exterior");

        for (int i = 0; i < exteriorObject.Length; i++)
        {
            exteriorObject[i].GetComponentInChildren<Renderer>().enabled = true;
        }
        GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

        for (int i = 0; i < interioObjects.Length; i++)
        {
            interioObjects[i].GetComponentInChildren<Renderer>().enabled = false;
        }
        isInteriorView = false;
    }


    public void InteriorView()
    {

        GameObject[] exteriorObject = GameObject.FindGameObjectsWithTag("Exterior");

        for (int i = 0; i < exteriorObject.Length; i++)
        {
            exteriorObject[i].GetComponentInChildren<Renderer>().enabled = false;
        }

        GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

        for (int i = 0; i < interioObjects.Length; i++)
        {
            interioObjects[i].GetComponentInChildren<Renderer>().enabled = true;
        }
        isInteriorView = true;
    }

    [Command] 
    public void CmdExteriorView()
    {


        GameObject[] exteriorObject = GameObject.FindGameObjectsWithTag("Exterior");


        for (int i = 0; i < exteriorObject.Length; i++)
        {
            Debug.Log(exteriorObject[i].name);
            exteriorObject[i].GetComponentInChildren<Renderer>().enabled = true;
        }

        GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

        for (int i = 0; i < interioObjects.Length; i++)
        {
            interioObjects[i].GetComponentInChildren<Renderer>().enabled = false;
        }
        isInteriorView = false;
    }

    [Command]
    public void CmdInteriorView()
    {
        GameObject[] exteriorObject = GameObject.FindGameObjectsWithTag("Exterior");

        for (int i = 0; i < exteriorObject.Length; i++)
        {
            exteriorObject[i].GetComponentInChildren<Renderer>().enabled = false;
        }

        GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

        for (int i = 0; i < interioObjects.Length; i++)
        {
            interioObjects[i].GetComponentInChildren<Renderer>().enabled = true;
        }

        isInteriorView = true;
        
    }


    public void Rotate(float Single)
    {
        if (isFix)
        {
            if (mockUpPrefab != null)
            {

                mockUpPrefab.transform.position = new Vector3(0.0f, 0.20f, -0.2f);
                mockUpPrefab.transform.RotateAround(Vector3.zero, Vector3.up, Single);



            }
        }
    }

    [Command]
    public void CmdRotate(float Single)
    {
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
