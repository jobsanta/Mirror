using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager {

	// Use this for initialization
    public override void OnServerConnect(NetworkConnection conn)
    {
        
        Debug.Log("test");
    }

    public override void OnStopHost()
    {
        Debug.Log("test stophost");
        GameObject[] controller= GameObject.FindGameObjectsWithTag("GameController");

        foreach (GameObject o in controller)
        {
            DestroyObject(o);
        }
    }



    public override void OnClientDisconnect(NetworkConnection conn)
    {

        Debug.Log("Client exit");


        StopClient();


    }
}
