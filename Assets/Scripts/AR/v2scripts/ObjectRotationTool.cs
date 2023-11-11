using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationTool : MonoBehaviour
{
    [SerializeField] private GameObject objectToRotate;
    private float rotationAngle = 45f;
    
    public void RotateObject()
    {
        objectToRotate.transform.Rotate(Vector3.up, rotationAngle);
    }
}
