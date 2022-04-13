using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DepartUI : MonoBehaviour
{
    [Header("Ojbects")]
    public Text destinationText;
    public Text distanceText;
    public Text typeText;
    public Text beautyText;
    public TriponMap map;
    public GameObject departBtn;
    public GameObject stopBtn;
    public GameObject backToVanBtn;
    [Header("Prefabs")]
    public Text popUpText;

    private Route route;
    private Transform canvas;
    private bool isMoving = false;

    private void Start()
    {
        isMoving = false;
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
        ShowInitial();
    }

    private void Update()
    {
        if (isMoving)
        {
            ShowUpdatingInfo();
        }
    }


    public void SetMovingStatus(bool m)
    {
        isMoving = m;
        // is moving
        if (m)
        {
            stopBtn.SetActive(true);
            departBtn.SetActive(false);
            backToVanBtn.SetActive(false);
        }
        // stop
        else
        {
            ShowInitial();
        }
    }

    private void ShowInitial()
    {
        // in city
        if (GlobalStates.currentLocation.route == null)
        {
            destinationText.text = string.Format("Destination: unselected");
            distanceText.text = string.Format("Distance: ");
            typeText.text = string.Format("Road type: ");
            beautyText.text = string.Format("Road scenario: ");
        }
        // on the road
        else
        {
            SelectDestination(GlobalStates.currentLocation.route);
            ShowUpdatingInfo();
        }


        departBtn.SetActive(true);
        stopBtn.SetActive(false);
        backToVanBtn.SetActive(true);
    }

    private void ShowUpdatingInfo()
    {
        int remainedDis = (int)(GlobalStates.currentLocation.route.distance - GlobalStates.currentLocation.distanceFromStart);
        distanceText.text = string.Format("Distance remained: {0}km", remainedDis);
    }

    public void SelectDestination(Route r)
    {
        route = r;
        destinationText.text = string.Format("Destination: {0}", r.destination.cityName);
        distanceText.text = string.Format("Distance: {0}km", (int)r.distance);
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

    // stop at the next service station
    public void StopAtStation()
    {
        int tmp = map.StopAtNextStation();
        Text popUp = Instantiate(popUpText, canvas);
        popUp.text = string.Format("Will stop at the next available gas station ({0}km)", tmp);
        Destroy(popUp.gameObject, 2f);
        stopBtn.SetActive(false); // can't press again before arrival at station
    }


    // back to van page
    public void BackToVan()
    {
        SceneManager.LoadScene("RV");
    }
}
