using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [Header("Santa Position Selector")]
    [Tooltip("Values")]
    [SerializeField] private Transform target;

    [Header("Camera Rotational Values")]
    [Tooltip("Values")]
    public float rotSpeed = 0.5f;
    private float _rotY;
    public float sensitivity = 0.5f;
    // public float speed = 30;
    //private Vector3 _offset;

    [Header("Camera Zoom Values")]
    [Tooltip("Values")]
    [SerializeField] private float zoomLevel;
    [SerializeField] private float maxZoom = 15.0f;
    [SerializeField] private float minZoom = 2.0f;
    // private Vector3 zoomPosition;
    public bool invertControls;
    public PlayerSettingsScriptableObject user_interface;

    void Start() 
    {
        _rotY = transform.eulerAngles.y;
        zoomLevel = 7.0f;
        //Not used, but keeps track of the distance for future purposes
        //_offset = target.position - transform.position;
    }

    void Update()
    {
        invertControls = user_interface.invertCamera;
    }

    //Rotate camera using mouse, zoom using scroll wheel
    void LateUpdate() 
    {        
        CameraRotate();
        CameraZoom();        
        transform.LookAt(target);
    }

    //Rotates around Santa, using the rotational and zoom values for consistent distance from Santa
    void CameraRotate()
    {
        _rotY += Input.GetAxis("Mouse X") * rotSpeed;
        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * new Vector3(-zoomLevel, -zoomLevel, -zoomLevel));
    }

    //Zooms into/out from Santa, restricting values so camera does not go out of bounds
    // Link: https://gamedevbeginner.com/how-to-zoom-a-camera-in-unity-3-methods-with-examples/#movement_zoom
    void CameraZoom()
    {
        //Inverted Controls
        if (invertControls)
        {
            zoomLevel += Input.mouseScrollDelta.y * sensitivity;
        }
        else
        {
            zoomLevel += -Input.mouseScrollDelta.y * sensitivity;
        }
        zoomLevel = Mathf.Clamp(zoomLevel, minZoom, maxZoom);
    }
}
