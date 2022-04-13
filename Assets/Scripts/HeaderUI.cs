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
    public Image clean;

    [Header("Left icons percentage display")]
    public GameObject percentageGroup;
    public Text healthText;
    public Text energyText;
    public Text fuelText;
    public Text batteryText;
    public Text cleanText;

    [Header("Panel")]
    public GameObject settingPanel;


    private void Start()
    {
        settingPanel.SetActive(false);
    }

    private void Update()
    {
        moneyText.text = GlobalStates.currentMoney.GetStringShown();
        timeText.text = GlobalStates.currentTime.GetStringShown();
        locationText.text = GlobalStates.currentLocation.GetLocationString();
        health.fillAmount = GlobalStates.currentHealth;
        energy.fillAmount = GlobalStates.currentEnergy;
        clean.fillAmount = GlobalStates.currentClean;
        fuel.GetComponent<PercentDisplay>().SetAmount(GlobalStates.currentFuel);
        battery.GetComponent<PercentDisplay>().SetAmount(GlobalStates.currentBattery);

        // percentage
        if (PlayerPrefs.GetInt("ShowPercentage", 1) == 1)
        {
            percentageGroup.SetActive(true);
            healthText.text = string.Format("{0}%", (int)(GlobalStates.currentHealth * 100));
            energyText.text = string.Format("{0}%", (int)(GlobalStates.currentEnergy * 100));
            fuelText.text = string.Format("{0}%", (int)(GlobalStates.currentFuel * 100));
            batteryText.text = string.Format("{0}%", (int)(GlobalStates.currentBattery * 100));
            cleanText.text = string.Format("{0}%", (int)(GlobalStates.currentClean * 100));
        }
        else
        {
            percentageGroup.SetActive(false);
        }
        
    }

    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
    }
}
