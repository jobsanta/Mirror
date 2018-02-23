﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawner : NetworkBehaviour {

    public GameObject objectPrefab;
    public GameObject billboardPrefab;
    public int numberOfObjects;
    public GameObject[] ServerComponentPrefab;
    public GameObject[] ClientComponentPrefab;
    public GameObject panelPrefabL;
    public GameObject panelPrefabR;
    public GameObject TransformTool;

    private NetworkStartPosition[] spawnPoints;


    public override void OnStartLocalPlayer()
    {

        if (!isLocalPlayer)
        {
            return;
        }
           


        if (!isServer)
        {

            transform.position = new Vector3(0.0f, -0.2f, 0.25f);
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

            Vector3 spawnPosition;
            
            spawnPosition = new Vector3(0.0f, 0.2f, 0.10f);

            CmdCreateBox(spawnPosition, Color.red);

            if (billboardPrefab != null)
                CreateBillboard(billboardPrefab, spawnPosition);


            spawnPosition = new Vector3(0.0f, 0.2f, 0.20f);
            CmdCreateClientComponent(spawnPosition, Color.red);


            StartCoroutine(CreatePanel(panelPrefabL, Camera.main.transform.position));
            //StartCoroutine(CreatePanel(panelPrefabR, Camera.main.transform.position));
        }
        else
        {
            transform.position = new Vector3(0.0f, -0.2f, -0.25f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            Vector3 spawnPosition;

                spawnPosition = new Vector3(0.0f, 0.2f, -0.10f);

            CmdCreateBox(spawnPosition, Color.red);

            if(billboardPrefab != null)
            CreateBillboard(billboardPrefab, spawnPosition);
            spawnPosition = new Vector3(0.0f, 0.2f, -0.20f);

            CmdCreateServerComponent(spawnPosition, Color.red);

            StartCoroutine(CreatePanel(panelPrefabL,Camera.main.transform.position));
           // StartCoroutine(CreatePanel(panelPrefabR, Camera.main.transform.position));


        }

        CreateTransformTool(TransformTool);


    }

  
   
    void CreateTransformTool(GameObject tool)
    {
        GameObject b = (GameObject)Instantiate(tool, new Vector3(0,0,0), new Quaternion());
    }

    IEnumerator CreatePanel(GameObject panel,Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(1.0f);
        var spawnRotation = Quaternion.Euler( 
            0.0f, 
            0.0f, 
            0.0f);
        GameObject o = (GameObject)Instantiate(panel, spawnPosition, spawnRotation);

    }

    void CreateBillboard(GameObject bill, Vector3 spawnPosition)
    {
        if (spawnPosition.z < 0)
        {
            spawnPosition.z = -spawnPosition.z;
            var spawnRotation = Quaternion.Euler(
             0.0f,
            0.0f,
             0.0f);
            GameObject b = (GameObject)Instantiate(billboardPrefab, spawnPosition, spawnRotation);
            b.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            spawnPosition.z = -spawnPosition.z;
            var spawnRotation = Quaternion.Euler(
             0.0f,
            180.0f,
             0.0f);
            GameObject b = (GameObject)Instantiate(billboardPrefab, spawnPosition, spawnRotation);
            b.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    [Command]    
    void CmdCreateBox(Vector3 spawnPosition, Color c)
    {

        if (spawnPosition.z < 0)
        {
            var spawnRotation = Quaternion.Euler(
            0.0f,
           0.0f,
            0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);


            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }
        else
        {
            var spawnRotation = Quaternion.Euler(
            0.0f,
           180.0f,
            0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);

     

            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }



    }
        

    [Command]
    void CmdCreateServerComponent(Vector3 spawnPosition, Color c)
    {
        for (int i=0; i < numberOfObjects; i++)
        {
            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                0.0f, 
                0.0f);


            GameObject o = (GameObject)Instantiate(ServerComponentPrefab[i%ServerComponentPrefab.Length], spawnPosition, spawnRotation);

            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }
    }

    [Command]
    void CmdCreateClientComponent(Vector3 spawnPosition, Color c)
    {
        for (int i=0; i < numberOfObjects; i++)
        {
            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                0.0f, 
                0.0f);


            GameObject o = (GameObject)Instantiate(ClientComponentPrefab[i%ClientComponentPrefab.Length], spawnPosition, spawnRotation);

            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }
    }
        




}
