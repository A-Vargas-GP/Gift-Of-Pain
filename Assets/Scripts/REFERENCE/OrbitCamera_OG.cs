using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera_OG : MonoBehaviour
{
    [Header("Santa Position Selector")]
    [Tooltip("Values")]
    [SerializeField] private Transform target;

    [Header("Camera Rotational Speed")]
    [Tooltip("Values")]
    public float rotSpeed = 0.5f;

    private float _rotY;
    private Vector3 _offset;

    void Start() 
    {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;
    }
    
    void LateUpdate() 
    {
        float horInput = Input.GetAxis("Horizontal");
        
        //Rotate camera using mouse
        _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;

        //Rotate camera using Arrow Keys OR Mouse when character is moving
        /*
        if (horInput != 0) 
        {
            _rotY += horInput * rotSpeed * 0.15f;
        } 
        else 
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 2;
        }
        */
        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * _offset);
        transform.LookAt(target);
    }

    //Storing previously tested code in case
    void CameraZoom()
    {
        // zoomPosition = transform.forward * Input.mouseScrollDelta.y * sensitivity;
            // transform.position += zoomPosition;
            // zoomPosition = transform.forward * Input.mouseScrollDelta.y * sensitivity;

        
        /*
            // zoomLevel = Mathf.Clamp(zoomLevel, minZoom, maxZoom);
            zoomPosition = transform.forward * zoomLevel;
            transform.position += zoomPosition;

            // zoomPosition = transform.forward * Input.mouseScrollDelta.y * sensitivity;
            // transform.position += zoomPosition;

            // zoomLevel += Input.mouseScrollDelta.y * sensitivity;
            // zoomPosition = transform.forward * zoomLevel;
            // transform.position = transform.position + zoomPosition;

            //Math Clamp - use this for zooms
            // zoomLevel += Input.mouseScrollDelta.y * sensitivity;
            // zoomLevel = Mathf.Clamp(zoomLevel, -maxZoom, maxZoom);
            // Below is not needed, but remains just in case
            // zoomPosition = Mathf.MoveTowards(zoomPosition, zoomLevel, speed * Time.deltaTime);
        */
    }
}
