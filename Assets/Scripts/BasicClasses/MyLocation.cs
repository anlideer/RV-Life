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
    public string destination;  // can be empty, only fill when LocationDetail.ROAD or SERVICE

    private Dictionary<LocationDetail, string> detailStrings = new Dictionary<LocationDetail, string>
    {
        {LocationDetail.PARKING, "Parking Lot" },
        {LocationDetail.SERVICE, "Service Station" },
        {LocationDetail.ROAD, "On the way" },
        {LocationDetail.ATTRACTION, "" },
    };

    public MyLocation() { }
    public MyLocation(string cname, LocationDetail det, string des="")
    {
        cityName = cname;
        detail = det;
        destination = des;
    }
    
    public string GetLocationString()
    {
        string s = "";
        if (destination != "")
        {
            s = string.Format("{0} -> {1}", cityName, destination);
        }
        else
        {
            s = cityName;
        }
        s += string.Format(" {0}", detailStrings[detail]);
        return s;
    }
}
