using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using UnityEngine.Networking;

[RequireComponent(typeof(AnchorableBehaviour))]
public class AnchorNetworkInteraction : NetworkBehaviour {

    private AnchorableBehaviour _anchObj;
    private Rigidbody rb;
    private BoxCollider bc;
    AnchorGroup _extgroup;
    AnchorGroup _intgroup;
    Dictionary<string, List<ConflictPair>> conflictDict;

    void Start() {

        GameObject exterior_group =  GameObject.Find("Exterior Anchor Group"); 
        GameObject interior_group =  GameObject.Find("Interior Anchor Group"); 


        _extgroup = exterior_group.GetComponent<AnchorGroup>();
        _intgroup = interior_group.GetComponent<AnchorGroup>();

        GameObject conflict = GameObject.Find("Conflict Dictionary");

        conflictDict = conflict.GetComponent<ConflictDictionarySync>().getConflictDictionary();

        _anchObj = GetComponent<AnchorableBehaviour>();
        if (tag == "Interior")
        {
            _anchObj.anchorGroup = _intgroup;
           // checkConflict(gameObject, gameObject.GetComponent<AnchorableBehaviour>().anchor.name, true, "Mockup(client)");
            if (!LayoutController.isOwnInteriorView)
            {   
                if(isServer && _anchObj.transform.position.z < 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false; ;
                        
                }
                else if(!isServer && _anchObj.transform.position.z > 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false;
                }
            }
        }
        else if (tag == "Exterior")
        {

            _anchObj.anchorGroup = _extgroup;
           // checkConflict(gameObject, gameObject.GetComponent<AnchorableBehaviour>().anchor.name, false, "Mockup(server)");
            if (LayoutController.isOwnInteriorView)
            {
                if (isServer && _anchObj.transform.position.z < 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false; 
                }
                else if (!isServer && _anchObj.transform.position.z > 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false;
                }
            }
        }


        if (_anchObj != null)
        {

            if (_anchObj.anchor == null)
            {


                _anchObj.OnAttachedToAnchor += onAttachedToAnchor;
                _anchObj.OnDetachedFromAnchor += onDetachedFromAnchor;
            }



            _anchObj.WhileAttachedToAnchor += whileAttachedToAnchor;

        }



        bc = GetComponent < BoxCollider>();

        rb = GetComponent<Rigidbody>();

    }

    void onAttachedToAnchor()
    {
        Anchor anchor = gameObject.GetComponent<AnchorableBehaviour>().anchor;
        AttachObjectManager attachObjectList =  anchor.GetComponentInParent<AttachObjectManager>();
        if (attachObjectList == null)
        {
            Debug.Log("no attach list");
        }
        else
        {
            if (gameObject.tag == "Exterior" || gameObject.tag == "BillboardEx")
            {
                attachObjectList.addExteriorObject(gameObject);

                if (isServer) checkConflict(gameObject, gameObject.GetComponent<AnchorableBehaviour>().anchor.name, false, "Mockup(server)");
                else checkConflict(gameObject, gameObject.GetComponent<AnchorableBehaviour>().anchor.name, false, "Mockup(client)");
            }
            else if (gameObject.tag == "Interior" || gameObject.tag == "BillboardInterior")
            {
                attachObjectList.addInteriorObject(gameObject);

                if (isServer) checkConflict(gameObject, gameObject.GetComponent<AnchorableBehaviour>().anchor.name, true, "Mockup(server)");
                else checkConflict(gameObject, gameObject.GetComponent<AnchorableBehaviour>().anchor.name, true, "Mockup(client)");
            }

        }

        CreateCopyComponent(gameObject, transform.position, transform.rotation, anchor.name);


    }

    void onDetachedFromAnchor()
    {
        Anchor anchor = gameObject.GetComponent<AnchorableBehaviour>().anchor;
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


        if (isServer)
        {
            GameObject layout = GameObject.Find("Mockup(client)");
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

                                RpcDeleteObject(objs[0].gameObject, "Mockup(client)");

                                NetworkServer.Destroy(objs[0].gameObject);
                            }
                        }

                    }
                }


            }
        }
        else
        {
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

                            RpcDeleteObject(objs[0].gameObject, "Mockup(server)");
                            NetworkServer.Destroy(objs[0].gameObject);
                        }
                    }

                }
            }


        }
    }

    [ClientRpc]
    void RpcDeleteObject(GameObject o, string layoutname)
    {
        GameObject layout = GameObject.Find(layoutname);
        if (layout != null)
        {

            if (o.tag == "Exterior" || o.tag == "BillboardEx")
                layout.GetComponent<AttachObjectManager>().removeExteriorObject(o);
            else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                layout.GetComponent<AttachObjectManager>().removeInteriorObject(o);

            


        }
    }

    void checkConflict(GameObject obj, string anchorname, bool isInterior, string Layoutname)
    {
        GameObject layout = GameObject.Find(Layoutname);
        Debug.Log(Layoutname);
        if (layout != null)
        {
            List<GameObject> attachList;

            if (isInterior) attachList = layout.GetComponent<AttachObjectManager>().getExteriorList();
            else attachList = layout.GetComponent<AttachObjectManager>().getInteriorList();


            //"Inner Component Capsule Async"
            string[] split = obj.name.Split('(');

            Debug.Log(split[0]);
            List<ConflictPair> conflicted;
            if (conflictDict.TryGetValue(split[0], out conflicted))
            {

                List<ConflictPair> possible_conflict = conflicted.FindAll(x => x.Conflict_in == anchorname);
                //("Anchor Point 1", "Exterior Anchor Point 1", "Exter Component Capsule Async"));
                //("Anchor Point 2", "Exterior Anchor Point 2", "Exter Component Capsule Async"));
                //("Anchor Point 3", "Exterior Anchor Point 3", "Exter Component Capsule Async"));
                //("Anchor Point 4", "Exterior Anchor Point 4", "Exter Component Capsule Async"));
                foreach (ConflictPair cp in possible_conflict)
                {
                    foreach (GameObject l in attachList)
                    {
                        string[] l_split = l.name.Split('(');
                        Debug.Log(l_split[0]);
                        if (cp.Conflict_name == l_split[0] && l.GetComponent<AnchorableBehaviour>().anchor.name == cp.Conflict_out)
                        {
                            Debug.Log("Conflict found at" + split[0] + "-" + cp.Conflict_in + "-" + cp.Conflict_name + "-" + cp.Conflict_out);
                        }
                    }
                }
            }
        }
    }

    void CreateCopyComponent(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation, string name)
    {

        if (isServer)
        {
            GameObject layout = GameObject.Find("Mockup(client)");
            if (layout != null)
            {
                spawnPosition.z = -spawnPosition.z;
                GameObject o = (GameObject)Instantiate(prefab, spawnPosition, spawnRotation);

                o.GetComponent<Rigidbody>().isKinematic = true;


                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                string anchorName ="";
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        anchorName = a.name;
                        o.GetComponent<AnchorableBehaviour>().anchor = a;
                        o.GetComponent<AnchorableBehaviour>().TryAttach(a);

                        if (o.tag == "Exterior" || o.tag == "BillboardEx")
                            layout.GetComponent<AttachObjectManager>().addExteriorObject(o);
                        else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                            layout.GetComponent<AttachObjectManager>().addInteriorObject(o);
                    }
                }
                NetworkServer.Spawn(o);
                RpcAttachObject(o.GetComponent<NetworkIdentity>().netId, anchorName, "Mockup(client)");
            }

        }
        else
        {
           CmdSpawnObject(prefab, spawnPosition, spawnRotation, name);

        }


    }

    
    [Command]
    void CmdSpawnObject(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation, string name)
    {
        GameObject layout = GameObject.Find("Mockup(server)");
        if (layout != null)
        {
            spawnPosition.z = -spawnPosition.z;
            GameObject o = (GameObject)Instantiate(prefab, spawnPosition, spawnRotation);
            o.GetComponent<Rigidbody>().isKinematic = true;
            Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
            foreach (Anchor a in anchors)
            {
                if (a.name == name)
                {
                    o.GetComponent<AnchorableBehaviour>().anchor = a;
                    o.GetComponent<AnchorableBehaviour>().TryAttach(a);
                    if (o.tag == "Exterior" || o.tag == "BillboardEx")
                        layout.GetComponent<AttachObjectManager>().addExteriorObject(o);
                    else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                        layout.GetComponent<AttachObjectManager>().addInteriorObject(o);
                }
            }
            NetworkServer.Spawn(o);
            RpcAttachObject(o.GetComponent<NetworkIdentity>().netId, name, "Mockup(server)");
        }
    }

    [ClientRpc]
    void RpcAttachObject(NetworkInstanceId identity, string name, string layoutname)
    {
        if(isServer)
            return;

        GameObject o = ClientScene.FindLocalObject(identity);
        if(o!=null)
        {
            GameObject layout = GameObject.Find(layoutname);
            if (layout != null)
            {

                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        o.GetComponent<AnchorableBehaviour>().anchor = a;
                        o.GetComponent<AnchorableBehaviour>().TryAttach(a);

                        if (o.tag == "Exterior" || o.tag == "BillboardEx")
                            layout.GetComponent<AttachObjectManager>().addExteriorObject(o);
                        else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                            layout.GetComponent<AttachObjectManager>().addInteriorObject(o);
                    }
                }
            }
        }
    }

    private void whileAttachedToAnchor()
    {
        // Anchor anchor = gameObject.GetComponent<AnchorableBehaviour>().anchor;
        // Debug.Log("Transform object");
        //Transform t = anchor.transform;
        Vector3 angles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Floor((angles.y)/90.0f)*90+30, 0));

       //if(!isServer)
       //  CmdAnchorMovement(t.position, t.rotation);
    }

    [Command]
    void CmdAnchorMovement(Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
    }


}
