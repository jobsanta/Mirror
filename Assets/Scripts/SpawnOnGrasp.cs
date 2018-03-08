using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using UnityEngine.Networking;

[RequireComponent(typeof(InteractionBehaviour))]
public class SpawnOnGrasp : MonoBehaviour {

    private InteractionBehaviour _intObj;
    Vector3 lockedPosition;
    Quaternion lockedRotation;
    void Start () {
        _intObj = GetComponent<InteractionBehaviour>();

        _intObj.OnGraspBegin += SetStartPosition;
        _intObj.OnGraspEnd += GraspSpawn;


    }

    void SetStartPosition()
    {
        lockedPosition = gameObject.GetComponent<Rigidbody>().position ;
        lockedRotation = gameObject.GetComponent<Rigidbody>().rotation;

        Debug.Log(lockedPosition + " " + lockedRotation);
    }

    void GraspSpawn()
    {

            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            ObjectSpawner spawner = Player.GetComponent<ObjectSpawner>();
            spawner.GraspObjectSpawner(gameObject, lockedPosition, lockedRotation);

        _intObj.OnGraspBegin -= SetStartPosition;
        _intObj.OnGraspEnd -= GraspSpawn;

    }

}
