using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RouteType 
{
    HighWay=1, Normal=2, Countryside=3, MountainUp=4, MountainDown=5, MountainNormal=6
};

public class Route
{
    public Node destination;
    public int distance;    // km
    public RouteType routeType;
    public float beauty;    // 0-1
}
