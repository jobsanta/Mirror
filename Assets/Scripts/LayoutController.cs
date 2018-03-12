using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Leap.Unity.Interaction;
using Leap.Unity.Examples;
using UnityEngine.UI;

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

    public bool isPR;
    public bool isHybrid;
    static public bool globalHybrid;
    static public bool globalPR;
    static public bool isOwnInteriorView = false;
    static public bool isTheirInteriorView = false;
    static public bool isBillBoardInteriorView = false;

    GameObject transfertool;

    static public bool thisisServer;

    private void Start()
    {
        thisisServer = isServer;
        globalPR = isPR;
        globalHybrid = isHybrid;
       
    }


    public IEnumerator setStartPoint()
    {
        yield return new WaitForSeconds(0.5f);

        if(isServer)
        {
            InteriorView(isServer, true);
            ExteriorView(isServer, false);
        }
        else
        {
            InteriorView(isServer, false);
            ExteriorView(isServer, true);
        }
    }

    public IEnumerator setBillboardStartPoint()
    {
        yield return new WaitForSeconds(0.5f);

        if (isServer)
        {
            InteriorView(isServer, true);
            BillboardExteriorView();
        }
        else
        {
            BillboardInteriorView();
            ExteriorView(isServer, true);
        }
    }



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
            if(transfertool == null)
            {
                transfertool = GameObject.Find("TransformTool");
            }
            transfertool.GetComponent<TransformTool>().Enable = true;
            transfertool.SetActive(true);

            mockUpRB =  mockUpPrefab.GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab.GetComponent<BoxCollider>();


            mockUpPrefab.transform.position = new Vector3(0.0f, 0.20f, 0.2f);
            mockUpPrefab.transform.RotateAround(Vector3.zero, Vector3.up, 0);


            mockupBoxCollider.enabled = false;
            mockUpRB.isKinematic = true;

            AttachObjectManager attachObjects =  mockUpPrefab.GetComponent<AttachObjectManager>();

            if (attachObjects != null)
            {
                foreach (GameObject g in attachObjects.exteriorList)
                {
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
            GameObject transtool = GameObject.Find("TransformTool");
            if (transfertool == null)
            {
                transfertool = GameObject.Find("TransformTool");
            }
            transfertool.GetComponent<TransformTool>().Enable = true;
            transfertool.SetActive(true);

            mockUpRB =  mockUpPrefab.GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab.GetComponent<BoxCollider>();


            mockUpPrefab.transform.position = new Vector3(0.0f, 0.20f, -0.2f);
            mockUpPrefab.transform.RotateAround(Vector3.zero, Vector3.up, 0);


            mockupBoxCollider.enabled = false;
            mockUpRB.isKinematic = true;

            AttachObjectManager attachObjects =  mockUpPrefab.GetComponent<AttachObjectManager>();

            if (attachObjects != null)
            {
                foreach (GameObject g in attachObjects.interiorList)
                {
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
            if (transfertool == null)
            {
                transfertool = GameObject.Find("TransformTool");
            }
            transfertool.GetComponent<TransformTool>().Enable = false;
            transfertool.SetActive(false);

            mockUpRB =  mockUpPrefab.GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab.GetComponent<BoxCollider>();

            mockupBoxCollider.enabled = true;
            mockUpRB.isKinematic = false;
            mockUpRB.useGravity = true;

            AttachObjectManager attachObjects =  mockUpPrefab.GetComponent<AttachObjectManager>();

            if (attachObjects != null)
            {
                foreach (GameObject g in attachObjects.interiorList)
                {
                    g.GetComponent<BoxCollider>().enabled = false;
                }
                foreach (GameObject g in attachObjects.exteriorList)
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
            if (transfertool == null)
            {
                transfertool = GameObject.Find("TransformTool");
            }
            transfertool.GetComponent<TransformTool>().Enable = false;
            transfertool.SetActive(false);

            mockUpRB =  mockUpPrefab.GetComponent<Rigidbody>();
            mockupBoxCollider =  mockUpPrefab.GetComponent<BoxCollider>();

            mockupBoxCollider.enabled = true;
            mockUpRB.isKinematic = false;
            mockUpRB.useGravity = true;

            AttachObjectManager attachObjects =  mockUpPrefab.GetComponent<AttachObjectManager>();

            if (attachObjects != null)
            {
                foreach(GameObject g in attachObjects.interiorList)
                {
                    g.GetComponent<BoxCollider>().enabled = false;
                }
                foreach (GameObject g in attachObjects.exteriorList)
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
                    {
                        Renderer[] rs = exteriorObject[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = true;

                        Text[] ts = exteriorObject[i].gameObject.GetComponentsInChildren<Text>();
                        for (int j = 0; j < ts.Length; j++) ts[j].enabled = true;

                        RawImage[] ri = exteriorObject[i].gameObject.GetComponentsInChildren<RawImage>();
                        for (int j = 0; j < ri.Length; j++) ri[j].enabled = true;


                    }
                }
                else
                    if (exteriorObject[i].transform.position.z > 0)
                    {
                        Renderer[] rs = exteriorObject[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = true;

                        Text[] ts = exteriorObject[i].gameObject.GetComponentsInChildren<Text>();
                        for (int j = 0; j < ts.Length; j++) ts[j].enabled = true;

                        RawImage[] ri = exteriorObject[i].gameObject.GetComponentsInChildren<RawImage>();
                        for (int j = 0; j < ri.Length; j++) ri[j].enabled = true;

                }
            }
            GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

            for (int i = 0; i < interioObjects.Length; i++)
            {
                if(isOwn)
                {
                    if (interioObjects[i].transform.position.z < 0)
                    {
                        Renderer[] rs = interioObjects[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = false;

                        InteractionBehaviour[] ib = interioObjects[i].GetComponentsInChildren<InteractionBehaviour>();
                        for (int j = 0; j < ib.Length; j++) ib[j].enabled = false;

                    }
                }
                else
                    if (interioObjects[i].transform.position.z > 0)
                    {
                        Renderer[] rs = interioObjects[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = false;
                    }
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
                    {
                        Renderer[] rs = exteriorObject[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = true;

                        Text[] ts = exteriorObject[i].gameObject.GetComponentsInChildren<Text>();
                        for (int j = 0; j < ts.Length; j++) ts[j].enabled = true;

                        RawImage[] ri = exteriorObject[i].gameObject.GetComponentsInChildren<RawImage>();
                        for (int j = 0; j < ri.Length; j++) ri[j].enabled = true;

                        InteractionBehaviour[] ib = exteriorObject[i].GetComponentsInChildren<InteractionBehaviour>();
                        for (int j = 0; j < ib.Length; j++) ib[j].enabled = true;
                    }
                }
                else
                    if (exteriorObject[i].transform.position.z < 0)
                    {
                        Renderer[] rs = exteriorObject[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = true;

                    Text[] ts = exteriorObject[i].gameObject.GetComponentsInChildren<Text>();
                    for (int j = 0; j < ts.Length; j++) ts[j].enabled = true;

                    RawImage[] ri = exteriorObject[i].gameObject.GetComponentsInChildren<RawImage>();
                    for (int j = 0; j < ri.Length; j++) ri[j].enabled = true;
                }
            }
            GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

            for (int i = 0; i < interioObjects.Length; i++)
            {
                if (isOwn)
                {
                    if (interioObjects[i].transform.position.z > 0)
                    {
                        Renderer[] rs = interioObjects[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = false;
                    }
                }
                else
                    if (interioObjects[i].transform.position.z < 0)
                    {
                        Renderer[] rs = interioObjects[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = false;
                    }
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
                    {
                        Renderer[] rs = exteriorObject[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = false;

                        Text[] ts = exteriorObject[i].gameObject.GetComponentsInChildren<Text>();
                        for (int j = 0; j < ts.Length; j++) ts[j].enabled = false;

                        RawImage[] ri = exteriorObject[i].gameObject.GetComponentsInChildren<RawImage>();
                        for (int j = 0; j < ri.Length; j++) ri[j].enabled = false;
                    }
                }
                else
                { 

                    if (exteriorObject[i].transform.position.z > 0)
                    {
                        Renderer[] rs = exteriorObject[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = false;
                        Text[] ts = exteriorObject[i].gameObject.GetComponentsInChildren<Text>();
                        for (int j = 0; j < ts.Length; j++) ts[j].enabled = false;

                        RawImage[] ri = exteriorObject[i].gameObject.GetComponentsInChildren<RawImage>();
                        for (int j = 0; j < ri.Length; j++) ri[j].enabled = false;
                    }
                }
            }
            GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

            for (int i = 0; i < interioObjects.Length; i++)
            {
                if (isOwn)
                {
                    if (interioObjects[i].transform.position.z < 0)
                    {
                        Renderer[] rs = interioObjects[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = true;

                        InteractionBehaviour[] ib = interioObjects[i].GetComponentsInChildren<InteractionBehaviour>();
                        for (int j = 0; j < ib.Length; j++) ib[j].enabled = true;
                    }
                }
                else
                    if (interioObjects[i].transform.position.z > 0)
                    {
                        Renderer[] rs = interioObjects[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = true; ;
                    }
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
                    {
                        Renderer[] rs = exteriorObject[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = false;
                        Text[] ts = exteriorObject[i].gameObject.GetComponentsInChildren<Text>();
                        for (int j = 0; j < ts.Length; j++) ts[j].enabled = false;

                        RawImage[] ri = exteriorObject[i].gameObject.GetComponentsInChildren<RawImage>();
                        for (int j = 0; j < ri.Length; j++) ri[j].enabled = false;

                        InteractionBehaviour[] ib = exteriorObject[i].GetComponentsInChildren<InteractionBehaviour>();
                        for (int j = 0; j < ib.Length; j++) ib[j].enabled = false;
                    }
                }
                else
                    if (exteriorObject[i].transform.position.z < 0)
                    {
                        Renderer[] rs = exteriorObject[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = false;
                        Text[] ts = exteriorObject[i].gameObject.GetComponentsInChildren<Text>();
                        for (int j = 0; j < ts.Length; j++) ts[j].enabled = false;

                    RawImage[] ri = exteriorObject[i].gameObject.GetComponentsInChildren<RawImage>();
                    for (int j = 0; j < ri.Length; j++) ri[j].enabled = false;
                }
            }
            GameObject[] interioObjects = GameObject.FindGameObjectsWithTag("Interior");

            for (int i = 0; i < interioObjects.Length; i++)
            {
                if (isOwn)
                {
                    if (interioObjects[i].transform.position.z > 0)
                    {
                        Renderer[] rs = interioObjects[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = true; ;


                    }
                }
                else
                    if (interioObjects[i].transform.position.z < 0)
                    {
                        Renderer[] rs = interioObjects[i].GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < rs.Length; j++) rs[j].enabled = true;
                    }
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

            GameObject Child = billboard.transform.GetChild(0).gameObject;
            Child.SetActive(false);
            Child = billboard.transform.GetChild(1).gameObject;
            Child.SetActive(true);
            Child = billboard.transform.GetChild(2).gameObject;
            Child.SetActive(false);
            Child = billboard.transform.GetChild(3).gameObject;
            Child.SetActive(true);
            Child = billboard.transform.GetChild(4).gameObject;
            Child.SetActive(false);

            foreach (GameObject o in billboard.GetComponent<AttachObjectManager>().exteriorList)
            {

                    Renderer[] rs = o.GetComponentsInChildren<Renderer>();
                    for (int j = 0; j < rs.Length; j++) rs[j].enabled = true;

                    Text[] ts = o.gameObject.GetComponentsInChildren<Text>();
                    for (int j = 0; j < ts.Length; j++) ts[j].enabled = true;


            }
            foreach (GameObject o in billboard.GetComponent<AttachObjectManager>().interiorList)
            {

                Renderer[] rs = o.GetComponentsInChildren<Renderer>();
                for (int j = 0; j < rs.Length; j++) rs[j].enabled = false;
            }
            

        }
        isBillBoardInteriorView = false;
 
    }

    void BillboardInteriorView()
    {
        GameObject billboard = GameObject.Find("Mockup(billboard)");
        if (billboard != null)
        {
            GameObject Child = billboard.transform.GetChild(0).gameObject;
            Child.SetActive(true);
            Child = billboard.transform.GetChild(1).gameObject;
            Child.SetActive(false);
            Child = billboard.transform.GetChild(2).gameObject;
            Child.SetActive(true);
            Child = billboard.transform.GetChild(3).gameObject;
            Child.SetActive(false);
            Child = billboard.transform.GetChild(4).gameObject;
            Child.SetActive(true);


            foreach (GameObject o in billboard.GetComponent<AttachObjectManager>().exteriorList)
            {

                Renderer[] rs = o.GetComponentsInChildren<Renderer>();
                for (int j = 0; j < rs.Length; j++) rs[j].enabled = false;
                Text[] ts = o.gameObject.GetComponentsInChildren<Text>();
                for (int j = 0; j < ts.Length; j++) ts[j].enabled = false;
            }
            foreach (GameObject o in billboard.GetComponent<AttachObjectManager>().interiorList)
            {

                Renderer[] rs = o.GetComponentsInChildren<Renderer>();
                for (int j = 0; j < rs.Length; j++) rs[j].enabled = true;

            }
        }
        isBillBoardInteriorView = true;
    }

    public void Synchronize()
    {
        if (isPR)
            return;

        if (isServer)
        {

            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                AttachObjectManager attachObjects = layout.GetComponent<AttachObjectManager>();
                List<GameObject> attachedList = attachObjects.getInteriorList();
                foreach (GameObject o in attachedList)
                {

                        DestroyObject(o);
                }
                attachObjects.clearInteriorObject();
            }
            RpcDeleteObjects();

            //Crete new objects from server layout
            layout = GameObject.Find("Mockup(server)");
            if (layout != null)
            {
                AttachObjectManager attachObjects =  layout.GetComponent<AttachObjectManager>();
                List<GameObject> attachedList = attachObjects.getInteriorList();
                foreach(GameObject o in attachedList)
                {

                  Anchor a = o.GetComponent<AnchorableBehaviour>().anchor;
                  o.GetComponent<AnchorNetworkInteractionAsync>().CreateCopyComponent(o, o.transform.position, o.transform.rotation, a.name);

                }

            }
        }
        else
        {

            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                AttachObjectManager attachObjects = layout.GetComponent<AttachObjectManager>();
                List<GameObject> attachedList = attachObjects.getExteriorList();
                foreach (GameObject o in attachedList)
                {

                        DestroyObject(o);

                }
                attachObjects.clearExteriorObject();
            }
            CmdDeleteObjects();
            //Create new object from client layout
            layout = GameObject.Find("Mockup(client)");
            if (layout != null)
            {
                AttachObjectManager attachObjects = layout.GetComponent<AttachObjectManager>();
                List<GameObject> attachedList = attachObjects.getExteriorList();
                foreach (GameObject o in attachedList)
                {
                        Anchor a = o.GetComponent<AnchorableBehaviour>().anchor;
                        o.GetComponent<AnchorNetworkInteractionAsync>().CreateCopyComponent(o, o.transform.position, o.transform.rotation, a.name);
                }

            }
        }
    }

    [ClientRpc]
    void RpcDeleteObjects()
    {
        if(!isServer)
        {
            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                AttachObjectManager attachObjects = layout.GetComponent<AttachObjectManager>();
                List<GameObject> attachedList = attachObjects.getInteriorList();
                foreach (GameObject o in attachedList)
                {

                        DestroyObject(o);
                }
                attachObjects.clearInteriorObject();
            }

            layout = GameObject.Find("Mockup(client)");
            if (layout != null)
            {
                AttachObjectManager attachObjects = layout.GetComponent<AttachObjectManager>();
                List<GameObject> attachedList = attachObjects.getInteriorList();
                foreach (GameObject o in attachedList)
                {
                        DestroyObject(o);
                }
                attachObjects.clearInteriorObject();
            }
        }

    }

    [Command]
    void CmdDeleteObjects()
    {
        if (isServer)
        {
            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                AttachObjectManager attachObjects = layout.GetComponent<AttachObjectManager>();
                List<GameObject> attachedList = attachObjects.getExteriorList();
                foreach (GameObject o in attachedList)
                {
                        DestroyObject(o);
                }
                attachObjects.clearExteriorObject();
            }

            layout = GameObject.Find("Mockup(server)");
            if (layout != null)
            {
                AttachObjectManager attachObjects = layout.GetComponent<AttachObjectManager>();
                List<GameObject> attachedList = attachObjects.getExteriorList();
                foreach (GameObject o in attachedList)
                {
                        DestroyObject(o);
                    
                }
                attachObjects.clearExteriorObject();
            }
        }
    }
}
