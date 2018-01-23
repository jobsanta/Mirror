using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawner : NetworkBehaviour {

    public GameObject objectPrefab;
    public int numberOfObjects;

   

    public override void OnStartLocalPlayer()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Transform p = GetComponent<Transform>();
        Vector3 spawnPosition;
        if (p.position.z > 0.0f)
            spawnPosition = new Vector3(0.0f, 0.15f, 0.10f);
        else
            spawnPosition = new Vector3(0.0f, 0.15f, -0.10f);

        CmdCreateBox(spawnPosition, Color.red);

    }

    [Command]
    void CmdCreateBox(Vector3 spawnPosition, Color c)
    {
        for (int i=0; i < numberOfObjects; i++)
        {
            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                Random.Range(0,180), 
                0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);

            o.GetComponent<Renderer>().material.color = c;

            NetworkServer.SpawnWithClientAuthority(o, gameObject);
        }
    }
        

}
