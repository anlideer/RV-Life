using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriponMap : MonoBehaviour
{
    [Header("Objects")]
    public GameObject pin;
    public DepartUI departUI;
    [Header("Setting")]
    public float moveDis = 10f;

    private MapController mapCtrl;
    private Dictionary<string, GameObject> nodeDic = new Dictionary<string, GameObject>();

    private bool isMoving = false;
    private PinLocationCalculator calculator;

    private void Start()
    {
        isMoving = false;
        mapCtrl = GetComponent<MapController>();
        // get nodes
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (var node in nodes)
        {
            nodeDic[node.name] = node;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            Moving();
        }
    }

    // depart
    public void StartTrip(Route r)
    {
        mapCtrl.LockMap(true);
        mapCtrl.FocusCurrent();
        GlobalStates.currentLocation.GoOnRoad(r);
        calculator = new PinLocationCalculator(GlobalStates.currentLocation, nodeDic);
        isMoving = true;
    }

    // moving
    public void Moving()
    {
        bool flag = GlobalStates.currentLocation.MoveAlongRoute((int)(moveDis*Time.deltaTime));
        pin.transform.position = calculator.Calculate();

        if (!flag)
        {
            // arrive
            Debug.Log("Arrive");
            isMoving = false;
            GlobalStates.currentLocation.Arrive();
            mapCtrl.LockMap(false);
        }

    }

}
