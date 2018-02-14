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

    void Start() {

        GameObject exterior_group =  GameObject.Find("Exterior Anchor Group"); 
        GameObject interior_group =  GameObject.Find("Interior Anchor Group"); 


        _extgroup = exterior_group.GetComponent<AnchorGroup>();
        _intgroup = interior_group.GetComponent<AnchorGroup>();

        _anchObj = GetComponent<AnchorableBehaviour>();
        if (tag == "Interior")
        {
            _anchObj.anchorGroup = _intgroup;
            if(!LayoutController.isOwnInteriorView)
            {   
                if(isServer && _anchObj.transform.position.z < 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = false;
                }
                else if(!isServer && _anchObj.transform.position.z > 0)
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
                if (isServer && _anchObj.transform.position.z < 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = false;
                }
                else if (!isServer && _anchObj.transform.position.z > 0)
                {
                    _anchObj.gameObject.GetComponentInChildren<Renderer>().enabled = false;
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

    void onAttachedToAnchor(AnchorableBehaviour anbobj, Anchor anchor)
    {
        AttachObjectManager attachObjectList =  anchor.GetComponentInParent<AttachObjectManager>();
        if (attachObjectList == null)
        {
            Debug.Log("no attach list");
        }
        else
        {
            attachObjectList.addObject(gameObject);
        }

        CreateCopyComponent(gameObject, transform.position, transform.rotation, anchor.name);


    }

    void onDetachedFromAnchor(AnchorableBehaviour anbobj, Anchor anchor)
    {
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
                        AnchorableBehaviour[] objs = new AnchorableBehaviour[1];
                        a.anchoredObjects.CopyTo(objs);

                        objs[0].Detach();
                        layout.GetComponent<AttachObjectManager>().removeObject(objs[0].gameObject);
                        NetworkServer.Destroy(objs[0].gameObject);

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
                    AnchorableBehaviour[] objs = new AnchorableBehaviour[1];
                    a.anchoredObjects.CopyTo(objs);

                    objs[0].Detach();
                    layout.GetComponent<AttachObjectManager>().removeObject(objs[0].gameObject);
                    NetworkServer.Destroy(objs[0].gameObject);

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
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        o.GetComponent<AnchorableBehaviour>().anchor = a;
                        layout.GetComponent<AttachObjectManager>().addObject(o);
                    }
                }
                NetworkServer.Spawn(o);
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
                    layout.GetComponent<AttachObjectManager>().addObject(o);
                }
            }
            NetworkServer.Spawn(o);
        }
    }


    private void whileAttachedToAnchor(AnchorableBehaviour anbobj, Anchor anchor)
    {

        // Debug.Log("Transform object");
        Transform t = anchor.transform;

        CmdAnchorMovement(t.position, t.rotation);
    }

    [Command]
    void CmdAnchorMovement(Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
    }


}
