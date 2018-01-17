using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeapSpawner : NetworkBehaviour 
{

    public GameObject LeapPreFab;
    Transform t;

    void Start()
    {
        t = GetComponent<Transform>();
    }


    public override void OnStartServer()
    {
        var L = (GameObject) Instantiate(LeapPreFab, t.position, t.rotation);

       NetworkServer.Spawn(L);

    }
}
