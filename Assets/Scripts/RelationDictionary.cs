using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationDictionary : MonoBehaviour {

    Dictionary<string, string> relation;
	// Use this for initialization
	void Start () {
        relation = new Dictionary<string, string>();


        relation.Add("Exterior Bathtub Async", "Interior Bathtub Async");
        relation.Add("Exterior Dryer Async", "Interior dryer Async");
        relation.Add("Exterior Kitchen Async", "Interior kitchen Async");
        relation.Add("Exterior Lamp Async", "Interior lamp Async");
        relation.Add("Exterior Sink Async", "Interior sink Async");
        relation.Add("Exterior Toilet Async", "Interior toilet Async");
        relation.Add("Exterior TV Async", "Interior tv Async");
        relation.Add("Exterior Workdesk Async", "Interior workdesk Async");

        relation.Add("Interior Bathtub Async", "Exterior Bathtub Async");
        relation.Add("Interior dryer Async", "Exterior Dryer Async");
        relation.Add("Interior kitchen Async", "Exterior Kitchen Async");
        relation.Add("Interior lamp Async", "Exterior Lamp Async");
        relation.Add("Interior sink Async", "Exterior Sink Async");
        relation.Add("Interior toilet Async", "Exterior Toilet Async");
        relation.Add("Interior tv Async", "Exterior TV Async");
        relation.Add("Interior workdesk Async", "Exterior Workdesk Async");
    }

    public Dictionary<string, string> GetRelationship()
    {
        return relation;
    }
	
}
