using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;


public class AttachObjectManager : MonoBehaviour {

	// Use this for initialization
    public List<GameObject> exteriorList;
    public List<GameObject> interiorList;

    List<string> attachOrder;
    List<string> attachOrderAnchor;


    Dictionary<string, string> conflictDict;
    AnchorGroup _extgroup;
    AnchorGroup _intgroup;

    ConflictDictionarySync conflictdictSync;
    ConflictDictionary conflictdictAsync;

    void Start () {

        GameObject conflict = GameObject.Find("Conflict Dictionary");

        conflictdictAsync = conflict.GetComponent<ConflictDictionary>();
        conflictdictSync = conflict.GetComponent<ConflictDictionarySync>();

        if (conflictdictAsync != null)
            conflictDict = conflictdictAsync.getConflictDictionary();
        else if (conflictdictSync!= null)
            conflictDict = conflictdictSync.GetConflictDictionary();
        attachOrder = new List<string>();
        attachOrderAnchor = new List<string>();

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
        if(LayoutController.thisisServer && gameObject.name=="Mockup(server)")
        {
            attachOrder.Add(objects.name);
            attachOrderAnchor.Add(objects.GetComponent<AnchorableBehaviour>().anchor.name);
        }


        checkConflict(objects, true);
    }

    public void clearInteriorObject()
    {
        interiorList.Clear();
    }

    public void removeInteriorObject(GameObject objects)
    {
        interiorList.Remove(objects);
        checkpairConflict(objects, true);
    }

    public void addExteriorObject(GameObject objects)
    {
        exteriorList.Add(objects);
        if (!LayoutController.thisisServer && gameObject.name == "Mockup(client)")
        {
            attachOrder.Add(objects.name);
            attachOrderAnchor.Add(objects.GetComponent<AnchorableBehaviour>().anchor.name);
        }
        checkConflict(objects, false);
    }

    public void clearExteriorObject()
    {
        exteriorList.Clear();
    }

    public void removeExteriorObject(GameObject objects)
    {
        exteriorList.Remove(objects);
        checkpairConflict(objects, false);
    }

    void checkConflict(GameObject obj,  bool isInterior)
    {

        string[] anchorsplit = obj.GetComponent<AnchorableBehaviour>().anchor.name.Split(' ');
        string anchorNumber = anchorsplit[anchorsplit.Length - 1];
        //"Inner Component Capsule Async"
        string[] split = obj.name.Split('(');

 
            string conflicted;
            if (conflictDict.TryGetValue(split[0], out conflicted))
            {

                List<GameObject> attachList;
                if (isInterior) attachList = exteriorList;
                else attachList = interiorList;
                
                bool foundMatch = false;
                
                foreach (GameObject l in attachList)
                {
                    string[] l_split = l.name.Split('(');
                    if (conflicted == l_split[0])
                        {
                            
                            string[] anchorSplitpair = l.GetComponent<AnchorableBehaviour>().anchor.name.Split(' ');
                            string anchorNp= anchorSplitpair[anchorSplitpair.Length - 1];
                             
                        if(anchorNp == anchorNumber)
                        {
                            foundMatch = true;
                             //Match found resolve 
                             ResolveConflict(l, obj);
                        }
                      
                    }
                }


                if(!foundMatch)
                {

                    GlowObject globj = obj.GetComponent<GlowObject>();
                    globj.isConflict = true;
                    globj.OnConflict();
                }
              
            }
        
    }

    void checkpairConflict(GameObject obj, bool isInterior)
    {


        string[] anchorsplit = obj.GetComponent<AnchorableBehaviour>().anchor.name.Split(' ');
        string anchorNumber = anchorsplit[anchorsplit.Length - 1];
        //"Inner Component Capsule Async"
        string[] split = obj.name.Split('(');

        string conflicted;
        if (conflictDict.TryGetValue(split[0], out conflicted))
        {
            List<GameObject> attachList;
            if (isInterior) attachList = exteriorList;
            else attachList = interiorList;


            foreach (GameObject l in attachList)
            {
                string[] l_split = l.name.Split('(');
                if (conflicted == l_split[0])
                {
                    GlowObject globj = l.GetComponent<GlowObject>();
                    globj.isConflict = true;
                    globj.OnConflict();

                }
            }
        }
    }

    void ResolveConflict(GameObject one, GameObject two)
    {
        GlowObject globj = one.GetComponent<GlowObject>();
        globj.isConflict = false;
        globj.OnGraspEnd();
     
        globj = two.GetComponent<GlowObject>();
        globj.isConflict = false;
        globj.OnGraspEnd();

    }

    public List<string> getOrderList()
    {
        return attachOrder;
    }

    public List<string> getOrderAnchorList()
    {
        return attachOrderAnchor;
    }
}
