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
        // double the speed
        int fac = PlayerPrefs.GetInt("Speed", 1);
        d = d * fac;

        // TODO: make it like a setting file
        // consume energy and fuel
        // default: highway
        float energyCell = 0.0003f;
        float fuelCell = 0.0015f;    
        float healthCell = 0.0003f;  // starvation
        float cleanCell = 0.0000375f;
        float batteryCell = 0.001f;
        float sp = 0.65f;   // bigger, slower
        var rt = GlobalStates.currentLocation.route.routeType;
        if (rt == RouteType.MountainUp)
        {
            sp = 0.7f;
            fuelCell = 0.002f;
        }
        else if (rt == RouteType.MountainDown)
        {
            sp = 0.7f;
            fuelCell = 0.001f;
        }
        else if (rt == RouteType.Normal)
        {
            sp = 0.8f;
        }
        GlobalStates.Driving(healthCell*d, energyCell * d, fuelCell * d, cleanCell*d, batteryCell*d);

        // consume time
        // deal with weather
        var wt = GlobalStates.currentWeather.GetWeatherNow();
        if (wt == WeatherType.NORMAL)
        {
            GlobalStates.currentTime.TimePassLittle(sp * d);
        }
        else if (wt == WeatherType.RAIN)
        {
            // consume more time, which means the speed is lower
            GlobalStates.currentTime.TimePassLittle(1.2f * sp * d);
        }
        else if (wt == WeatherType.RAINSTORM)
        {
            GlobalStates.currentTime.TimePassLittle(1.3f * sp * d);
        }

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
