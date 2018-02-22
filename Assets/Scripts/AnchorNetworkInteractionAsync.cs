using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using UnityEngine.Networking;

[RequireComponent(typeof(AnchorableBehaviour))]
public class AnchorNetworkInteractionAsync : NetworkBehaviour
{

    private AnchorableBehaviour _anchObj;
    private Rigidbody rb;
    private BoxCollider bc;
    AnchorGroup _extgroup;
    AnchorGroup _intgroup;
    private IEnumerator createcoroutine;
    private IEnumerator deletecoroutine;
    private IEnumerator checkcoroutine;
    public static bool isPR;
    public static bool isHyBrid;

    Dictionary<string, string> relationDict;

    void Start()
    {

        GameObject relation = GameObject.Find("Relation Dictionary");

        relationDict = relation.GetComponent<RelationDictionary>().GetRelationship();

        isPR = LayoutController.globalPR;
        isHyBrid = LayoutController.globalHybrid;
        GameObject exterior_group = GameObject.Find("Exterior Anchor Group");
        GameObject interior_group = GameObject.Find("Interior Anchor Group");


        _extgroup = exterior_group.GetComponent<AnchorGroup>();
        _intgroup = interior_group.GetComponent<AnchorGroup>();

        _anchObj = GetComponent<AnchorableBehaviour>();

        //Debug.Log("Name : " + gameObject.name + " tag: " + gameObject.tag + " isOIN " + LayoutController.isOwnInteriorView + " isbin " + LayoutController.isBillBoardInteriorView);
        if (tag == "Interior")
        {
            _anchObj.anchorGroup = _intgroup;
            if (!LayoutController.isOwnInteriorView)
            {
                if (LayoutController.thisisServer && _anchObj.transform.position.z < 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = false;
                }
                else if (!LayoutController.thisisServer && _anchObj.transform.position.z > 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = false;
                }
            }
            else if (LayoutController.isOwnInteriorView)
            {
                if (LayoutController.thisisServer && _anchObj.transform.position.z < 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = true;
                }
                else if (!LayoutController.thisisServer && _anchObj.transform.position.z > 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = true;
                }
            }
        }
        else if (tag == "BillboardInterior")
        {
            if (LayoutController.isBillBoardInteriorView)
            {
                if (LayoutController.thisisServer && _anchObj.transform.position.z > 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = true;
                }
                else if (!LayoutController.thisisServer && _anchObj.transform.position.z < 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = true;
                }
            }
            else if (!LayoutController.isBillBoardInteriorView)
            {
                if (LayoutController.thisisServer && _anchObj.transform.position.z > 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = false;
                }
                else if (!LayoutController.thisisServer && _anchObj.transform.position.z < 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = false;
                }
            }
        }
        else if (tag == "Exterior")
        {
            _anchObj.anchorGroup = _extgroup;
            if (LayoutController.isOwnInteriorView)
            {
                if (LayoutController.thisisServer && _anchObj.transform.position.z < 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = false;
                }
                else if (!LayoutController.thisisServer && _anchObj.transform.position.z > 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = false;
                }
            }

            else if (!LayoutController.isOwnInteriorView)
            {
                if (LayoutController.thisisServer && _anchObj.transform.position.z < 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = true;
                }
                else if (!LayoutController.thisisServer && _anchObj.transform.position.z > 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = true;
                }
            }
        }
        else if (tag == "BillboardEx")
        {
            if (LayoutController.isBillBoardInteriorView)
            {
                if (LayoutController.thisisServer && _anchObj.transform.position.z > 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = false;
                }
                else if (!LayoutController.thisisServer && _anchObj.transform.position.z < 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = false;
                }
            }
            else if (!LayoutController.isBillBoardInteriorView)
            {
                if (LayoutController.thisisServer && _anchObj.transform.position.z > 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = true;
                }
                else if (!LayoutController.thisisServer && _anchObj.transform.position.z < 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = true;
                }
            }
        }
        
      


        if (_anchObj != null)
        {

            if (_anchObj.anchor == null)
            {


                _anchObj.OnAttachedToAnchor += OnAttachedToAnchor;
                _anchObj.OnDetachedFromAnchor += OnDetachedFromAnchor;
            }



            _anchObj.WhileAttachedToAnchor += whileAttachedToAnchor;

        }



        bc = GetComponent<BoxCollider>();

        rb = GetComponent<Rigidbody>();

    }

    void OnAttachedToAnchor(AnchorableBehaviour anbobj, Anchor anchor)
    {

        AttachObjectManager attachObjectList = anchor.GetComponentInParent<AttachObjectManager>();
        if (attachObjectList == null)
        {
            Debug.Log("no attach list");
        }
        else
        {
            if (gameObject.tag == "Exterior" || gameObject.tag == "BillboardEx")
                attachObjectList.addExteriorObject(gameObject);
            else if (gameObject.tag == "Interior" || gameObject.tag == "BillboardInterior")
                attachObjectList.addInteriorObject(gameObject);
        }

        //if(isPR)
        //{
        //    createcoroutine = DelayCopyComponent(gameObject, transform.position, transform.rotation, anchor.name);
        //    StartCoroutine(createcoroutine);
        //}
        //else
        //{
        //    //CreateCopyComponent(gameObject, transform.position, transform.rotation, anchor.name);
        //}



    }

    void OnDetachedFromAnchor(AnchorableBehaviour anbobj, Anchor anchor)
    {
        //if (isPR)
        //    StopCoroutine(createcoroutine);


        AttachObjectManager attachObjectList = anchor.GetComponentInParent<AttachObjectManager>();
        if (attachObjectList == null)
        {
            Debug.Log("no attach list");
        }
        else
        {
            if (gameObject.tag == "Exterior" || gameObject.tag == "BillboardEx")
                attachObjectList.removeExteriorObject(gameObject);
            else if (gameObject.tag == "Interior" || gameObject.tag == "BillboardInterior")
                attachObjectList.removeInteriorObject(gameObject);
        }

        //if(isPR)
        //{
        //    deletecoroutine = DelayDeleteCopyComponent(anchor);
        //    StartCoroutine(deletecoroutine);
        //}
        
    }

    public void OnGraspBeginCheck(GameObject obj)
    {

        if (!isHyBrid)
            return;

        string name = obj.name.Remove(obj.name.Length - 7);
        string related;
        bool orignalfound = false;
        Anchor originalAnchor = null;

        if (relationDict.TryGetValue(name, out related))
        {

            //get previos anchor if any
            GameObject layout;
            if (isServer) layout = GameObject.Find("Mockup(server)");
            else layout = GameObject.Find("Mockup(client)");
            if (layout != null)
            {

                AttachObjectManager attachObject = layout.GetComponent<AttachObjectManager>();

                List<GameObject> attachlist;
                //switch because check another 
                if (isServer) attachlist = attachObject.getExteriorList();
                else attachlist = attachObject.getInteriorList();


                foreach (GameObject o in attachlist)
                {
                    if (o.name == string.Format("{0}(Clone)(Clone)", related))
                    {
                        orignalfound = true;
                        originalAnchor = o.GetComponent<AnchorableBehaviour>().anchor;
                    }
                }
                
            }

            if(orignalfound)
            {
                if (isServer) RpcCheckRelation(related,originalAnchor.name);
                else CmdCheckRelation(related, originalAnchor.name);
            }
            else
            {
                if (isServer) RpcCheckRelation(related, null);
                else CmdCheckRelation(related, null);
            }

        }
    }

    [Command]
    public void CmdCheckRelation(string name, string anchorname)
    {
        if (!isServer)
            return;

        GameObject layout = GameObject.Find("Mockup(server)");
        if (layout != null)
        {
            List<GameObject> list = layout.GetComponent<AttachObjectManager>().getInteriorList();
            bool found = false;
            foreach (GameObject o in list)
            {
                if (o.name == string.Format("{0}(Clone)", name))
                {
                    found = true;
                    //if found any in the list update information
                    if(anchorname != null)
                    {
                        Debug.Log("emg transfer objects " + anchorname);
                        o.GetComponent<AnchorNetworkInteractionAsync>().DeleteCopyComponent(anchorname);
                    }
                    Debug.Log("emg clone objects");
                    o.GetComponent<AnchorNetworkInteractionAsync>().CreateCopyComponent(o, o.transform.position,
                     o.transform.rotation, o.GetComponent<AnchorableBehaviour>().anchor.name);
                }
            }

            if(!found)
            {
                GameObject relatedObject = GameObject.Find(string.Format("{0}(Clone)", name));
                if(relatedObject !=null)
                {
                    Debug.Log("emg delete objects");
                    if(relatedObject.GetComponent<AnchorableBehaviour>().anchor !=null)
                    relatedObject.GetComponent<AnchorNetworkInteractionAsync>().DeleteCopyComponent(relatedObject.GetComponent<AnchorableBehaviour>().anchor);
                }
            }


        }
    }

    [ClientRpc]
    public void RpcCheckRelation(string name, string anchorname)
    {
        if (isServer)
            return;

        GameObject layout = GameObject.Find("Mockup(client)");
        if (layout != null)
        {
            List<GameObject> list = layout.GetComponent<AttachObjectManager>().getExteriorList();
            bool found = false;
            foreach (GameObject o in list)
            {
                if (o.name == string.Format("{0}(Clone)", name))
                {
                    found = true;
                    //if found any in the list update information
                    if (anchorname != null)
                    {
                        Debug.Log("emg transfer objects " + anchorname);
                        o.GetComponent<AnchorNetworkInteractionAsync>().DeleteCopyComponent(anchorname);
                    }
                    Debug.Log("emg clone objects");
                    o.GetComponent<AnchorNetworkInteractionAsync>().CreateCopyComponent(o, o.transform.position,
                         o.transform.rotation, o.GetComponent<AnchorableBehaviour>().anchor.name);
                }
            }

            if (!found)
            {
                GameObject relatedObject = GameObject.Find(string.Format("{0}(Clone)", name));
                if (relatedObject != null)
                {
                    if(relatedObject.GetComponent<AnchorableBehaviour>().anchor != null)
                    {
                        Debug.Log("emg delete objects");
                        if (relatedObject.GetComponent<AnchorableBehaviour>().anchor != null)
                            relatedObject.GetComponent<AnchorNetworkInteractionAsync>().DeleteCopyComponent(relatedObject.GetComponent<AnchorableBehaviour>().anchor);
                    }
                   
                }
            }

        }
    }

    public void OnGraspEndCheckAnchor(GameObject obj)
    {
        if (isPR)
        {
            checkcoroutine = DelayCheckAnchor(obj);
            StartCoroutine(checkcoroutine);
        }

    }

    IEnumerator DelayCheckAnchor(GameObject obj)
    {
        yield return new WaitForSeconds(5.0f);
        GameObject layout;

        bool orignalfound = false;
        bool billboardfound = false;
        Anchor originalAnchor = null;
        Anchor billboardAnchor=null;

        if (isServer) layout = GameObject.Find("Mockup(server)");
        else layout = GameObject.Find("Mockup(client)");

        if (layout != null)
        {
            AttachObjectManager attachObject =  layout.GetComponent<AttachObjectManager>();

            List<GameObject> attachlist;
            if (isServer) attachlist = attachObject.getInteriorList();
            else  attachlist = attachObject.getExteriorList();


            foreach (GameObject o in attachlist)
            {
                if(o.Equals(obj))
                {
                    orignalfound = true;
                    originalAnchor = o.GetComponent<AnchorableBehaviour>().anchor;
                }
            }
        }

        layout = GameObject.Find("Mockup(billboard)");
        if (layout != null)
        {
            AttachObjectManager attachObject = layout.GetComponent<AttachObjectManager>();

            List<GameObject> attachlist;
            if (isServer) attachlist = attachObject.getInteriorList();
            else attachlist = attachObject.getExteriorList();
            foreach (GameObject o in attachlist)
            {
                if (o.name == string.Format("{0}(Clone)", obj.name))
                {
                    billboardfound = true;
                    billboardAnchor = o.GetComponent<AnchorableBehaviour>().anchor;
                }
            }
        }

        if (orignalfound == false && billboardfound == true)
        {
            //remove in original remove objects in the copy
            Debug.Log("Delete object");
            DeleteCopyComponent(obj.GetComponent<AnchorableBehaviour>().anchor);
        }
        else if (orignalfound == true && billboardfound == false)
        {
            //add in original add objects to the copy
            Debug.Log("Add object");
            CreateCopyComponent(obj, obj.transform.position, obj.transform.rotation, obj.GetComponent<AnchorableBehaviour>().anchor.name);
        }
        else if ((orignalfound == true && billboardfound == true) )
        {
            if(originalAnchor.name != billboardAnchor.name)
            {
                Debug.Log("Transfer");
                DeleteCopyComponent(billboardAnchor);
                CreateCopyComponent(obj, obj.transform.position, obj.transform.rotation, obj.GetComponent<AnchorableBehaviour>().anchor.name);
            }
        }

    }

    public void DeleteCopyComponent(Anchor anchor)
    {
        if (isServer)
        {

            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == anchor.name)
                    {
                        if (a.anchoredObjects.Count > 0)
                        {
                            AnchorableBehaviour[] objs = new AnchorableBehaviour[a.anchoredObjects.Count];
                            a.anchoredObjects.CopyTo(objs);

                            if (objs[0] != null)
                            {
                                objs[0].Detach();

                                if (objs[0].gameObject.tag == "Exterior" || objs[0].gameObject.tag == "BillboardEx")
                                    layout.GetComponent<AttachObjectManager>().removeExteriorObject(objs[0].gameObject);
                                else if (objs[0].gameObject.tag == "Interior" || objs[0].gameObject.tag == "BillboardInterior")
                                    layout.GetComponent<AttachObjectManager>().removeInteriorObject(objs[0].gameObject);

                                DestroyObject(objs[0].gameObject);
                            }
                        }
                    }
                }

            }
            RpcDeleteObject(anchor.name);
        }
        else
        {
            //Destroy client bill board objects
            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == anchor.name)
                    {
                        if (a.anchoredObjects.Count > 0)
                        {
                            AnchorableBehaviour[] objs = new AnchorableBehaviour[a.anchoredObjects.Count];
                            a.anchoredObjects.CopyTo(objs);

                            if (objs[0] != null)
                            {
                                objs[0].Detach();

                                if (objs[0].gameObject.tag == "Exterior" || objs[0].gameObject.tag == "BillboardEx")
                                    layout.GetComponent<AttachObjectManager>().removeExteriorObject(objs[0].gameObject);
                                else if (objs[0].gameObject.tag == "Interior" || objs[0].gameObject.tag == "BillboardInterior")
                                    layout.GetComponent<AttachObjectManager>().removeInteriorObject(objs[0].gameObject);

                                DestroyObject(objs[0].gameObject);
                            }
                        }
                    }
                }


            }
            //Destroy server and server billboard object
            CmdDeleteObject(anchor.name);
        }


    }

    public void DeleteCopyComponent(string anchor)
    {
        if (isServer)
        {

            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == anchor)
                    {
                        if (a.anchoredObjects.Count > 0)
                        {
                            AnchorableBehaviour[] objs = new AnchorableBehaviour[a.anchoredObjects.Count];
                            a.anchoredObjects.CopyTo(objs);

                            if (objs[0] != null)
                            {
                                objs[0].Detach();

                                if (objs[0].gameObject.tag == "Exterior" || objs[0].gameObject.tag == "BillboardEx")
                                    layout.GetComponent<AttachObjectManager>().removeExteriorObject(objs[0].gameObject);
                                else if (objs[0].gameObject.tag == "Interior" || objs[0].gameObject.tag == "BillboardInterior")
                                    layout.GetComponent<AttachObjectManager>().removeInteriorObject(objs[0].gameObject);

                                DestroyObject(objs[0].gameObject);
                            }
                        }
                    }
                }

            }
            RpcDeleteObject(anchor);
        }
        else
        {
            //Destroy client bill board objects
            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == anchor)
                    {
                        if (a.anchoredObjects.Count > 0)
                        {
                            AnchorableBehaviour[] objs = new AnchorableBehaviour[a.anchoredObjects.Count];
                            a.anchoredObjects.CopyTo(objs);

                            if (objs[0] != null)
                            {
                                objs[0].Detach();

                                if (objs[0].gameObject.tag == "Exterior" || objs[0].gameObject.tag == "BillboardEx")
                                    layout.GetComponent<AttachObjectManager>().removeExteriorObject(objs[0].gameObject);
                                else if (objs[0].gameObject.tag == "Interior" || objs[0].gameObject.tag == "BillboardInterior")
                                    layout.GetComponent<AttachObjectManager>().removeInteriorObject(objs[0].gameObject);

                                DestroyObject(objs[0].gameObject);
                            }
                        }
                    }
                }


            }
            //Destroy server and server billboard object
            CmdDeleteObject(anchor);
        }

    }

    IEnumerator DelayDeleteCopyComponent(Anchor anchor)
    {

        yield return new WaitForSeconds(3.0f);
        if (isServer)
        {

            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == anchor.name)
                    {
                        if (a.anchoredObjects.Count > 0)
                        {
                            AnchorableBehaviour[] objs = new AnchorableBehaviour[a.anchoredObjects.Count];
                            a.anchoredObjects.CopyTo(objs);

                            if (objs[0] != null)
                            {
                                objs[0].Detach();

                                if (objs[0].gameObject.tag == "Exterior" || objs[0].gameObject.tag == "BillboardEx")
                                    layout.GetComponent<AttachObjectManager>().removeExteriorObject(objs[0].gameObject);
                                else if (objs[0].gameObject.tag == "Interior" || objs[0].gameObject.tag == "BillboardInterior")
                                    layout.GetComponent<AttachObjectManager>().removeInteriorObject(objs[0].gameObject);

                                DestroyObject(objs[0].gameObject);
                            }
                        }
                    }
                }

            }
            RpcDeleteObject(anchor.name);
        }
        else
        {
            //Destroy client bill board objects
            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == anchor.name)
                    {
                        if (a.anchoredObjects.Count > 0)
                        {
                            AnchorableBehaviour[] objs = new AnchorableBehaviour[a.anchoredObjects.Count];
                            a.anchoredObjects.CopyTo(objs);

                            if (objs[0] != null)
                            {
                                objs[0].Detach();

                                if (objs[0].gameObject.tag == "Exterior" || objs[0].gameObject.tag == "BillboardEx")
                                    layout.GetComponent<AttachObjectManager>().removeExteriorObject(objs[0].gameObject);
                                else if (objs[0].gameObject.tag == "Interior" || objs[0].gameObject.tag == "BillboardInterior")
                                    layout.GetComponent<AttachObjectManager>().removeInteriorObject(objs[0].gameObject);

                                DestroyObject(objs[0].gameObject);
                            }
                        }
                    }
                }


            }
            //Destroy server and server billboard object
            CmdDeleteObject(anchor.name);
        }

    }

    
    [Command]
    void CmdDeleteObject(string name)
    {
        GameObject layout = GameObject.Find("Mockup(server)");
        if (layout != null)
        {
            Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
            foreach (Anchor a in anchors)
            {
                if (a.name == name)
                {
                    if (a.anchoredObjects.Count > 0)
                    {
                        AnchorableBehaviour[] objs = new AnchorableBehaviour[a.anchoredObjects.Count];
                        a.anchoredObjects.CopyTo(objs);
                        if (objs[0] != null)
                        {
                            objs[0].Detach();
                            if (objs[0].gameObject.tag == "Exterior" || objs[0].gameObject.tag == "BillboardEx")
                                layout.GetComponent<AttachObjectManager>().removeExteriorObject(objs[0].gameObject);
                            else if (objs[0].gameObject.tag == "Interior" || objs[0].gameObject.tag == "BillboardInterior")
                                layout.GetComponent<AttachObjectManager>().removeInteriorObject(objs[0].gameObject);

                            DestroyObject(objs[0].gameObject);
                        }
                    }

                }
            }


        }

        layout = GameObject.Find("Mockup(billboard)");
        if (layout != null)
        {
            Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
            foreach (Anchor a in anchors)
            {
                if (a.name == name)
                {
                    if (a.anchoredObjects.Count > 0)
                    {
                        AnchorableBehaviour[] objs = new AnchorableBehaviour[a.anchoredObjects.Count];
                        a.anchoredObjects.CopyTo(objs);
                        if (objs[0] != null)
                        {
                            objs[0].Detach();
                            objs[0].Detach();
                            if (objs[0].gameObject.tag == "Exterior" || objs[0].gameObject.tag == "BillboardEx")
                                layout.GetComponent<AttachObjectManager>().removeExteriorObject(objs[0].gameObject);
                            else if (objs[0].gameObject.tag == "Interior" || objs[0].gameObject.tag == "BillboardInterior")
                                layout.GetComponent<AttachObjectManager>().removeInteriorObject(objs[0].gameObject);
                            DestroyObject(objs[0].gameObject);
                        }
                    }

                }
            }


        }
    }
    [ClientRpc]
    void RpcDeleteObject(string name)
    {
        if(!isServer)
        {
            GameObject layout = GameObject.Find("Mockup(client)");
            if (layout != null)
            {
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        if (a.anchoredObjects.Count > 0)
                        {
                            AnchorableBehaviour[] objs = new AnchorableBehaviour[a.anchoredObjects.Count];
                            a.anchoredObjects.CopyTo(objs);
                            if (objs[0] != null)
                            {
                                objs[0].Detach();
                                if (objs[0].gameObject.tag == "Exterior" || objs[0].gameObject.tag == "BillboardEx")
                                    layout.GetComponent<AttachObjectManager>().removeExteriorObject(objs[0].gameObject);
                                else if (objs[0].gameObject.tag == "Interior" || objs[0].gameObject.tag == "BillboardInterior")
                                    layout.GetComponent<AttachObjectManager>().removeInteriorObject(objs[0].gameObject);
                                DestroyObject(objs[0].gameObject);
                            }
                        }

                    }
                }


            }

            layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        if (a.anchoredObjects.Count > 0)
                        {
                            AnchorableBehaviour[] objs = new AnchorableBehaviour[a.anchoredObjects.Count];
                            a.anchoredObjects.CopyTo(objs);
                            if (objs[0] != null)
                            {
                                objs[0].Detach();
                                if (objs[0].gameObject.tag == "Exterior" || objs[0].gameObject.tag == "BillboardEx")
                                    layout.GetComponent<AttachObjectManager>().removeExteriorObject(objs[0].gameObject);
                                else if (objs[0].gameObject.tag == "Interior" || objs[0].gameObject.tag == "BillboardInterior")
                                    layout.GetComponent<AttachObjectManager>().removeInteriorObject(objs[0].gameObject);
                                DestroyObject(objs[0].gameObject);
                            }
                        }

                    }
                }


            }
        }

    }

    public void CreateCopyComponent(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation, string name)
    {
        if (isServer)
        {
            //Spawn object in client
            spawnPosition.z = -spawnPosition.z;
            RpcSetSpawnObject(prefab, name,spawnPosition,spawnRotation);
            
            //Spawn object in server for billboard
            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                //spawnPosition.z = -spawnPosition.z;
                GameObject o = (GameObject)Instantiate(prefab, spawnPosition, spawnRotation);
                o.GetComponent<BoxCollider>().enabled = false;
                o.GetComponent<Rigidbody>().isKinematic = true;
                if (o.tag == "Exterior")
                    o.tag = "BillboardEx";
                else if (o.tag == "Interior")
                    o.tag = "BillboardInterior";

               
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        o.GetComponent<AnchorableBehaviour>().anchor = a;
                        o.GetComponent<AnchorableBehaviour>().TryAttach(true);

                        if (o.tag == "Exterior" || o.tag == "BillboardEx")
                            layout.GetComponent<AttachObjectManager>().addExteriorObject(o);
                        else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                            layout.GetComponent<AttachObjectManager>().addInteriorObject(o);
                    }
                }
            }

        }
        else
        {
            //spawn object in client billboard
            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                spawnPosition.z = -spawnPosition.z;
                GameObject o = (GameObject)Instantiate(prefab, spawnPosition, spawnRotation);
                o.GetComponent<BoxCollider>().enabled = false;
                o.GetComponent<Rigidbody>().isKinematic = true;
                if (o.tag == "Exterior")
                    o.tag = "BillboardEx";
                else if (o.tag == "Interior")
                    o.tag = "BillboardInterior";

                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        o.GetComponent<AnchorableBehaviour>().anchor = a;
                        o.GetComponent<AnchorableBehaviour>().TryAttach(true);
                        if (o.tag == "Exterior" || o.tag == "BillboardEx")
                            layout.GetComponent<AttachObjectManager>().addExteriorObject(o);
                        else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                            layout.GetComponent<AttachObjectManager>().addInteriorObject(o);
                    }
                }
            }
            //spawn object in server
            CmdSpawnObject(prefab, spawnPosition, spawnRotation, name);
        }


    }

    IEnumerator DelayCopyComponent(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation, string name)
    {
         yield return new WaitForSeconds(3.0f);
        if (isServer)
        {
            //Spawn object in client
            spawnPosition.z = -spawnPosition.z;
            RpcSetSpawnObject(prefab, name, spawnPosition, spawnRotation);

            //Spawn object in server for billboard
            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                //spawnPosition.z = -spawnPosition.z;
                GameObject o = (GameObject)Instantiate(prefab, spawnPosition, spawnRotation);
                o.GetComponent<BoxCollider>().enabled = false;
                o.GetComponent<Rigidbody>().isKinematic = true;
                if (o.tag == "Exterior")
                    o.tag = "BillboardEx";
                else if (o.tag == "Interior")
                    o.tag = "BillboardInterior";

                o.layer = 11;
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        o.GetComponent<AnchorableBehaviour>().anchor = a;
                        o.GetComponent<AnchorableBehaviour>().TryAttach(true);
                        if (o.tag == "Exterior" || o.tag == "BillboardEx")
                            layout.GetComponent<AttachObjectManager>().addExteriorObject(o);
                        else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                            layout.GetComponent<AttachObjectManager>().addInteriorObject(o);
                    }
                }
            }

        }
        else
        {
            //spawn object in client billboard
            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                spawnPosition.z = -spawnPosition.z;
                GameObject o = (GameObject)Instantiate(prefab, spawnPosition, spawnRotation);
                o.GetComponent<BoxCollider>().enabled = false;
                o.GetComponent<Rigidbody>().isKinematic = true;
                o.name = o.name + "billboard";
                if (o.tag == "Exterior")
                    o.tag = "BillboardEx";
                else if (o.tag == "Interior")
                    o.tag = "BillboardInterior";

                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        o.GetComponent<AnchorableBehaviour>().anchor = a;
                        o.GetComponent<AnchorableBehaviour>().TryAttach(true);
                        if (o.tag == "Exterior" || o.tag == "BillboardEx")
                            layout.GetComponent<AttachObjectManager>().addExteriorObject(o);
                        else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                            layout.GetComponent<AttachObjectManager>().addInteriorObject(o);
                    }
                }
            }
            //spawn object in server
            CmdSpawnObject(prefab, spawnPosition, spawnRotation, name);
        }


    }

    [ClientRpc]
    void RpcSetSpawnObject(GameObject prefab, string anchorname, Vector3 spawnPosition, Quaternion spawnRotation)
    {
      
        if(!isServer)
        {
            //spawn object on client side and attach to client layout


            GameObject layout = GameObject.Find("Mockup(client)");
            GameObject o = (GameObject)Instantiate(prefab, spawnPosition, spawnRotation);
            if (layout != null)
            {
                o.GetComponent<BoxCollider>().enabled = false;
                o.GetComponent<Rigidbody>().isKinematic = true;
                


                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == anchorname)
                    {
                        o.GetComponent<AnchorableBehaviour>().anchor = a;
                        o.GetComponent<AnchorableBehaviour>().TryAttach(true);
                        if (o.tag == "Exterior" || o.tag == "BillboardEx")
                            layout.GetComponent<AttachObjectManager>().addExteriorObject(o);
                        else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                            layout.GetComponent<AttachObjectManager>().addInteriorObject(o);
                    }
                }
            }

            //spawn object on client side and attach to client billboard layout
            layout = GameObject.Find("Mockup(billboard)");
            spawnPosition.z = -spawnPosition.z;
            GameObject b = (GameObject)Instantiate(o, spawnPosition, spawnRotation);
            if (layout != null)
            {
                
                b.GetComponent<BoxCollider>().enabled = false;
                b.GetComponent<Rigidbody>().isKinematic = true;
                b.name = b.name + "billboard";
                if (o.tag == "Exterior")
                    b.tag = "BillboardEx";
                else if (o.tag == "Interior")
                    b.tag = "BillboardInterior";

                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == anchorname)
                    {
                        b.GetComponent<AnchorableBehaviour>().anchor = a;
                        // b.GetComponent<AnchorableBehaviour>().TryAttach(true);
                        if (o.tag == "Exterior" || o.tag == "BillboardEx")
                            layout.GetComponent<AttachObjectManager>().addExteriorObject(b);
                        else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                            layout.GetComponent<AttachObjectManager>().addInteriorObject(b);
                    }
                }
            }

        }
    }

    [Command]
    void CmdSpawnObject(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation, string name)
    {
        if(isServer)
        {
            //spawn object in the server
            GameObject layout = GameObject.Find("Mockup(server)");
            if (layout != null)
            {
                if(spawnPosition.z >0)
                spawnPosition.z = -spawnPosition.z;

                GameObject o = (GameObject)Instantiate(prefab, spawnPosition, spawnRotation);
                o.GetComponent<BoxCollider>().enabled = false;
                o.GetComponent<Rigidbody>().isKinematic = true;
              
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        o.GetComponent<AnchorableBehaviour>().anchor = a;
                        o.GetComponent<AnchorableBehaviour>().TryAttach(true);
                        if (o.tag == "Exterior" || o.tag == "BillboardEx")
                            layout.GetComponent<AttachObjectManager>().addExteriorObject(o);
                        else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                            layout.GetComponent<AttachObjectManager>().addInteriorObject(o);
                    }
                }
            }

            //spawn object in server billboard
            layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                if(spawnPosition.z < 0)
                spawnPosition.z = -spawnPosition.z;

                GameObject o = (GameObject)Instantiate(prefab, spawnPosition, spawnRotation);
                o.GetComponent<BoxCollider>().enabled = false;
                o.GetComponent<Rigidbody>().isKinematic = true;
                if (o.tag == "Exterior")
                    o.tag = "BillboardEx";
                else if (o.tag == "Interior")
                    o.tag = "BillboardInterior";
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        o.GetComponent<AnchorableBehaviour>().anchor = a;
                        o.GetComponent<AnchorableBehaviour>().TryAttach(true);
                        if (o.tag == "Exterior" || o.tag == "BillboardEx")
                            layout.GetComponent<AttachObjectManager>().addExteriorObject(o);
                        else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                            layout.GetComponent<AttachObjectManager>().addInteriorObject(o);
                    }
                }
            }
        }

    }


    private void whileAttachedToAnchor(AnchorableBehaviour anbobj, Anchor anchor)
    {

        // Debug.Log("Transform object");
        Transform t = anchor.transform;

      //  CmdAnchorMovement(t.position, t.rotation);
    }

    [Command]
    void CmdAnchorMovement(Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
    }


}
