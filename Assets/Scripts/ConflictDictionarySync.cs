using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictDictionarySync : MonoBehaviour {

    //Dictionary <nameofobject, Dictionary<position_in, List<conflictPair>>>
    Dictionary <string, List<ConflictPair>> conflictList;
	// Use this for initialization
	void Start () {
        conflictList = new Dictionary<string,  List<ConflictPair>>();

        List<ConflictPair> conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Anchor Point 1", "Exterior Anchor Point 1", "Exter Component Capsule"));
        conflicts.Add(new ConflictPair("Anchor Point 2", "Exterior Anchor Point 2", "Exter Component Capsule"));
        conflicts.Add(new ConflictPair("Anchor Point 3", "Exterior Anchor Point 3", "Exter Component Capsule"));
        conflicts.Add(new ConflictPair("Anchor Point 4", "Exterior Anchor Point 4", "Exter Component Capsule"));

        conflictList.Add("Inner Component Capsule", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Anchor Point 1", "Exterior Anchor Point 1", "Exter Component Cube"));
        conflicts.Add(new ConflictPair("Anchor Point 2", "Exterior Anchor Point 2", "Exter Component Cube"));
        conflicts.Add(new ConflictPair("Anchor Point 3", "Exterior Anchor Point 3", "Exter Component Cube"));
        conflicts.Add(new ConflictPair("Anchor Point 4", "Exterior Anchor Point 4", "Exter Component Cube"));

        conflictList.Add("Inner Component Cube", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Anchor Point 1", "Exterior Anchor Point 1", "Exter Component Cylinder"));
        conflicts.Add(new ConflictPair("Anchor Point 2", "Exterior Anchor Point 2", "Exter Component Cylinder"));
        conflicts.Add(new ConflictPair("Anchor Point 3", "Exterior Anchor Point 3", "Exter Component Cylinder"));
        conflicts.Add(new ConflictPair("Anchor Point 4", "Exterior Anchor Point 4", "Exter Component Cylinder"));

        conflictList.Add("Inner Component Cylinder", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Anchor Point 1", "Exterior Anchor Point 1", "Exter Component Sphere"));
        conflicts.Add(new ConflictPair("Anchor Point 2", "Exterior Anchor Point 2", "Exter Component Sphere"));
        conflicts.Add(new ConflictPair("Anchor Point 3", "Exterior Anchor Point 3", "Exter Component Sphere"));
        conflicts.Add(new ConflictPair("Anchor Point 4", "Exterior Anchor Point 4", "Exter Component Sphere"));

        conflictList.Add("Inner Component Sphere", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Exterior Anchor Point 1", "Anchor Point 1", "Inner Component Sphere"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 2", "Anchor Point 2", "Inner Component Sphere"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 3", "Anchor Point 3", "Inner Component Sphere"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 4", "Anchor Point 4", "Inner Component Sphere"));

        conflictList.Add("Exter Component Sphere", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Exterior Anchor Point 1", "Anchor Point 1", "Inner Component Cube"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 2", "Anchor Point 2", "Inner Component Cube"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 3", "Anchor Point 3", "Inner Component Cube"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 4", "Anchor Point 4", "Inner Component Cube"));

        conflictList.Add("Exter Component Cube", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Exterior Anchor Point 1", "Anchor Point 1", "Inner Component Cylinder"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 2", "Anchor Point 2", "Inner Component Cylinder"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 3", "Anchor Point 3", "Inner Component Cylinder"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 4", "Anchor Point 4", "Inner Component Cylinder"));

        conflictList.Add("Exter Component Cylinder", conflicts);

        // ----------------------------------------------------------------------------------------------------

        conflicts = new List<ConflictPair>();

        conflicts.Add(new ConflictPair("Exterior Anchor Point 1", "Anchor Point 1", "Inner Component Capsule"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 2", "Anchor Point 2", "Inner Component Capsule"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 3", "Anchor Point 3", "Inner Component Capsule"));
        conflicts.Add(new ConflictPair("Exterior Anchor Point 4", "Anchor Point 4", "Inner Component Capsule"));

        conflictList.Add("Exter Component Capsule", conflicts);

    }

    public Dictionary<string, List<ConflictPair>> getConflictDictionary()
    {
        return conflictList;
    }

}
