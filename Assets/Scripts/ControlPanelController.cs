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


        controller = player.GetComponent<LayoutController>();

        Debug.Log(player.name);
     
  
    }
	// Update is called once per frame
	void LateUpdate () {

        Vector3 offset = new Vector3(-0.05f,-0.025f,0.3f);

        if (Camera.main.transform.position.z > 0)
        {
            offset.z = -0.3f;
            offset.x = 0.05f;

        }


        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(new Vector3(0,180,0));
        transform.position = Camera.main.transform.position + offset;
	}


    public void FreeFall()
    {
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }
        controller.SetFreeFall();
    }
    public void FixView()
    {
        Debug.Log("Button Press");
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }

        controller.SetFrontView();
    }
}
