using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawner : NetworkBehaviour {

    public GameObject objectPrefab;
    public int numberOfObjects;
    public GameObject[] componentPrefab;

   

    public override void OnStartLocalPlayer()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Transform p = GetComponent<Transform>();
        Vector3 spawnPosition;
        if (p.position.z > 0.0f)
            spawnPosition = new Vector3(0.0f, 0.2f, 0.10f);
        else
            spawnPosition = new Vector3(0.0f, 0.2f, -0.10f);

        CmdCreateBox(spawnPosition, Color.red);

        if (p.position.z > 0.0f)
            spawnPosition = new Vector3(0.0f, 0.2f, 0.20f);
        else
            spawnPosition = new Vector3(0.0f, 0.2f, -0.20f);
        CmdCreateComponent(spawnPosition, Color.red);

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
    void CmdCreateComponent(Vector3 spawnPosition, Color c)
    {
        for (int i=0; i < numberOfObjects; i++)
        {
            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                0.0f, 
                0.0f);


            GameObject o = (GameObject)Instantiate(componentPrefab[i%componentPrefab.Length], spawnPosition, spawnRotation);

            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }
    }




}
