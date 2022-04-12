using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderUI : MonoBehaviour
{
    [Header("Middle and right texts")]
    public Text moneyText;
    public Text timeText;
    public Text locationText;

    [Header("Left icons")]
    public Image health;
    public Image energy;
    public GameObject fuel;
    public GameObject battery;

    private void Update()
    {
        moneyText.text = GlobalStates.currentMoney.GetStringShown();
        timeText.text = GlobalStates.currentTime.GetStringShown();
        locationText.text = GlobalStates.currentLocation.GetLocationString();
        health.fillAmount = GlobalStates.currentHealth;
        energy.fillAmount = GlobalStates.currentEnergy;
        fuel.GetComponent<PercentDisplay>().SetAmount(GlobalStates.currentFuel);
        battery.GetComponent<PercentDisplay>().SetAmount(GlobalStates.currentBattery);
    }
}
