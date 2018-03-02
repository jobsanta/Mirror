using System.Collections;
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
    IEnumerator coroutine;
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
            
            spawnPosition = new Vector3(0.0f, 0.15f, 0.15f);

            CmdCreateBox(spawnPosition, Color.red);

            if (billboardPrefab != null)
                CreateBillboard(billboardPrefab, spawnPosition);


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

                spawnPosition = new Vector3(0.0f, 0.15f, -0.15f);

            CmdCreateBox(spawnPosition, Color.red);

            if(billboardPrefab != null)
            CreateBillboard(billboardPrefab, spawnPosition);
            spawnPosition = new Vector3(0.0f, 0.15f, -0.15f);

            CmdCreateServerComponent(spawnPosition, Color.red);

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
           45.0f,
            0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);


            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }
        else
        {
            var spawnRotation = Quaternion.Euler(
            0.0f,
          -135.0f,
            0.0f);


            GameObject o = (GameObject)Instantiate(objectPrefab, spawnPosition, spawnRotation);


            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }



    }
        

    [Command]
    void CmdCreateServerComponent(Vector3 spawnPosition, Color c)
    {
        int row = ServerComponentPrefab.Length / 4;
        if (row == 0) row = 1;
        int col = ServerComponentPrefab.Length / row;

        for (int i=0; i < ServerComponentPrefab.Length; i++)
        {
            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                0.0f, 
                0.0f);
            Vector3 pos = new Vector3(((int)i/row)*0.4f/col-0.2f, 0.1f, -0.3f+ (i%row)/10.0f);
            GameObject o = (GameObject)Instantiate(ServerComponentPrefab[i%ServerComponentPrefab.Length], pos, spawnRotation);

            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }
    }

    [Command]
    void CmdCreateClientComponent(Vector3 spawnPosition, Color c)
    {

        int row = ClientComponentPrefab.Length / 4;

        if (row == 0) row = 1;
        int col = ClientComponentPrefab.Length / row;
        for (int i=0; i < ClientComponentPrefab.Length; i++)
        {
            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                0.0f, 
                0.0f);

            Vector3 pos = new Vector3(((int)i / row) * 0.4f / col - 0.2f, 0.1f, 0.3f - (i % row) / 10.0f);
            GameObject o = (GameObject)Instantiate(ClientComponentPrefab[i%ClientComponentPrefab.Length], pos, spawnRotation);

            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }
    }
        




}
