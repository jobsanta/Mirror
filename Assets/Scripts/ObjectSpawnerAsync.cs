﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawnerAsync : NetworkBehaviour {

    public GameObject objectPrefab;
    public int numberOfObjects;
    public GameObject[] ServerComponentPrefab;
    public GameObject[] ClientComponentPrefab;
    public GameObject panelPrefabL;
    public GameObject panelPrefabR;

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

            CreateBox(spawnPosition);

            spawnPosition = new Vector3(0.0f, 0.2f, -0.10f);

            CreateBillbordBox(spawnPosition);

            spawnPosition = new Vector3(0.0f, 0.2f, 0.20f);
            CreateClientComponent(spawnPosition);


            CreatePanel(panelPrefabL, Camera.main.transform.position);
            CreatePanel(panelPrefabR, Camera.main.transform.position);
        }
        else
        {
            transform.position = new Vector3(0.0f, -0.2f, -0.25f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            Vector3 spawnPosition;

            spawnPosition = new Vector3(0.0f, 0.2f, -0.10f);

            CreateBox(spawnPosition);

            spawnPosition = new Vector3(0.0f, 0.2f, 0.10f);

            CreateBillbordBox(spawnPosition);

            spawnPosition = new Vector3(0.0f, 0.2f, -0.20f);

            CreateServerComponent(spawnPosition);

            CreatePanel(panelPrefabL,Camera.main.transform.position);
            CreatePanel(panelPrefabR, Camera.main.transform.position);

        }

        


    }

  
   


    void CreatePanel(GameObject panel,Vector3 spawnPosition)
    {
        var spawnRotation = Quaternion.Euler( 
            0.0f, 
            0.0f, 
            0.0f);
        GameObject o = (GameObject)Instantiate(panel, spawnPosition, spawnRotation);

    }


  
    void CreateBox(Vector3 spawnPosition)
    {

        if (spawnPosition.z < 0)
        {
            var spawnRotation = Quaternion.Euler(
            0.0f,
           0.0f,
            0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);
            o.name = "Mockup(server)";

            //NetworkServer.SpawnWithClientAuthority(o, gameObject);
        }
        else
        {
            var spawnRotation = Quaternion.Euler(
            0.0f,
           180.0f,
            0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);
            o.name = "Mockup(client)";

            //NetworkServer.SpawnWithClientAuthority(o, gameObject);
        }
        
    }

    void CreateBillbordBox(Vector3 spawnPosition)
    {

        if (spawnPosition.z < 0)
        {
            var spawnRotation = Quaternion.Euler(
            0.0f,
           0.0f,
            0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);
            o.GetComponent<Rigidbody>().isKinematic = true;
            o.name = "Mockup(server)";
            //NetworkServer.SpawnWithClientAuthority(o, gameObject);
        }
        else
        {
            var spawnRotation = Quaternion.Euler(
            0.0f,
            0.0f,
            0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);
            o.GetComponent<Rigidbody>().isKinematic = true;
            o.name = "Mockup(client)";
            //NetworkServer.SpawnWithClientAuthority(o, gameObject);
        }

    }


    void CreateServerComponent(Vector3 spawnPosition)
    {
        for (int i=0; i < numberOfObjects; i++)
        {
            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                0.0f, 
                0.0f);


            GameObject o = (GameObject)Instantiate(ServerComponentPrefab[i%ServerComponentPrefab.Length], spawnPosition, spawnRotation);

           // NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }
    }

    void CreateClientComponent(Vector3 spawnPosition)
    {
        for (int i=0; i < numberOfObjects; i++)
        {
            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                0.0f, 
                0.0f);


            GameObject o = (GameObject)Instantiate(ClientComponentPrefab[i%ClientComponentPrefab.Length], spawnPosition, spawnRotation);


        }
    }
        




}
