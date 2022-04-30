using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasStationPanel : MonoBehaviour
{
    //[Header("Objects")]

    [Header("Settings")]
    public float fuelPrice = 5.6f;  // 0.01fuel->5.6yuan
    public float foodPrice = 20f;
    public float showerPrice = 10f;
    public float foodAmount = 0.5f;
    public float showerEnergyAmount = 0.1f;

    [Header("Prefabs")]
    public GameObject refuelPic;
    public Text popUpText;

    private Transform canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
    }


    // refuel
    public void RefuelAll()
    {
        float amount = 1f - GlobalStates.currentFuel;
        float cost = fuelPrice * amount * 100;
        // check affordable
        if (GlobalStates.currentMoney.Affordable(cost))
        {
            GlobalStates.currentTime.TimePass(new MyTime(0, 0, 20));
            GlobalStates.currentMoney.Spend(cost);
            GlobalStates.currentFuel = 1f;
            // show pic
            GameObject obj = Instantiate(refuelPic, canvas);
            Destroy(obj, 3f);
            MyDialogManager.Show(string.Format("Cost ¥{0}.", (int)cost));
        }
        else
        {
            MyDialogManager.Show(string.Format("It will cost ¥{0}. You don't have enough money", (int)cost));
        }


    }

    // buy food and eat
    public void Eat()
    {
        if (GlobalStates.currentMoney.Affordable(foodPrice))
        {
            GlobalStates.currentTime.TimePass(new MyTime(0, 1, 0));
            GlobalStates.ChangeHealth(foodAmount);
            GlobalStates.currentMoney.Spend(foodPrice);
            MyDialogManager.Show(string.Format("Eating/speed:down/....../speed:init/ Cost ¥{0}.", (int)foodPrice));
        }
        else
        {
            MyDialogManager.Show(string.Format("It will cost ¥{0}. You don't have enough money.", (int)foodPrice));
        }


    }

    // shower
    public void TakeShower()
    {
        if (GlobalStates.currentMoney.Affordable(showerPrice))
        {
            GlobalStates.currentTime.TimePass(new MyTime(0, 0, 30));
            GlobalStates.ChangeEnergy(showerEnergyAmount);
            GlobalStates.currentClean = 1f;
            GlobalStates.currentMoney.Spend(10f);
            MyDialogManager.Show(string.Format("Showering/speed:down/....../speed:init/ Cost ¥{0}.", (int)showerPrice));
        }
        else
        {
            MyDialogManager.Show(string.Format("It will cost ¥{0}. You don't have enough money.", (int)showerPrice));
        }


    }
}
