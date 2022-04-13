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

    public static void CheckConditions()
    {
        string res = "";
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            res += "Your health goes to zero.\n";
        }

        if (currentEnergy <= 0f)
        {
            currentEnergy = 0f;
            res += "Your energy goes to zero.\n";
        }

        if (currentFuel <= 0f)
        {
            currentFuel = 0f;
            res += "Your van is out of fuel.\n";
        }

        if (currentBattery <= 0f)
        {
            currentBattery = 0f;
            res += "Your van is out of electricity.\n";
        }

        if (currentClean <= 0f)
        {
            currentClean = 0f;
            res += "Your cleanness goes to zero.\n";
        }


        if (res != "")
        {
            MyDialogManager.Show(res);
        }
    }

    // driving decrease energy and fuel
    public static void Driving(float health_consumed, float energy_consumed, float fuel_consumed, float clean_consumed)
    {
        currentHealth -= health_consumed;
        currentEnergy -= energy_consumed;
        currentFuel -= fuel_consumed;
        currentClean -= clean_consumed;
        if (currentEnergy < 0f)
            currentEnergy = 0f;
        if (currentFuel < 0f)
            currentFuel = 0f;
    }
}
