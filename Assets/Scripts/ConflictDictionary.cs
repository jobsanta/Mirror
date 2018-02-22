using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictPair
{
    public string Conflict_in { get; set; }
    public string Conflict_out { get; set; }
    public string Conflict_name { get; set; }

    public ConflictPair(string conflictin, string conflictout, string name)
    {
        Conflict_in = conflictin;
        Conflict_out = conflictout;
        Conflict_name = name;
    }
}

public class ConflictDictionary : MonoBehaviour {

    //Dictionary <nameofobject, Dictionary<position_in, List<conflictPair>>>
    Dictionary <string, List<ConflictPair>> conflictList;
	// Use this for initialization
	void Start () {
        conflictList = new Dictionary<string,  List<ConflictPair>>();

        List<ConflictPair> conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Anchor Point 1", "Exterior Anchor Point 1", "Exter Component Capsule Async"));
        conflicts.Add(new ConflictPair("Anchor Point 2", "Exterior Anchor Point 2", "Exter Component Capsule Async"));
        conflicts.Add(new ConflictPair("Anchor Point 3", "Exterior Anchor Point 3", "Exter Component Capsule Async"));
        conflicts.Add(new ConflictPair("Anchor Point 4", "Exterior Anchor Point 4", "Exter Component Capsule Async"));

        conflictList.Add("Inner Component Capsule Async", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Anchor Point 1", "Exterior Anchor Point 1", "Exter Component Cube Async"));
        conflicts.Add(new ConflictPair("Anchor Point 2", "Exterior Anchor Point 2", "Exter Component Cube Async"));
        conflicts.Add(new ConflictPair("Anchor Point 3", "Exterior Anchor Point 3", "Exter Component Cube Async"));
        conflicts.Add(new ConflictPair("Anchor Point 4", "Exterior Anchor Point 4", "Exter Component Cube Async"));

        conflictList.Add("Inner Component Cube Async", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Anchor Point 1", "Exterior Anchor Point 1", "Exter Component Cylinder Async"));
        conflicts.Add(new ConflictPair("Anchor Point 2", "Exterior Anchor Point 2", "Exter Component Cylinder Async"));
        conflicts.Add(new ConflictPair("Anchor Point 3", "Exterior Anchor Point 3", "Exter Component Cylinder Async"));
        conflicts.Add(new ConflictPair("Anchor Point 4", "Exterior Anchor Point 4", "Exter Component Cylinder Async"));

        conflictList.Add("Inner Component Cylinder Async", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Anchor Point 1", "Exterior Anchor Point 1", "Exter Component Sphere Async"));
        conflicts.Add(new ConflictPair("Anchor Point 2", "Exterior Anchor Point 2", "Exter Component Sphere Async"));
        conflicts.Add(new ConflictPair("Anchor Point 3", "Exterior Anchor Point 3", "Exter Component Sphere Async"));
        conflicts.Add(new ConflictPair("Anchor Point 4", "Exterior Anchor Point 4", "Exter Component Sphere Async"));

        conflictList.Add("Inner Component Sphere Async", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Exterior Anchor Point 1", "Anchor Point 1", "Inner Component Sphere Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 2", "Anchor Point 2", "Inner Component Sphere Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 3", "Anchor Point 3", "Inner Component Sphere Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 4", "Anchor Point 4", "Inner Component Sphere Async"));

        conflictList.Add("Exter Component Sphere Async", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Exterior Anchor Point 1", "Anchor Point 1", "Inner Component Cube Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 2", "Anchor Point 2", "Inner Component Cube Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 3", "Anchor Point 3", "Inner Component Cube Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 4", "Anchor Point 4", "Inner Component Cube Async"));

        conflictList.Add("Exter Component Cube Async", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Exterior Anchor Point 1", "Anchor Point 1", "Inner Component Cylinder Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 2", "Anchor Point 2", "Inner Component Cylinder Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 3", "Anchor Point 3", "Inner Component Cylinder Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 4", "Anchor Point 4", "Inner Component Cylinder Async"));

        conflictList.Add("Exter Component Cylinder Async", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Exterior Anchor Point 1", "Anchor Point 1", "Inner Component Capsule Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 2", "Anchor Point 2", "Inner Component Capsule Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 3", "Anchor Point 3", "Inner Component Capsule Async"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 4", "Anchor Point 4", "Inner Component Capsule Async"));

        conflictList.Add("Exter Component Capsule Async", conflicts);

    }

    public Dictionary<string, List<ConflictPair>> getConflictDictionary()
    {
        return conflictList;
    }

}
