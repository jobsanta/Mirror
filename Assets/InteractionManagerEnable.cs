using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using Leap.Unity.Query;
using Leap.Unity.Examples;

public class InteractionManagerEnable : MonoBehaviour {

	// Use this for initialization
	void Start () {

        gameObject.name = "TransformTool";
        gameObject.GetComponent<TransformTool>().Enable = false;
        //custom
        GameObject hplayer = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<TransformTool>().interactionManager = hplayer.GetComponentInChildren<InteractionManager>();

        StartCoroutine(TargetToTransform());

    }

    IEnumerator TargetToTransform()
    {
        yield return new WaitForSeconds(1.0f);
        if (LayoutController.thisisServer)
        {
            GameObject layout = GameObject.Find("Mockup(server)");
            gameObject.GetComponent<TransformTool>().target = layout.GetComponent<Transform>();
        }
        else
        {
            GameObject layout = GameObject.Find("Mockup(client)");
            gameObject.GetComponent<TransformTool>().target = layout.GetComponent<Transform>();
        }
    }

}
