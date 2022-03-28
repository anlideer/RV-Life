using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RouteType 
{
    HighWay, Normal, Countryside, MountainUp, MountainDown, MountainNormal
};

public class Route
{
    public Node destination;
    public int distance;    // km
    public RouteType routeType;
    public float beauty;    // 0-1
}
