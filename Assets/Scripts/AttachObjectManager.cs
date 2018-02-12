using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;


public class AttachObjectManager : MonoBehaviour {

	// Use this for initialization
    public List<GameObject> childList;
    AnchorGroup _extgroup;
    AnchorGroup _intgroup;

    void Start () {
        childList = new List<GameObject>();
        GameObject exterior_group =  GameObject.Find("Exterior Anchor Group"); 
        GameObject interior_group =  GameObject.Find("Interior Anchor Group"); 


        _extgroup = exterior_group.GetComponent<AnchorGroup>();
        _intgroup = interior_group.GetComponent<AnchorGroup>();


        Anchor[] extanchor = gameObject.transform.GetChild(2).gameObject.GetComponentsInChildren<Anchor>();
        Anchor[] intanchor = gameObject.transform.GetChild(1).gameObject.GetComponentsInChildren<Anchor>();

        for (int i = 0; i < extanchor.Length; i++)
            _extgroup.Add(extanchor[i]);

        for (int i = 0; i < intanchor.Length; i++)
            _intgroup.Add(intanchor[i]);
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
