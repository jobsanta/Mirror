using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using UnityEngine.UI;
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

        if (!LayoutController.isOwnInteriorView)
        {
            if (tag == "Interior")
            {
                _anchObj.anchorGroup = _intgroup;
                if (isServer && _anchObj.transform.position.z < 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false; ;

                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = false; ;

                }
                else if (!isServer && _anchObj.transform.position.z > 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false;

                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = false; ;
                }
            }
            else if (tag == "Exterior")
            {
                _anchObj.anchorGroup = _extgroup;
                // checkConflict(gameObject, gameObject.GetComponent<AnchorableBehaviour>().anchor.name, false, "Mockup(server)");
                if (isServer && _anchObj.transform.position.z < 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = true;

                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = true; ;
                }
                else if (!isServer && _anchObj.transform.position.z > 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = true;

                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = true; 
                }
            }
        }
        else
        {
            if (tag == "Interior")
            {
                _anchObj.anchorGroup = _intgroup;
                if (isServer && _anchObj.transform.position.z < 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = true;

                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = true; 
                }
                else if (!isServer && _anchObj.transform.position.z > 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = true;

                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = true; 
                }
            }
            else if (tag == "Exterior")
            {
                _anchObj.anchorGroup = _extgroup;
                // checkConflict(gameObject, gameObject.GetComponent<AnchorableBehaviour>().anchor.name, false, "Mockup(server)");
                if (isServer && _anchObj.transform.position.z < 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false;

                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = false; 
                }
                else if (!isServer && _anchObj.transform.position.z > 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false;

                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = false; ;
                }
            }
        }


        if (!LayoutController.isTheirInteriorView)
        {
            if (tag == "Interior")
            {
                _anchObj.anchorGroup = _intgroup;
                if (isServer && _anchObj.transform.position.z > 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false;
                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = false; 

                }
                else if (!isServer && _anchObj.transform.position.z < 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false;
                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = false; 
                }
            }
            else if (tag == "Exterior")
            {
                _anchObj.anchorGroup = _extgroup;
                // checkConflict(gameObject, gameObject.GetComponent<AnchorableBehaviour>().anchor.name, false, "Mockup(server)");
                if (isServer && _anchObj.transform.position.z > 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = true;
                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = true;
                }
                else if (!isServer && _anchObj.transform.position.z < 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = true;
                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = true;
                }
            }
        }
        else
        {
            if (tag == "Interior")
            {
                _anchObj.anchorGroup = _intgroup;
                if (isServer && _anchObj.transform.position.z > 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = true;
                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = true;

                }
                else if (!isServer && _anchObj.transform.position.z < 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = true;
                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = true;
                }
            }
            else if (tag == "Exterior")
            {
                _anchObj.anchorGroup = _extgroup;
                // checkConflict(gameObject, gameObject.GetComponent<AnchorableBehaviour>().anchor.name, false, "Mockup(server)");
                if (isServer && _anchObj.transform.position.z > 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false;
                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = false;
                }
                else if (!isServer && _anchObj.transform.position.z < 0)
                {
                    Renderer[] rs = _anchObj.gameObject.GetComponentsInChildren<Renderer>();
                    for (int i = 0; i < rs.Length; i++) rs[i].enabled = false;
                    Text[] ts = _anchObj.gameObject.GetComponentsInChildren<Text>();
                    for (int i = 0; i < ts.Length; i++) ts[i].enabled = false;
                }
            }
        }



        // checkConflict(gameObject, gameObject.GetComponent<AnchorableBehaviour>().anchor.name, true, "Mockup(client)");




        if (_anchObj != null)
        {

            if((gameObject.tag == "Exterior" && !isServer) || (gameObject.tag == "Interior" && isServer))
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
        anchor.gameObject.GetComponentInChildren<Renderer>().enabled = false;
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
                Debug.Log("cmd1");
                CmdAttachObject(gameObject.GetComponent<NetworkIdentity>().netId, anchor.name, "Mockup(client)");


            }
            else if (gameObject.tag == "Interior" || gameObject.tag == "BillboardInterior")
            {
                attachObjectList.addInteriorObject(gameObject);
                Debug.Log("rpc1");
                RpcAttachObject(gameObject.GetComponent<NetworkIdentity>().netId, anchor.name, "Mockup(server)");
            }

        }

        CreateCopyComponent(gameObject, transform.position, transform.rotation, anchor.name);


    }

    void onDetachedFromAnchor()
    {
        Anchor anchor = gameObject.GetComponent<AnchorableBehaviour>().anchor;
        anchor.gameObject.GetComponentInChildren<Renderer>().enabled = true;
        AttachObjectManager attachObjectList = anchor.GetComponentInParent<AttachObjectManager>();
        if (attachObjectList == null)
        {
            Debug.Log("no attach list");
        }
        else
        {
            if (gameObject.tag == "Exterior" || gameObject.tag == "BillboardEx")
            {
                attachObjectList.removeExteriorObject(gameObject);

                Debug.Log("cmd2");
                CmdDetachObject(gameObject.GetComponent<NetworkIdentity>().netId, "Mockup(client)");
            }

            else if (gameObject.tag == "Interior" || gameObject.tag == "BillboardInterior")
            {
                attachObjectList.removeInteriorObject(gameObject);
                Debug.Log("rpc2");
                RpcDetachObject(gameObject.GetComponent<NetworkIdentity>().netId, "Mockup(server)");
            }
                
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
                                RpcDetachObject(objs[0].gameObject.GetComponent<NetworkIdentity>().netId, "Mockup(client)");
                                objs[0].Detach();

                                if (objs[0].gameObject.tag == "Exterior" || objs[0].gameObject.tag == "BillboardEx")
                                    layout.GetComponent<AttachObjectManager>().removeExteriorObject(objs[0].gameObject);
                                else if (objs[0].gameObject.tag == "Interior" || objs[0].gameObject.tag == "BillboardInterior")
                                    layout.GetComponent<AttachObjectManager>().removeInteriorObject(objs[0].gameObject);
                                Debug.Log("rpc3");
                               

                                NetworkServer.Destroy(objs[0].gameObject);
                            }
                        }

                    }
                }


            }
        }
        else
        {
            GameObject layout = GameObject.Find("Mockup(server)");
            if (layout != null)
            {

                if (gameObject.tag == "Exterior" || gameObject.tag == "BillboardEx")
                    layout.GetComponent<AttachObjectManager>().removeExteriorObject(gameObject);
                else if (gameObject.tag == "Interior" || gameObject.tag == "BillboardInterior")
                    layout.GetComponent<AttachObjectManager>().removeInteriorObject(gameObject);

            


            }
            Debug.Log("cmd3");
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

                            NetworkServer.Destroy(objs[0].gameObject);
                        }
                    }

                }
            }


        }
    }

    [ClientRpc]
    void RpcDetachObject(NetworkInstanceId netid, string layoutname)
    {

        GameObject o = ClientScene.FindLocalObject(netid);
        GameObject layout = GameObject.Find(layoutname);
        if (layout != null && o!=null)
        {

            if (o.tag == "Exterior" || o.tag == "BillboardEx")
                layout.GetComponent<AttachObjectManager>().removeExteriorObject(o);
            else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                layout.GetComponent<AttachObjectManager>().removeInteriorObject(o);

            


        }
    }

    [Command]
    void CmdDetachObject(NetworkInstanceId netid, string layoutname)
    {

        GameObject o = ClientScene.FindLocalObject(netid);
        GameObject layout = GameObject.Find(layoutname);
        if (layout != null && o!=null)
        {

            if (o.tag == "Exterior" || o.tag == "BillboardEx")
                layout.GetComponent<AttachObjectManager>().removeExteriorObject(o);
            else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                layout.GetComponent<AttachObjectManager>().removeInteriorObject(o);




        }
    }

    void CreateCopyComponent(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation, string name)
    {

        if (isServer)
        {
            GameObject ourLayout = GameObject.Find("Mockup(server)");
            GameObject layout = GameObject.Find("Mockup(client)");
            if (layout != null && ourLayout !=null)
            {
                spawnPosition.z = -spawnPosition.z;
                spawnRotation.eulerAngles = new Vector3(spawnRotation.eulerAngles.x, spawnRotation.eulerAngles.y+ layout.transform.rotation.eulerAngles.y - ourLayout.transform.rotation.eulerAngles.y, spawnRotation.eulerAngles.z) ;
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
                Debug.Log("rpc4");
                RpcAttachObject(o.GetComponent<NetworkIdentity>().netId, anchorName, "Mockup(client)");
            }

        }
        else
        {
            Debug.Log("cmd4");
            CmdSpawnObject(prefab, spawnPosition, spawnRotation, name);
        }


    }

    
    [Command]
    void CmdSpawnObject(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation, string name)
    {
        GameObject ourLayout = GameObject.Find("Mockup(client)");
        GameObject layout = GameObject.Find("Mockup(server)");

        GameObject player = GameObject.Find("Player (Remote)");
        if (layout != null && ourLayout != null)
        {
            spawnPosition.z = -spawnPosition.z;
            spawnRotation.eulerAngles = new Vector3(spawnRotation.eulerAngles.x, spawnRotation.eulerAngles.y + layout.transform.rotation.eulerAngles.y - ourLayout.transform.rotation.eulerAngles.y, spawnRotation.eulerAngles.z);
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
            NetworkServer.SpawnWithClientAuthority(o,player);
            Debug.Log("rpc5");
            RpcAttachObject(o.GetComponent<NetworkIdentity>().netId, name, "Mockup(server)");
        }
    }

    [Command] 
    void CmdAttachObject(NetworkInstanceId identity, string name, string layoutname)
    {
        GameObject o = ClientScene.FindLocalObject(identity);
        if (o != null)
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
                       // o.GetComponent<AnchorableBehaviour>().TryAttach(a);

                        if (o.tag == "Exterior" || o.tag == "BillboardEx")
                            layout.GetComponent<AttachObjectManager>().addExteriorObject(o);
                        else if (o.tag == "Interior" || o.tag == "BillboardInterior")
                            layout.GetComponent<AttachObjectManager>().addInteriorObject(o);
                    }
                }
            }
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
                       // o.GetComponent<AnchorableBehaviour>().TryAttach(a);

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
         Anchor anchor = gameObject.GetComponent<AnchorableBehaviour>().anchor;
        Vector3 angles = transform.rotation.eulerAngles;

        // Debug.Log("Transform object");
        //Transform t = anchor.transform;

        transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Round((angles.y- anchor.transform.eulerAngles.y) /90.0f)*90+ anchor.transform.eulerAngles.y, 0));

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
