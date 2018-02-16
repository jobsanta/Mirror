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
    static public bool isOwnInteriorView = false;
    static public bool isTheirInteriorView = false;
    static public bool isBillBoardInteriorView = false;

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

    public void SetHorizontalPosition(float h)
    {
        if (!isFix)
        {
            SetFrontView();
        }
        if (isServer)
        {
            CmdHorizontalPosition(h);
        }
        else
        {
            HorizontalPosition(h);
        }
    }
    public void SetVerticalPosition(float v)
    {
        if (!isFix)
        {
            SetFrontView();
        }
        if (isServer)
        {
            CmdVerticalPostion(v);
        }
        else
        {
            VerticalPosition(v);
        }
    }

    public void SetHorizontalRotation(float h)
    {
        if (!isFix)
        {
            SetFrontView();
        }
        if (isServer)
        {
            CmdHorizontalRotation(h);
        }
        else
        {
            HorizontalRotation(h);
        }
    }
    public void SetVerticalRotation(float v)
    {
        if (!isFix)
        {
            SetFrontView();
        }
        if (isServer)
        {
            CmdVerticalRotation(v);
        }
        else
        {
            VerticalRotation(v);
        }
    }
    
    void HorizontalPosition(float h)
    {
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }

        if (mockUpPrefab != null)
        {
            Vector3 currentPosition = mockUpPrefab.transform.position;
            mockUpPrefab.transform.position = new Vector3(h, currentPosition.y, currentPosition.z);
        }
    }
    [Command]
    void CmdHorizontalPosition(float h)
    {
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }

        if (mockUpPrefab != null)
        {
            Vector3 currentPosition = mockUpPrefab.transform.position;
            mockUpPrefab.transform.position = new Vector3(h, currentPosition.y, currentPosition.z);
        }
    }

    void VerticalPosition(float v)
    {
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }

        if (mockUpPrefab != null)
        {
            Vector3 currentPosition = mockUpPrefab.transform.position;
            mockUpPrefab.transform.position = new Vector3(currentPosition.x, v, currentPosition.z);
        }
    }
    [Command]
    void CmdVerticalPostion(float v)
    {
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }

        if (mockUpPrefab != null)
        {
            Vector3 currentPosition = mockUpPrefab.transform.position;
            mockUpPrefab.transform.position = new Vector3(currentPosition.x, v, currentPosition.z);
        
        }
    }
    
    void HorizontalRotation(float h)
    {
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }

        if (mockUpPrefab != null)
        {
            Vector3 currentRotation = mockUpPrefab.transform.eulerAngles;
            mockUpPrefab.transform.eulerAngles = new Vector3(currentRotation.x, h, currentRotation.z);
        }
    }
    [Command]
    void CmdHorizontalRotation(float h)
    {
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }

        if (mockUpPrefab != null)
        {
            Vector3 currentRotation = mockUpPrefab.transform.eulerAngles;
            mockUpPrefab.transform.eulerAngles = new Vector3(currentRotation.x, h, currentRotation.z);
        }
    }

    void VerticalRotation(float v)
    {
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }

        if (mockUpPrefab != null)
        {
            Vector3 currentRotation = mockUpPrefab.transform.eulerAngles;
            mockUpPrefab.transform.eulerAngles = new Vector3( v, currentRotation.y, currentRotation.z);
        }
    }
    [Command]
    void CmdVerticalRotation(float v)
    {
        if (mockUpPrefab == null)
        {
            mockUpPrefab = GameObject.FindGameObjectWithTag("Layout");
        }
        if (mockUpPrefab != null)
        {
            Vector3 currentRotation = mockUpPrefab.transform.eulerAngles;
            mockUpPrefab.transform.eulerAngles = new Vector3(v, currentRotation.y, currentRotation.z);
        }
    }

    void FrontView()
    {
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
                    if (g.tag == "Exterior")
                        g.GetComponent<BoxCollider>().enabled = true;
                }
            }
            isFix = true;

        }



    }
    
    [Command]
    void CmdFrontView()
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
                    if (g.tag == "Interior")
                        g.GetComponent<BoxCollider>().enabled = true;
                }
            }
            isFix = true;

        }

    }

    void FreeFall()
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
    void CmdFreeFall()
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


    public void SetOwnSkeletonView()
    {
        if (!isOwnInteriorView)
        {
               InteriorView(isServer, true);
            
        }
        else
        {
               ExteriorView(isServer, true);
        }
    }

    public void SetTheirSkeletonView()
    {
        if (!isTheirInteriorView)
        {
            InteriorView(isServer, false);

        }
        else
        {
            ExteriorView(isServer, false);
        }
    }

    public void SetBillboardSkeletonView()
    {
        if (!isBillBoardInteriorView)
        {
            BillboardInteriorView();

        }
        else
        {
            BillboardExteriorView();
        }
    }



    void ExteriorView(bool isServ, bool isOwn)
    {
        if (isServ)
        {
            GameObject[] exteriorObject = GameObject.FindGameObjectsWithTag("Exterior");

            for (int i = 0; i < exteriorObject.Length; i++)
            {
                if(isOwn)
                {
                    if (exteriorObject[i].transform.position.z < 0)
                        exteriorObject[i].GetComponentInChildren<Renderer>().enabled = true;
                }
                else
                    if (exteriorObject[i].transform.position.z > 0)
                    exteriorObject[i].GetComponentInChildren<Renderer>().enabled = true;
            }
            GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

            for (int i = 0; i < interioObjects.Length; i++)
            {
                if(isOwn)
                {
                    if (interioObjects[i].transform.position.z < 0)
                        interioObjects[i].GetComponentInChildren<Renderer>().enabled = false;
                }
                else
                    if (interioObjects[i].transform.position.z > 0)
                        interioObjects[i].GetComponentInChildren<Renderer>().enabled = false;
            }
            if (isOwn)
                isOwnInteriorView = false;
            else
                isTheirInteriorView = false;
        }
        else
        {
            GameObject[] exteriorObject = GameObject.FindGameObjectsWithTag("Exterior");

            for (int i = 0; i < exteriorObject.Length; i++)
            {
                if (isOwn)
                {
                    if (exteriorObject[i].transform.position.z > 0)
                        exteriorObject[i].GetComponentInChildren<Renderer>().enabled = true;
                }
                else
                    if (exteriorObject[i].transform.position.z < 0)
                        exteriorObject[i].GetComponentInChildren<Renderer>().enabled = true;
            }
            GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

            for (int i = 0; i < interioObjects.Length; i++)
            {
                if (isOwn)
                {
                    if (interioObjects[i].transform.position.z > 0)
                        interioObjects[i].GetComponentInChildren<Renderer>().enabled = false;
                }
                else
                    if (interioObjects[i].transform.position.z < 0)
                        interioObjects[i].GetComponentInChildren<Renderer>().enabled = false;
            }
            if (isOwn)
                isOwnInteriorView = false;
            else
                isTheirInteriorView = false;
        }

    }
    
    void InteriorView(bool isServ, bool isOwn)
    {

        if (isServ)
        {
            GameObject[] exteriorObject = GameObject.FindGameObjectsWithTag("Exterior");

            for (int i = 0; i < exteriorObject.Length; i++)
            {
                if (isOwn)
                {
                    if (exteriorObject[i].transform.position.z < 0)
                        exteriorObject[i].GetComponentInChildren<Renderer>().enabled = false;
                }
                else
                    if (exteriorObject[i].transform.position.z > 0)
                    exteriorObject[i].GetComponentInChildren<Renderer>().enabled = false;
            }
            GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

            for (int i = 0; i < interioObjects.Length; i++)
            {
                if (isOwn)
                {
                    if (interioObjects[i].transform.position.z < 0)
                        interioObjects[i].GetComponentInChildren<Renderer>().enabled = true;
                }
                else
                    if (interioObjects[i].transform.position.z > 0)
                    interioObjects[i].GetComponentInChildren<Renderer>().enabled = true;
            }
            if (isOwn)
                isOwnInteriorView = true;
            else
                isTheirInteriorView = true;
        }
        else
        {
            GameObject[] exteriorObject = GameObject.FindGameObjectsWithTag("Exterior");

            for (int i = 0; i < exteriorObject.Length; i++)
            {
                if (isOwn)
                {
                    if (exteriorObject[i].transform.position.z > 0)
                        exteriorObject[i].GetComponentInChildren<Renderer>().enabled = false;
                }
                else
                    if (exteriorObject[i].transform.position.z < 0)
                    exteriorObject[i].GetComponentInChildren<Renderer>().enabled = false;
            }
            GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

            for (int i = 0; i < interioObjects.Length; i++)
            {
                if (isOwn)
                {
                    if (interioObjects[i].transform.position.z > 0)
                        interioObjects[i].GetComponentInChildren<Renderer>().enabled = true;
                }
                else
                    if (interioObjects[i].transform.position.z < 0)
                    interioObjects[i].GetComponentInChildren<Renderer>().enabled = true;
            }
            if (isOwn)
                isOwnInteriorView = true;
            else
                isTheirInteriorView = true;
        }
    }


    void BillboardExteriorView()
    {
        GameObject billboard = GameObject.Find("Mockup(billboard)");
        if(billboard != null)
        {
            Renderer[] components = billboard.GetComponentsInChildren<Renderer>();
            for(int i =0;i<components.Length;i++)
            {
                if (components[i].gameObject.tag == "Exterior")
                {
                    components[i].enabled = true;
                }
                else if (components[i].gameObject.tag == "Interior")
                {
                    components[i].enabled = false;
                }
            }

            foreach( GameObject o in billboard.GetComponent<AttachObjectManager>().childList)
            {
                if (o.tag == "Exterior")
                {
                    o.GetComponentInChildren<Renderer>().enabled = true;
                }
                else if (o.tag == "Interior")
                {
                    o.GetComponentInChildren<Renderer>().enabled = false;
                }
            }

        }
        isBillBoardInteriorView = false;
 
    }

    void BillboardInteriorView()
    {
        GameObject billboard = GameObject.Find("Mockup(billboard)");
        if (billboard != null)
        {
            Renderer[] components = billboard.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i].gameObject.tag == "Exterior")
                {
                    components[i].enabled = false;
                }
                else if (components[i].gameObject.tag == "Interior")
                {
                    components[i].enabled = true;
                }
            }

            foreach (GameObject o in billboard.GetComponent<AttachObjectManager>().childList)
            {
                if (o.tag == "Exterior")
                {
                    o.GetComponentInChildren<Renderer>().enabled = false;
                }
                else if (o.tag == "Interior")
                {
                    o.GetComponentInChildren<Renderer>().enabled = true;
                }
            }

        }
        isBillBoardInteriorView = true;
    }

}
