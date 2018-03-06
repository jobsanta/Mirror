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

        CmdCreateServerComponent();
      
    }


    [Command]
    void CmdCreateServerComponent()
    {
        int row = ServerComponentPrefab.Length / 4;
        if (row == 0) row = 1;
        int col = ServerComponentPrefab.Length / row;

        GameObject layout = GameObject.Find("Mockup(server)");
        Anchor[] anchors = layout.transform.GetChild(0).gameObject.GetComponentsInChildren<Anchor>();

        for (int i = 0; i < ServerComponentPrefab.Length; i++)
        {
            var spawnRotation = Quaternion.Euler(
                0.0f,
                0.0f,
                0.0f);
            Vector3 pos = new Vector3(((int)i / row) * 0.4f / col - 0.2f, 0.105f, -0.3f + (i % row) / 20.0f);

            GameObject o = (GameObject)Instantiate(ServerComponentPrefab[i % ServerComponentPrefab.Length], pos, spawnRotation);


            bool attach = false;

            while (!attach)
            {
                int value = Random.Range(0, anchors.Length - 1);
                if (anchors[value].anchoredObjects.Count == 0)
                {
                    AnchorableBehaviour a = o.GetComponent<AnchorableBehaviour>();
                    a.anchor = anchors[value];
                    attach = a.TryAttach(true);

                }

            }



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

            Vector3 pos = new Vector3(((int)i / row) * 0.4f / col - 0.2f, 0.105f, 0.3f - (i % row) / 20.0f);
            GameObject o = (GameObject)Instantiate(ClientComponentPrefab[i%ClientComponentPrefab.Length], pos, spawnRotation);

            NetworkServer.SpawnWithClientAuthority(o, gameObject);

        }
    }
        




}
