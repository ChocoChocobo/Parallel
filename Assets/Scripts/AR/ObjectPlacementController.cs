using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.OpenXR.Input;

public class ObjectPlacementController : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject objectToPlace;
    [SerializeField] private ARPlaneManager aRPlaneManager;

    private UnityEngine.Pose placementPose;
    private bool placementPoseIsValid = false;
    private GameObject placedObject;

    // ��� ��������� � ���������� Planes
    private bool planesEnabled = false;

    private void Start()
    {
        if (aRPlaneManager == null)
        {
            Debug.LogError("AR Plane Manager �� ��������� � �������");
            return;
        }
        aRPlaneManager.enabled = !planesEnabled;
    }

    private void Update()
    {
        UpdatePlacementPose();
        //UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (placedObject == null) // ���� ������� ��� ������� �� ����� �� ���������� � ������������, �� ��� ���������
            {
                PlaceObject();
            }
            else // �����, ���� ������ ��� ��� ��������, � �� ����� ��������� ������, ������ ���������
            {
                RemoveObject();
            }           
        }
    }

    // ����� ��������� ������������ ������� � ������������
    private void UpdatePlacementPose()
    {
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }       
    }

    // ��� ���������� � ������� 
    /*private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            // ��������� ��� ���������� �������, ����� ���������� ���� �������� ������
        }
        else
        {

        }
    }*/

    private void PlaceObject()
    {
        // �������������� ������ �� ����� Pose, ���� ��� ������� � ������������ � rotation
        placedObject = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);   
    }

    private void RemoveObject()
    {
        if (placedObject != null)
        {
            Destroy(placedObject);
            placedObject = null;
        }
    }
}
