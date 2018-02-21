using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationDictionary : MonoBehaviour {

    Dictionary<string, string> relation;
	// Use this for initialization
	void Start () {
        relation = new Dictionary<string, string>();

        relation.Add("Exter Component Capsule Async", "Inner Component Capsule Async");
        relation.Add("Exter Component Cube Async", "Inner Component Cube Async");
        relation.Add("Exter Component Cylinder Async", "Inner Component Cylinder Async");
        relation.Add("Exter Component Sphere Asyn", "Inner Component Sphere Asyn");


        relation.Add( "Inner Component Capsule Async", "Exter Component Capsule Async");
        relation.Add( "Inner Component Cube Async", "Exter Component Cube Async");
        relation.Add( "Inner Component Cylinder Async", "Exter Component Cylinder Async");
        relation.Add( "Inner Component Sphere Asyn", "Exter Component Sphere Asyn");
    }

    public Dictionary<string, string> getRelationship()
    {
        return relation;
    }
	
}
