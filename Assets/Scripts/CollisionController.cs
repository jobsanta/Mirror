using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {

    BoxCollider boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
	// Use this for initialization
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Layout"))
            {
            boxCollider.enabled = false;
        }

        if(!collision.gameObject.CompareTag("Layout"))
        {
            boxCollider.enabled = true;
        }
    }
}
