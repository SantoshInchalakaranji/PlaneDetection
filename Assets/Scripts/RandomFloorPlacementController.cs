using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
[RequireComponent(typeof(ARRaycastManager))]


public class RandomFloorPlacementController: MonoBehaviour
{
private GameObject spawnNew;

private ARRaycastManager arRaycastManager;
private ARPlaneManager arPlaneManager;
     [SerializeField]

     public GameObject[] objectArray;

static List<ARRaycastHit> hits= new List<ARRaycastHit>();
void Awake()
{
    arRaycastManager = GetComponent<ARRaycastManager>();
    arPlaneManager = GetComponent < ARPlaneManager > ();
}
bool TryGetTouchPosition(out Vector3 touchPosition)
{
    if (Input.touchCount > 0)
    {
        touchPosition =Input.GetTouch(0).position;
        return true;
    }

    touchPosition =default;
    return false;
}
void Update()
{
    if (!TryGetTouchPosition(out Vector3 touchPosition))
        return;

    if (Input.touchCount > 0 && Input.touches[0].phase==TouchPhase.Began)
{
    if (arRaycastManager.Raycast(touchPosition, hits,TrackableType.Planes))
    {
        var hitPose =hits[0].pose;
        foreach (var plane in arPlaneManager.trackables)
        { 
                    plane.gameObject.SetActive(false);
        }
        arPlaneManager.enabled = false;

        //spawnNew Instantiate(objectToSpawn, hit Pose.position, hit Pose.rotation);
        spawnNew =Instantiate(objectArray[Random.Range(0, objectArray.Length)], hitPose.position, hitPose.rotation);
    }
}
}
}