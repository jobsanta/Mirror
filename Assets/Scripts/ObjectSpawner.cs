﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawner : NetworkBehaviour {

    public GameObject objectPrefab;
    public int numberOfObjects;
    public GameObject[] ServerComponentPrefab;
    public GameObject[] ClientComponentPrefab;
    public GameObject panelPrefab;

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


            CreatePanel(Camera.main.transform.position);
        }
        else
        {
            transform.position = new Vector3(0.0f, -0.2f, -0.25f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            Vector3 spawnPosition;

                spawnPosition = new Vector3(0.0f, 0.2f, -0.10f);

            CmdCreateBox(spawnPosition, Color.red);


                spawnPosition = new Vector3(0.0f, 0.2f, -0.20f);

            CmdCreateServerComponent(spawnPosition, Color.red);

            CreatePanel(Camera.main.transform.position);

        }

        


    }

  
   


    void CreatePanel(Vector3 spawnPosition)
    {
        var spawnRotation = Quaternion.Euler( 
            0.0f, 
            0.0f, 
            0.0f);
        GameObject o = (GameObject)Instantiate(panelPrefab, spawnPosition, spawnRotation);

    }


    [Command]    
    void CmdCreateBox(Vector3 spawnPosition, Color c)
    {
        
            var spawnRotation = Quaternion.Euler( 
                0.0f, 
               0.0f, 
                0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);


            NetworkServer.SpawnWithClientAuthority(o, gameObject);
       
  
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
        




}
