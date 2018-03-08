using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using Leap.Unity.Query;
using Leap.Unity.Examples;

public class InteractionManagerEnable : MonoBehaviour {

	// Use this for initialization
	void Start () {



        StartCoroutine(TargetToTransform());

    }

    IEnumerator TargetToTransform()
    {
        yield return new WaitForSeconds(1.0f);

        gameObject.name = "TransformTool";
        gameObject.GetComponent<TransformTool>().Enable = true;
        //custom
        GameObject hplayer = GameObject.FindGameObjectWithTag("Player");
        InteractionManager _interactionManager = hplayer.GetComponentInChildren<InteractionManager>();
        gameObject.GetComponent<TransformTool>().interactionManager = _interactionManager;
        InteractionBehaviour[] _behaviors = gameObject.GetComponentsInChildren<InteractionBehaviour>();
        for (int i = 0; i < _behaviors.Length; i++)
            _behaviors[i].manager = _interactionManager;

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
