using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachObjectManager : MonoBehaviour {

	// Use this for initialization
    public List<GameObject> childList;


    void Start () {
        childList = new List<GameObject>();
    }

    public void addObject(GameObject objects)
    {
        childList.Add(objects);
    }

    public void clearObject()
    {
        childList.Clear();
    }

    public void removeObject(GameObject objects)
    {
        childList.Remove(objects);
    }
}
