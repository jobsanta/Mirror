using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Leap.Unity.Attributes;
using Leap.Unity.Interaction.Internal;
using Leap.Unity.Query;
using Leap.Unity.Space;
using Leap.Unity.Interaction;


public class ControlPanelController : MonoBehaviour {

    public bool Left;

    GameObject player;
    LayoutController controller;

	// Update is called once per frame
	void LateUpdate () 
        {
        //if (player == null)
        //{
        //    player = GameObject.FindGameObjectWithTag("Player");

        //    if (player != null)
        //    {
        //        controller = player.GetComponent<LayoutController>();
        //        InteractionButton[] buttons = gameObject.GetComponentsInChildren<InteractionButton>();
        //        Debug.Log(buttons.Length);
        //        for (int i = 0; i < buttons.Length; i++)
        //        {
        //            Debug.Log(buttons[i].name);
        //            buttons[i].manager = player.GetComponentInChildren<InteractionManager>();
        //        }
        //    }
        //}
        
        Vector3 offset = new Vector3(-0.1f,-0.01f,0.55f);

        if (Left)
        {
            if (Camera.main.transform.position.z > 0)
            {
                offset.x = (0.55f * (0.3f - Camera.main.transform.position.x) / Camera.main.transform.position.z);
                offset.x -= 0.05f;
                //offset.x = 0.1f;
                offset.z = -0.55f;
            }
            else if (Camera.main.transform.position.z < 0)
            {
                offset.x = (0.55f * (-0.3f - Camera.main.transform.position.x) / -Camera.main.transform.position.z);
                offset.x += 0.05f;
                //offset.x = 0.1f
            }

        }
        else
        {
            if (Camera.main.transform.position.z > 0)
            {
                offset.x = (0.55f * (-0.3f - Camera.main.transform.position.x) / Camera.main.transform.position.z);
                offset.x += 0.05f;
                //offset.x = 0.1f;
                offset.z = -0.55f;
            }
            else if (Camera.main.transform.position.z < 0)
            {
                offset.x = (0.55f * (0.3f - Camera.main.transform.position.x) / -Camera.main.transform.position.z);
                offset.x -= 0.05f;
                //offset.x = 0.1f
            }

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

    public void changeOwnSkeletonView()
    {
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }

        controller.SetOwnSkeletonView();
    }
    public void changeTheirSkeletonView()
    {
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }

        controller.SetTheirSkeletonView();
    }

    public void changeBillboardSkeletonView()
    {
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }

        controller.SetBillboardSkeletonView();
    }


    public void moveLayoutHorizontal(float h)
    {
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }
        controller.SetHorizontalPosition(h);
    }

    public void moveLayoutVertical(float v)
    {
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }
        controller.SetVerticalPosition(v);
    }

    public void HorizontalRotation(float h)
    {
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }
        controller.SetHorizontalRotation(h);
    }

    public void VerticalRotation(float v)
    {
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }
        controller.SetVerticalRotation(v);
    }

    public void Sync()
    {
        if (controller == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");


            controller = player.GetComponent<LayoutController>();
        }
        controller.Synchronize();
    }


}
