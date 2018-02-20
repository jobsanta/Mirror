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

    void Start()
    {

        GameObject exterior_group = GameObject.Find("Exterior Anchor Group");
        GameObject interior_group = GameObject.Find("Interior Anchor Group");


        _extgroup = exterior_group.GetComponent<AnchorGroup>();
        _intgroup = interior_group.GetComponent<AnchorGroup>();

        _anchObj = GetComponent<AnchorableBehaviour>();

        Debug.Log("Name : " + gameObject.name + " tag: " + gameObject.tag + " isOIN " + LayoutController.isOwnInteriorView + " isbin " + LayoutController.isBillBoardInteriorView);
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


                _anchObj.OnAttachedToAnchor += onAttachedToAnchor;
                _anchObj.OnDetachedFromAnchor += onDetachedFromAnchor;
            }



            _anchObj.WhileAttachedToAnchor += whileAttachedToAnchor;

        }



        bc = GetComponent<BoxCollider>();

        rb = GetComponent<Rigidbody>();

    }

    void onAttachedToAnchor(AnchorableBehaviour anbobj, Anchor anchor)
    {
        Debug.Log(anbobj.anchor);
        AttachObjectManager attachObjectList = anchor.GetComponentInParent<AttachObjectManager>();
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

            GameObject layout = GameObject.Find("Mockup(billboard)");
            if (layout != null)
            {
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == anchor.name)
                    {
                        AnchorableBehaviour[] objs = new AnchorableBehaviour[1];
                        a.anchoredObjects.CopyTo(objs);

                        if(objs[0] != null)
                        {
                            objs[0].Detach();
                            layout.GetComponent<AttachObjectManager>().removeObject(objs[0].gameObject);
                            DestroyObject(objs[0].gameObject);
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
                        AnchorableBehaviour[] objs = new AnchorableBehaviour[1];
                        a.anchoredObjects.CopyTo(objs);

                        if (objs[0] != null)
                        {
                            objs[0].Detach();
                            layout.GetComponent<AttachObjectManager>().removeObject(objs[0].gameObject);
                            DestroyObject(objs[0].gameObject);
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
                    AnchorableBehaviour[] objs = new AnchorableBehaviour[1];
                    a.anchoredObjects.CopyTo(objs);
                    if (objs[0] != null)
                    {
                        objs[0].Detach();
                        layout.GetComponent<AttachObjectManager>().removeObject(objs[0].gameObject);
                        DestroyObject(objs[0].gameObject);
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
                    AnchorableBehaviour[] objs = new AnchorableBehaviour[1];
                    a.anchoredObjects.CopyTo(objs);
                    if (objs[0] != null)
                    {
                        objs[0].Detach();
                        layout.GetComponent<AttachObjectManager>().removeObject(objs[0].gameObject);
                        DestroyObject(objs[0].gameObject);
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
                        AnchorableBehaviour[] objs = new AnchorableBehaviour[1];
                        a.anchoredObjects.CopyTo(objs);
                        if (objs[0] != null)
                        {
                            objs[0].Detach();
                            layout.GetComponent<AttachObjectManager>().removeObject(objs[0].gameObject);
                            DestroyObject(objs[0].gameObject);
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
                        AnchorableBehaviour[] objs = new AnchorableBehaviour[1];
                        a.anchoredObjects.CopyTo(objs);
                        if (objs[0] != null)
                        {
                            objs[0].Detach();
                            layout.GetComponent<AttachObjectManager>().removeObject(objs[0].gameObject);
                            DestroyObject(objs[0].gameObject);
                        }

                    }
                }


            }
        }

    }

    void CreateCopyComponent(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation, string name)
    {
       // yield return new WaitForSeconds(3.0f);
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

                o.layer = 11;
                Anchor[] anchors = layout.GetComponentsInChildren<Anchor>();
                foreach (Anchor a in anchors)
                {
                    if (a.name == name)
                    {
                        o.GetComponent<AnchorableBehaviour>().anchor = a;
                        o.GetComponent<AnchorableBehaviour>().TryAttach(true);
                        layout.GetComponent<AttachObjectManager>().addObject(o);
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
                        layout.GetComponent<AttachObjectManager>().addObject(o);
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
                        layout.GetComponent<AttachObjectManager>().addObject(o);
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
                        layout.GetComponent<AttachObjectManager>().addObject(b);
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
                        layout.GetComponent<AttachObjectManager>().addObject(o);
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
                        layout.GetComponent<AttachObjectManager>().addObject(o);
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
