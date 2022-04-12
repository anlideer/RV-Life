using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepartUI : MonoBehaviour
{
    [Header("Ojbects")]
    public Text destinationText;
    public Text distanceText;
    public Text typeText;
    public Text beautyText;
    public Text buttonText;
    public TriponMap map;
    [Header("Prefabs")]
    public Text popUpText;

    private Route route;
    private Transform canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
        destinationText.text = string.Format("Destination: unselected");
        distanceText.text = string.Format("Distance: ");
        typeText.text = string.Format("Road type: ");
        beautyText.text = string.Format("Road scenario: ");
    }

    public void SelectDestination(Route r)
    {
        route = r;
        destinationText.text = string.Format("Destination: {0}", r.destination.cityName);
        distanceText.text = string.Format("Distance: {0}km", r.distance);
        typeText.text = string.Format("Road type: {0}", r.GetRouteTypeString());
        beautyText.text = string.Format("Road scenario: {0}", r.GetBeautyString());
    }

    // depart
    public void Depart()
    {
        if (route == null)
        {
            Text popUp = Instantiate(popUpText, canvas);
            popUp.text = "Destination not selected";
            Destroy(popUp.gameObject, 2f);
        }
        else
        {
            map.StartTrip(route);
        }
    }

}
