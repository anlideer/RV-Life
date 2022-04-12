using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LocationDetail
{
    PARKING, SERVICE, ROAD, ATTRACTION,
}

public class MyLocation
{
    public string cityName;
    public LocationDetail detail;
    public Route route;  // can be empty, only fill when LocationDetail.ROAD or SERVICE
    public int distanceFromStart;

    private Dictionary<LocationDetail, string> detailStrings = new Dictionary<LocationDetail, string>
    {
        {LocationDetail.PARKING, "Parking Lot" },
        {LocationDetail.SERVICE, "Service Station" },
        {LocationDetail.ROAD, "On the way" },
        {LocationDetail.ATTRACTION, "" },
    };

    public MyLocation() { }
    public MyLocation(string cname, LocationDetail det, Route r = null)
    {
        cityName = cname;
        detail = det;
        route = r;
    }

    public void GoOnRoad(Route r, LocationDetail det=LocationDetail.ROAD)
    {
        route = r;
        distanceFromStart = 0;
        detail = det;
    }

    public void Arrive(LocationDetail det = LocationDetail.PARKING)
    {
        cityName = route.destination.cityName;
        detail = det;
        route = null;
        distanceFromStart = 0;
    }
    
    public string GetLocationString()
    {
        string s = "";
        if (route != null)
        {
            s = string.Format("{0}->{1}", cityName, route.destination.cityName);
        }
        else
        {
            s = cityName;
        }
        s += string.Format(" -{0}", detailStrings[detail]);
        return s;
    }

    public bool MoveAlongRoute(int d)
    {
        distanceFromStart += d;
        int dis = route.distance;
        if (distanceFromStart > dis)
        {
            distanceFromStart = dis;
            return false;
        }
        return true;
    }
}
