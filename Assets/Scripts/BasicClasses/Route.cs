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
    public float distance;    // km
    public RouteType routeType;
    public float beauty;    // 0-1

    private Dictionary<RouteType, string> routeNames = new Dictionary<RouteType, string> 
    {
        {RouteType.HighWay, "Highway" },
        {RouteType.Normal, "Normal road" },
        {RouteType.Countryside, "Countryside road" },
        {RouteType.MountainUp, "Mountain road (up)" },
        {RouteType.MountainDown, "Mountain road (down)" },
        {RouteType.MountainNormal, "Mountain road" },
    };

    public string GetRouteTypeString()
    {
        return routeNames[routeType];
    }

    public string GetBeautyString()
    {
        if (beauty < 0.5f)
            return "Normal";
        else if (beauty < 0.8f)
            return "Beautiful";
        else
            return "Amazing";
    }

}
