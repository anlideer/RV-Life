using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Pin object")]
    public GameObject pin;
    [Header("Camera movement")]
    public Transform leftTop;
    public Transform rightBottom;
    public float sensitivity = 0.2f;
    [Header("Zoom arguments")]
    public float zoomInSize = 1.5f;
    public float zoomInLimit = 1f;
    public float zoomOutLimit = 5f;
    public float zoomInterval = 0.2f;

    private GameObject[] nodes;
    private GameObject currentNode;
    private Camera cam;

    private bool isDragging = false;
    private Vector3 camPos;
    private Vector3 mousePos;
    

    private void Start()
    {
        // get nodes
        cam = Camera.main;
        nodes = GameObject.FindGameObjectsWithTag("Node");
        PlacePin();
        ZoomIntoCurrent();
        camPos = cam.transform.position;
    }

    private void Update()
    {
        DetectZoom();
        DetectDrag();
        if (isDragging)
            DragByMouse();
    }

    private void PlacePin()
    {
        string location = GlobalStates.currentLocation.cityName;
        foreach(var node in nodes)
        {
            if (node.name == location)
            {
                pin.transform.position = node.transform.position;
                currentNode = node;
                break;
            }
        }
    }

    private void ZoomIntoCurrent()
    {
        Vector3 pos = currentNode.transform.position;
        pos.z = cam.transform.position.z;
        cam.transform.position = pos;
        cam.orthographicSize = zoomInSize;
    }

    private void DetectZoom()
    {
        float mouseVal = Input.mouseScrollDelta.y;
        // zoom in
        if (mouseVal > 0)
        {
            float newSize = cam.orthographicSize - zoomInterval;
            if (newSize < zoomInLimit)
                newSize = zoomInLimit;
            cam.orthographicSize = newSize;
        }
        else if (mouseVal < 0)
        {
            float newSize = cam.orthographicSize + zoomInterval;
            if (newSize > zoomOutLimit)
                newSize = zoomOutLimit;
            cam.orthographicSize = newSize;
        }
    }

    private void DetectDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            camPos = cam.transform.position;
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            camPos = cam.transform.position;
        }
    }

    private void DragByMouse()
    {
        Vector3 offsetVec = Input.mousePosition - mousePos;
        Vector3 movement = -(sensitivity / cam.orthographicSize) * offsetVec * GetCameraMovementPenalty();
        Vector3 newPos = camPos + movement;
        newPos.z = cam.transform.position.z;
        // check if it's out of border
        newPos = GetCameraValidPos(newPos, movement);
        cam.transform.position = newPos;
    }

    private Vector3 GetCameraValidPos(Vector3 pos, Vector3 offset)
    {
        // compute cam borders
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        // left
        if (offset.x < 0 && pos.x - width / 2 < leftTop.position.x)
            pos.x = leftTop.position.x + width / 2;
        // right
        if (offset.x > 0 && pos.x + width / 2 > rightBottom.position.x)
            pos.x = rightBottom.position.x - width / 2;
        // bottom
        if (offset.y < 0 && pos.y - height / 2 < rightBottom.position.y)
            pos.y = rightBottom.position.y + height / 2;
        // top
        if (offset.y > 0 && pos.y + height / 2 > leftTop.position.y)
            pos.y = leftTop.position.y - height / 2;

        return pos;
    }

    // to make it move smoother, give differnet penality according to the camera size
    private float GetCameraMovementPenalty()
    {
        if (cam.orthographicSize <= 1.3f)
            return 0.3f;
        else if (cam.orthographicSize <= 1.5f)
            return 0.5f;
        else if (cam.orthographicSize >= 2f && cam.orthographicSize < 3f)
            return 1.2f;
        else
            return 1f;
    }
}
