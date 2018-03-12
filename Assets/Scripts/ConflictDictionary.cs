using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConflictDictionary : MonoBehaviour {

    //Dictionary <nameofobject, Dictionary<position_in, List<conflictPair>>>
    Dictionary <string, string> conflictList;
	// Use this for initialization
	void Start () {
        conflictList = new Dictionary<string, string>();
        conflictList.Add("Exterior Bathtub Async", "Interior Bathtub Async");
        conflictList.Add("Exterior Dryer Async", "Interior dryer Async");
        conflictList.Add("Exterior Kitchen Async", "Interior kitchen Async");
        conflictList.Add("Exterior Lamp Async", "Interior lamp Async");
        conflictList.Add("Exterior Sink Async", "Interior sink Async");
        conflictList.Add("Exterior Toilet Async", "Interior toilet Async");
        conflictList.Add("Exterior TV Async", "Interior tv Async");
        conflictList.Add("Exterior Workdesk Async", "Interior workdesk Async");

        conflictList.Add("Interior Bathtub Async", "Exterior Bathtub Async");
        conflictList.Add("Interior dryer Async", "Exterior Dryer Async");
        conflictList.Add("Interior kitchen Async", "Exterior Kitchen Async");
        conflictList.Add("Interior lamp Async", "Exterior Lamp Async");
        conflictList.Add("Interior sink Async", "Exterior Sink Async");
        conflictList.Add("Interior toilet Async", "Exterior Toilet Async");
        conflictList.Add("Interior tv Async", "Exterior TV Async");
        conflictList.Add("Interior workdesk Async", "Exterior Workdesk Async");

    }

    public Dictionary<string, string> getConflictDictionary()
    {
        return conflictList;
    }

}
