using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStates: MonoBehaviour
{
    public static MyTime currentTime = new MyTime(1, 8, 0);
    public static MyMoney currentMoney = new MyMoney(6000f);
    public static MyLocation currentLocation = new MyLocation("Chengdu", LocationDetail.PARKING);
    public static float currentHealth = 1f; // 0-1f
    public static float currentEnergy = 1f; // 0-1f
    public static float currentFuel = 1f;   // 0-1f
    public static float currentBattery = 1f;    // 0-1f
    public static float currentClean = 1f;  // 0-1f
    public static bool isStopped = false;
    public static bool isDriving = false;
    public static bool isSleeping = false;

    public static void CheckConditions()
    {
        if (currentHealth <= 0f)
        {
            List<string> res = new List<string>();
            MyDialogManager.Show("Your health goes to zero...Spend 100 to buy food urgently.");

            if (GlobalStates.currentMoney.Affordable(100))
            {
                GlobalStates.currentTime.TimePass(new MyTime(0, 1, 0));
                GlobalStates.currentHealth = 1f;
                GlobalStates.currentMoney.Spend(100);
                Transform canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
                GameObject obj = Instantiate(Resources.Load("FoodBag") as GameObject, canvas);
                Destroy(obj, 3f);
            }
            else
            {
                MyDialogManager.Show(new List<string> { "You don't have enough money, your journey stops here.", "Game Over." });
                // TODO: back to main menu
            }
        }

        if (currentEnergy <= 0f)
        {
            currentEnergy = 0f;
            MyDialogManager.Show("Your energy goes to zero...Stop and spend 12 hours sleeping...");

            GlobalStates.currentTime.TimePass(new MyTime(0, 12, 0));
            GlobalStates.currentEnergy = 1f;
            Transform canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
            GameObject obj = Instantiate(Resources.Load("Night") as GameObject, canvas);
            Destroy(obj, 3f);
        }

        if (currentFuel <= 0f)
        {
            currentFuel = 0f;
            if (GlobalStates.currentMoney.Affordable(500))
            {

                MyDialogManager.Show(new List<string> { "Your van is out of fuel...Call for help...",  "Spend 500. The van is slightly refueled." });
                GlobalStates.currentMoney.Spend(500);
                GlobalStates.currentTime.TimePass(new MyTime(0, 2, 0));
                GlobalStates.ChangeFuel(0.2f);
                Transform canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
                GameObject obj = Instantiate(Resources.Load("Tier") as GameObject, canvas);
                Destroy(obj, 3f);
            }
            else
            {
                MyDialogManager.Show(new List<string> { "You don't have enough money, your journey stops here.", "Game Over." });
                // TODO: game over
            }
        }

        if (currentBattery <= 0f)
        {
            currentBattery = 0f;
            MyDialogManager.Show("Your van is out of electricity. Use some fuel to get it recharged.");
            GlobalStates.currentTime.TimePass(new MyTime(0, 0, 30));
            GlobalStates.ChangeFuel(-0.1f);
            GlobalStates.ChangeBattery(0.3f);
        }

        // TODO: deal with cleanness, and van states (water system) warning and consequences.
        // i.e. low cleanness can cause energy going down faster.
        /*
        if (currentClean <= 0f)
        {
            currentClean = 0f;
            res += "Your cleanness goes to zero.\n";
        }
        */

    }

    // driving decrease energy and fuel
    public static void Driving(float health_consumed, float energy_consumed, float fuel_consumed, float clean_consumed, float battery_gain)
    {
        ChangeHealth(-health_consumed);
        ChangeEnergy(-energy_consumed);
        ChangeFuel(-fuel_consumed);
        ChangeClean(-clean_consumed);
        ChangeBattery(battery_gain);

        CheckConditions();
    }

    public static void ChangeHealth(float change)
    {
        currentHealth += change;
        if (currentHealth > 1f)
            currentHealth = 1f;
        else if (currentHealth < 0f)
            currentHealth = 0f;
    }

    public static void ChangeEnergy(float change)
    {
        currentEnergy += change;
        if (currentEnergy > 1f)
            currentEnergy = 1f;
        else if (currentEnergy < 0f)
            currentEnergy = 0f;
    }

    public static void ChangeFuel(float change)
    {
        currentFuel += change;
        if (currentFuel > 1f)
            currentFuel = 1f;
        else if (currentFuel < 0f)
            currentFuel = 0f;
    }

    public static void ChangeBattery(float change)
    {
        currentBattery += change;
        if (currentBattery > 1f)
            currentBattery = 1f;
        else if (currentBattery < 0f)
            currentBattery = 0f;
    }

    public static void ChangeClean(float change)
    {
        currentClean += change;
        if (currentClean > 1f)
            currentClean = 1f;
        else if (currentClean < 0f)
            currentClean = 0f;
    }

}
