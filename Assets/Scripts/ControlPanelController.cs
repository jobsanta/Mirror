using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Leap.Unity.Attributes;
using Leap.Unity.Interaction.Internal;
using Leap.Unity.Query;
using Leap.Unity.Space;
using Leap.Unity.Interaction;

public class ControlPanelController : MonoBehaviour {

    GameObject player;
    LayoutController controller;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            controller = player.GetComponent<LayoutController>();
        }  
    }
	// Update is called once per frame
	void LateUpdate () {

        Vector3 offset = new Vector3(-0.1f,-0.025f,0.6f);


        if (Camera.main.transform.position.z > 0)
        {
			offset.x = (0.6f * (0.3f - Camera.main.transform.position.x) / Camera.main.transform.position.z);
			offset.x -= 0.05f;
            //offset.x = 0.1f;
			offset.z = -0.6f;
        }
		else if (Camera.main.transform.position.z < 0)
		{
			offset.x = (0.6f * (-0.3f - Camera.main.transform.position.x) / -Camera.main.transform.position.z);
			offset.x += 0.05f;
			//offset.x = 0.1f
		}


        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(new Vector3(0,180,0));
        transform.position = Camera.main.transform.position + offset;
	}

    public void changeFixView()
    {
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }

        controller.SetFrontView();
    }

    public void changeSkeletonView()
    {
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }

        controller.SetSkeletonView();
    }
}
