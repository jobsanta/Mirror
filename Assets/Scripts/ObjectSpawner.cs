using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Leap.Unity.Interaction;

public class ObjectSpawner : NetworkBehaviour {

    public GameObject objectPrefab;
    public GameObject billboardPrefab;
    public int numberOfObjects;
    public GameObject[] ServerComponentPrefab;
    public GameObject[] ClientComponentPrefab;
    public GameObject panelPrefabL;
    public GameObject panelPrefabR;
    public GameObject TransformTool;
    IEnumerator coroutine;
    private NetworkStartPosition[] spawnPoints;

    public bool isAsync;


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
            
            spawnPosition = new Vector3(0.0f, 0.105f, 0.2f);

            CmdCreateBox(spawnPosition, Color.red);

            if (billboardPrefab != null)
                CreateBillboard(billboardPrefab, spawnPosition);

            if (isAsync) DisableComponent();


            spawnPosition = new Vector3(0.0f, 0.15f, 0.15f);
            CmdCreateClientComponent(spawnPosition, Color.red);

            coroutine = CreatePanel(panelPrefabL, Camera.main.transform.position);
            StartCoroutine(coroutine);
            //StartCoroutine(CreatePanel(panelPrefabR, Camera.main.transform.position));
        }
        else
        {
            transform.position = new Vector3(0.0f, -0.2f, -0.25f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            Vector3 spawnPosition;

            spawnPosition = new Vector3(0.0f, 0.105f, -0.2f);

            CmdCreateBox(spawnPosition, Color.red);

            if(billboardPrefab != null)
            CreateBillboard(billboardPrefab, spawnPosition);

          


            spawnPosition = new Vector3(0.0f, 0.15f, -0.15f);

            coroutine = CreateServerComponent();
            StartCoroutine(coroutine);

            coroutine = CreatePanel(panelPrefabL, Camera.main.transform.position);
            StartCoroutine(coroutine);
            // StartCoroutine(CreatePanel(panelPrefabR, Camera.main.transform.position));


        }

        CreateTransformTool(TransformTool);


    }
 
    void CreateTransformTool(GameObject tool)
    {
        GameObject b = (GameObject)Instantiate(tool, new Vector3(0,0,0), new Quaternion());
    }

    IEnumerator CreatePanel(GameObject panel,Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(0.5f);
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
           30.0f,
            0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);


            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }
        else
        {
            var spawnRotation = Quaternion.Euler(
            0.0f,
          -150.0f,
            0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);


            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }



    }
        
    IEnumerator CreateServerComponent()
    {
        yield return new WaitForSeconds(0.5f);

        CrServerComponent();
      
    }

    void DisableComponent()
    {
        for(int i =0;i< ServerComponentPrefab.Length;i++)
        {
           GameObject o =  GameObject.Find(ServerComponentPrefab[i].name + "(Clone)");
            if(o!=null)   o.SetActive(false);
        }
        
    }

    void CrServerComponent()
    {
        var spawnRotation = Quaternion.Euler(
        0.0f,
        0.0f,
        0.0f);
        Vector3 pos;
        GameObject o;
        for (int i = 0; i < ServerComponentPrefab.Length/2 ; i++)
        {
            pos = new Vector3(-0.1975f+i*0.0625f,0.105f,-0.4f);

            o = (GameObject)Instantiate(ServerComponentPrefab[i % ServerComponentPrefab.Length], pos, spawnRotation);

            o.transform.RotateAround(new Vector3(0.0f, 0.0f, -0.2f), Vector3.up, 30.0f);

            NetworkServer.SpawnWithClientAuthority(o, gameObject);


        }


        for (int i = ServerComponentPrefab.Length / 2; i < ServerComponentPrefab.Length; i++)
        {

            pos = new Vector3(0.175f , 0.105f, -0.4f + (i-ServerComponentPrefab.Length / 2) * 0.0625f);

            o = (GameObject)Instantiate(ServerComponentPrefab[i % ServerComponentPrefab.Length], pos, spawnRotation);

            o.transform.RotateAround(new Vector3(0.0f, 0.0f, -0.2f), Vector3.up, 30.0f);

            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }
    }

    [Command]
    void CmdCreateClientComponent(Vector3 spawnPosition, Color c)
    {

        int row = ClientComponentPrefab.Length / 4;

        if (row == 0) row = 1;
        int col = ClientComponentPrefab.Length / row;
        Vector3 pos;
        GameObject o;
        var spawnRotation = Quaternion.Euler(
         0.0f,
         0.0f,
         0.0f);
        for (int i = 0; i < ClientComponentPrefab.Length / 2; i++)
        {
            pos = new Vector3(0.1975f - i * 0.0625f, 0.105f, 0.4f);

            o = (GameObject)Instantiate(ClientComponentPrefab[i % ClientComponentPrefab.Length], pos, spawnRotation);

            o.transform.RotateAround(new Vector3(0.0f, 0.0f, 0.2f), Vector3.up, 30.0f);

            NetworkServer.SpawnWithClientAuthority(o, gameObject);

            if (isAsync) o.SetActive(false);

        }

        for (int i = ClientComponentPrefab.Length / 2; i < ClientComponentPrefab.Length; i++)
        {

            pos = new Vector3(-0.175f, 0.105f, 0.4f - (i - ClientComponentPrefab.Length / 2) * 0.0625f);

            o = (GameObject)Instantiate(ClientComponentPrefab[i % ClientComponentPrefab.Length], pos, spawnRotation);

            o.transform.RotateAround(new Vector3(0.0f, 0.0f, 0.2f), Vector3.up, 30.0f);

            NetworkServer.SpawnWithClientAuthority(o, gameObject);

            if (isAsync) o.SetActive(false);

        }

    }
        

    public void GraspObjectSpawner(GameObject obj, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        CmdCreateClientComponentOnGrasp(obj, spawnPosition, spawnRotation);
    }

    [Command]
    void CmdCreateClientComponentOnGrasp(GameObject obj, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject  o = (GameObject)Instantiate(obj, spawnPosition, spawnRotation);

           
        NetworkServer.SpawnWithClientAuthority(o, gameObject);

    }



}
