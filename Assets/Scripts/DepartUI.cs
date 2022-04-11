using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepartUI : MonoBehaviour
{
    public Text destinationText;
    public Text distanceText;
    public Text typeText;
    public Text beautyText;

    private void Start()
    {
        destinationText.text = string.Format("Destination: unselected");
        distanceText.text = string.Format("Distance: ");
        typeText.text = string.Format("Road type: ");
        beautyText.text = string.Format("Road scenario: ");
    }

    public void SelectDestination(Route r)
    {
        destinationText.text = string.Format("Destination: {0}", r.destination.cityName);
        distanceText.text = string.Format("Distance: {0}km", r.distance);
        typeText.text = string.Format("Road type: {0}", r.GetRouteTypeString());
        beautyText.text = string.Format("Road scenario: {0}", r.GetBeautyString());
    }

}
