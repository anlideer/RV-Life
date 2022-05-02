using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class VanUI : MonoBehaviour
{
    public GameObject waterPanel;
    public GameObject gasPanel;
    public Text waterText;
    public Text greyText;
    public Text blackText;
    public GameObject delivery;

    private void Start()
    {
        waterPanel.SetActive(false);
    }

    private void Update()
    {
        waterText.text = string.Format("{0}%", (int)(VanStates.waterTank * 100));
        greyText.text = string.Format("{0}%", (int)(VanStates.greyTank * 100));
        blackText.text = string.Format("{0}%", (int)(VanStates.blackTank * 100));
    }

    private void OnEnable()
    {
        if (GlobalStates.currentLocation.detail == LocationDetail.SERVICE)
            gasPanel.SetActive(true);
        else
            gasPanel.SetActive(false);

        if (GlobalStates.currentLocation.detail == LocationDetail.PARKING)
            delivery.SetActive(true);
        else
            delivery.SetActive(false);
    }

    public void OpenWaterPanel()
    {
        waterPanel.SetActive(true);
    }

    public void CloseWaterPanel()
    {
        waterPanel.SetActive(false);
    }

    public void RefillWaterBox(float duration)
    {
        waterPanel.SetActive(false);
        MyDialogManager.Show("Refill water container/speed:down/.../speed:init/");
        GlobalStates.currentTime.TimePass(new MyTime(0, 0, 30));
        VanStates.waterTank = 1f;
    }

    public void EmptyGreyBox(float duration)
    {
        waterPanel.SetActive(false);
        MyDialogManager.Show("Empty grey container/speed:down/.../speed:init/");
        GlobalStates.currentTime.TimePass(new MyTime(0, 0, 30));
        VanStates.greyTank = 0f;
    }

    public void EmptyBlackBox(float duration)
    {
        waterPanel.SetActive(false);
        MyDialogManager.Show("Empty black container/speed:down/.../speed:init/");
        GlobalStates.currentTime.TimePass(new MyTime(0, 0, 30));
        VanStates.blackTank = 0f;
    }

    public void MaintainWaterSystem(float duration)
    {
        waterPanel.SetActive(false);
        // TODO: maintain, check the water system
    }

    // back to map
    public void BackToMap()
    {
        SceneManager.LoadScene("Map");
    }

    // provide different options
    public void Sleep()
    {
        MyDialogManager.SleepDialog();
    }

    public void Shower()
    {
        if (VanStates.waterTank == 0f)
        {
            MyDialogManager.Show("No water left in water container");
            return;
        }

        MyDialogManager.Show("Showering/speed:down/.../speed:init/");
        float e = 0.1f;
        float c = 1f;
        float nor = 0.2f;
        float grey = 0.1f;
        GlobalStates.currentTime.TimePass(new MyTime(0, 0, 30));
        float amount = VanStates.ConsumeWater(nor);
        if (amount < nor)
        {
            MyDialogManager.Show("Water is not enough. Showering effect is not as strong as normal");
            float ratio = amount / nor;
            GlobalStates.ChangeEnergy(e*ratio);
            GlobalStates.ChangeClean(c*ratio);
            VanStates.ProduceGrey(grey*ratio);
        }
        else
        {
            GlobalStates.ChangeEnergy(e);
            GlobalStates.ChangeClean(c);
            VanStates.ProduceGrey(grey);
        }

    }

    public void BuyFoodDelivery()
    {
        if (GlobalStates.currentMoney.Affordable(30))
        {
            MyDialogManager.Show("Eating......Happy to eat local food.");
            GlobalStates.currentTime.TimePass(new MyTime(0, 1, 0));
            GlobalStates.currentMoney.Spend(30);
            GlobalStates.ChangeHealth(0.7f);
            GlobalStates.ChangeEnergy(0.1f);
        }
        else
        {
            MyDialogManager.Show("I can't afford this.");
        }
    }

}
