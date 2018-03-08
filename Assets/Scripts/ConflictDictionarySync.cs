using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictDictionarySync : MonoBehaviour {

    //Dictionary <nameofobject, Dictionary<position_in, List<conflictPair>>>
    Dictionary<string, string> conflictList = new Dictionary<string, string>();

    // Use this for initialization
    void Start () {

        conflictList.Add("Exterior Bathtub", "Interior Bathtub");
        conflictList.Add("Exterior Dryer", "Interior dryer");
        conflictList.Add("Exterior Kitchen", "Interior kitchen");
        conflictList.Add("Exterior Lamp", "Interior lamp");
        conflictList.Add("Exterior Sink", "Interior sink");
        conflictList.Add("Exterior Toilet", "Interior toilet");
        conflictList.Add("Exterior TV", "Interior tv");
        conflictList.Add("Exterior Workdesk", "Interior workdesk");

        conflictList.Add( "Interior Bathtub", "Exterior Bathtub");
        conflictList.Add("Interior dryer","Exterior Dryer");
        conflictList.Add("Interior kitchen","Exterior Kitchen");
        conflictList.Add("Interior lamp","Exterior Lamp");
        conflictList.Add( "Interior sink", "Exterior Sink");
        conflictList.Add("Interior toilet", "Exterior Toilet");
        conflictList.Add( "Interior tv", "Exterior TV");
        conflictList.Add( "Interior workdesk", "Exterior Workdesk");

    }

    public Dictionary<string, string> GetConflictDictionary()
    {
        return conflictList;
    }

}
