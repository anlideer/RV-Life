using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinLocationCalculator
{
    private MyLocation myloc;
    private Dictionary<string, GameObject> nodeDic = new Dictionary<string, GameObject> ();
    private GameObject startPoint;
    private GameObject endPoint;
    private Vector3 s;
    private Vector3 t;
    private float dis;


    public PinLocationCalculator(MyLocation ml, Dictionary<string, GameObject> nd)
    {
        myloc = ml;
        nodeDic = nd;
        if (ml.route != null)
        {
            startPoint = nd[myloc.cityName];
            endPoint = nd[myloc.route.destination.cityName];
            s = startPoint.transform.position;
            t = endPoint.transform.position;
            dis = Vector3.Distance(s, t);

        }
    }

    // calculate the position on map
    public Vector3 Calculate()
    {
        if (myloc.route == null)
        {
            return nodeDic[myloc.cityName].transform.position;
        }
        float disFromS = dis * myloc.distanceFromStart / myloc.route.distance;
        Vector3 res = s + (t - s).normalized * disFromS;
        res.z = 0;
        return res;
    }
}
