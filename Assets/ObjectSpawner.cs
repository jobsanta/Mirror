using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawner : NetworkBehaviour {

    public GameObject objectPrefab;
    public int numberOfObjects;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
            {
            CmdCreateBox();
            }
    }

    [Command]
    void CmdCreateBox()
    {
        for (int i=0; i < numberOfObjects; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(-0.3f, 0.3f),
                1.0f,
                Random.Range(-0.5f, 0.5f));

            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                Random.Range(0,180), 
                0.0f);

            var o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(o);
        }
    }
}
