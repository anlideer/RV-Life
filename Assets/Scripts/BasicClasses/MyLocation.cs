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
    public float distanceFromStart;

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
        if (GlobalStates.currentLocation.detail != LocationDetail.SERVICE)
        {
            route = r;
            distanceFromStart = 0;
            detail = det;
        }
        // continue journey from gas station
        else
        {
            detail = det;
        }
    }

    public void Arrive(LocationDetail det = LocationDetail.PARKING)
    {
        cityName = route.destination.cityName;
        detail = det;
        route = null;
        distanceFromStart = 0;
    }

    public void StopAtStation()
    {
        detail = LocationDetail.SERVICE;
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

    public bool MoveAlongRoute(float d)
    {
        // TODO: make it like a seeting
        // consume energy and fuel
        float energyCell = 0.0008f;
        float fuelCell = 0.0015f;    
        float healthCell = 0.0003f;  // starvation
        float cleanCell = 0.0000375f;
        GlobalStates.Driving(healthCell*d, energyCell * d, fuelCell * d, cleanCell*d);

        // consume time
        GlobalStates.currentTime.TimePassLittle(0.65f * d);

        // add
        distanceFromStart += d;
        float dis = route.distance;
        if (distanceFromStart > dis)
        {
            distanceFromStart = dis;
            return false;
        }
        return true;
    }
}
