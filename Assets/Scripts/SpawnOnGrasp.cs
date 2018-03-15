using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using UnityEngine.Networking;

[RequireComponent(typeof(InteractionBehaviour))]
public class SpawnOnGrasp : MonoBehaviour {

    private InteractionBehaviour _intObj;
    private AnchorableBehaviour _AncObj;
    Vector3 lockedPosition;
    Quaternion lockedRotation;
    static int globalCount = 0;
    void Start () {
        _intObj = GetComponent<InteractionBehaviour>();
        _AncObj = GetComponent<AnchorableBehaviour>();
        _intObj.OnGraspBegin += SetStartPosition;
        _AncObj.OnAttachedToAnchor += OnAnchor;

    }

    void SetStartPosition()
    {
        lockedPosition = gameObject.GetComponent<Rigidbody>().position ;
        lockedRotation = gameObject.GetComponent<Rigidbody>().rotation;

        globalCount++;
        gameObject.name = gameObject.name + globalCount.ToString();
        _intObj.OnGraspBegin -= SetStartPosition;

    }

    
    void OnAnchor()
    {


        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        ObjectSpawner spawner = Player.GetComponent<ObjectSpawner>();
        spawner.GraspObjectSpawner(gameObject, lockedPosition, lockedRotation);

        _AncObj.OnAttachedToAnchor -= OnAnchor;
    }

}
