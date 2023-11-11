using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class ObjectTapToPlace : MonoBehaviour
{
    public GameObject objectToInstantiate; // Объект, который нужно разместить

    private GameObject spawnedObject; // Размещённый объект
    private ARRaycastManager arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>(); // Попадания лучей Raycast

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
        // Здесь допом можно сделать анимацию с пандой, где она говорит, что туда, куда ты нажимаешь, еще не отсканировано
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition)) return;

        if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(objectToInstantiate, hitPose.position, hitPose.rotation);
            }
            else            // Если объект уже был размещён, можно его передвигать пальчиком
            {
                spawnedObject.transform.position = hitPose.position;
            }
        }
    }
}
