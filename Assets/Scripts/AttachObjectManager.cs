using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;


public class AttachObjectManager : MonoBehaviour {

	// Use this for initialization
    public List<GameObject> exteriorList;
    public List<GameObject> interiorList;
    AnchorGroup _extgroup;
    AnchorGroup _intgroup;

    void Start () {
        exteriorList = new List<GameObject>();
        interiorList = new List<GameObject>();
        GameObject exterior_group =  GameObject.Find("Exterior Anchor Group"); 
        GameObject interior_group =  GameObject.Find("Interior Anchor Group"); 


        _extgroup = exterior_group.GetComponent<AnchorGroup>();
        _intgroup = interior_group.GetComponent<AnchorGroup>();


        Anchor[] extanchor = gameObject.transform.GetChild(1).gameObject.GetComponentsInChildren<Anchor>();
        Anchor[] intanchor = gameObject.transform.GetChild(0).gameObject.GetComponentsInChildren<Anchor>();

        for (int i = 0; i < extanchor.Length; i++)
            _extgroup.Add(extanchor[i]);

        for (int i = 0; i < intanchor.Length; i++)
            _intgroup.Add(intanchor[i]);
    }

    public List<GameObject> getInteriorList()
    {
        return interiorList;
    }

    public List<GameObject> getExteriorList()
    {
        return exteriorList;
    }

    public void addInteriorObject(GameObject objects)
    {
        interiorList.Add(objects);
    }

    public void clearInteriorObject()
    {
        interiorList.Clear();
    }

    public void removeInteriorObject(GameObject objects)
    {
        interiorList.Remove(objects);
    }

    public void addExteriorObject(GameObject objects)
    {
        exteriorList.Add(objects);
    }

    public void clearExteriorObject()
    {
        exteriorList.Clear();
    }

    public void removeExteriorObject(GameObject objects)
    {
        exteriorList.Remove(objects);
    }
}
