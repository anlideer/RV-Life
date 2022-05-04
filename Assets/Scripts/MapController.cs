using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Pin object")]
    public GameObject pin;
    [Header("Prefab")]
    public GameObject arrowPrefab;
    [Header("Camera movement")]
    public Transform leftTop;
    public Transform rightBottom;
    public float sensitivity = 0.2f;
    [Header("Zoom arguments")]
    public float zoomInSize = 1.5f;
    public float zoomInLimit = 1f;
    public float zoomOutLimit = 5f;
    public float zoomInterval = 0.2f;
    [Header("Depart UI")]
    public DepartUI departUI;

    private Dictionary<string, GameObject> nodeDic = new Dictionary<string, GameObject> ();
    private GameObject currentNode;
    private Camera cam;

    private bool isDragging = false;
    private Vector3 camPos;
    private Vector3 mousePos;
    private Dictionary<string, GameObject> arrows = new Dictionary<string, GameObject>();
    private GameObject selectedDes;

    private bool mapLocked = false;
    

    private void Awake()
    {
        mapLocked = false;
        // get nodes
        cam = Camera.main;
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach(var node in nodes)
        {
            nodeDic[node.name] = node;
        }
        currentNode = nodeDic[GlobalStates.currentLocation.cityName];
    }

    private void OnEnable()
    {
        currentNode = nodeDic[GlobalStates.currentLocation.cityName];
        PlacePin();
        ZoomIntoCurrent();
        camPos = cam.transform.position;
        ShowRoutesFromCurrent();
    }

    private void Update()
    {
        if (mapLocked)
            return;

        DetectZoom();
        DetectDrag();
        if (isDragging)
            DragByMouse();
    }

    // lock map
    public void LockMap(bool islocked)
    {
        mapLocked = islocked;
        isDragging = false;
    }

    // focus
    public void FocusCurrent()
    {
        Vector3 pos = currentNode.transform.position;
        var r = departUI.route;
        if (r != null)
        {
            Debug.Log(pos);
            Vector3 des = nodeDic[r.destination.cityName].transform.position;
            Debug.Log(des);
            pos = (pos + des) / 2;
        }
        
        pos.z = cam.transform.position.z;
        cam.transform.position = pos;
        cam.orthographicSize = zoomInLimit;
    }

    // delete all arrows and show new arrows
    public void ShowNewCurrent()
    {
        currentNode = nodeDic[GlobalStates.currentLocation.cityName];
        selectedDes = null;
        foreach(var a in arrows.Values)
        {
            Destroy(a);
        }
        arrows.Clear();
        ShowRoutesFromCurrent();
    }

    // delete all arrows except chosen one
    public void DeleteArrows()
    {
        if (selectedDes == null)
            return;
        foreach(string key in arrows.Keys)
        {
            if (key != selectedDes.name)
            {
                Destroy(arrows[key]);
            }    
        }
    }

    private void PlacePin()
    {
        string location = GlobalStates.currentLocation.cityName;
        if (nodeDic.ContainsKey(location))
        {
            currentNode = nodeDic[location];
            var calculator = new PinLocationCalculator(GlobalStates.currentLocation, nodeDic);
            pin.transform.position = calculator.Calculate();
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
            DetectClickNode();
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

    // init an arrow in right position
    private GameObject CreateArrow(Transform a, Transform b)
    {
        GameObject obj = Instantiate(arrowPrefab);
        ArrowCtrl ac = obj.GetComponent<ArrowCtrl>();
        ac.startPoint = a;
        ac.endPoint = b;
        ac.UpdatePointing();
        return obj;
    }

    // show every route from current node
    private void ShowRoutesFromCurrent()
    {
        if (GlobalStates.currentLocation.route == null)
        {
            currentNode = nodeDic[GlobalStates.currentLocation.cityName];
            List<Route> routes = currentNode.GetComponent<Node>().routes;
            foreach (var route in routes)
            {
                Node n = route.destination;
                if (nodeDic.ContainsKey(n.cityName))
                {
                    GameObject obj = CreateArrow(currentNode.transform, nodeDic[n.cityName].transform);
                    arrows[n.cityName] = obj;
                }
            }
        }
        else
        {
            var route = GlobalStates.currentLocation.route;
            currentNode = nodeDic[GlobalStates.currentLocation.cityName];
            GameObject obj = CreateArrow(currentNode.transform, nodeDic[route.destination.cityName].transform);
            obj.GetComponent<ArrowCtrl>().SetToRed(true);
            arrows[route.destination.cityName] = obj;
        }

    }

    // detect click node
    private void DetectClickNode()
    {
        bool flag = false;
        RaycastHit2D[] hitObjs = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        foreach (var hit in hitObjs)
        {
            if (hit.collider.tag == "Node")
            {
                if (arrows.ContainsKey(hit.collider.name))
                {
                    selectedDes = hit.collider.gameObject;
                    flag = true;
                    break;
                }
            }
        }

        if (flag)
        {
            foreach (string city in arrows.Keys)
            {
                if (city != selectedDes.name)
                    arrows[city].GetComponent<ArrowCtrl>().SetToRed(false);
                else
                    arrows[city].GetComponent<ArrowCtrl>().SetToRed(true);
            }

            // update depart ui
            foreach(var r in currentNode.GetComponent<Node>().routes)
            {
                if (r.destination.name == selectedDes.name)
                {
                    departUI.SelectDestination(r);
                    break;
                }
            }    
        }

    }
}
