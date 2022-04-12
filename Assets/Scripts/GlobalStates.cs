using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStates: MonoBehaviour
{
    public static MyTime currentTime = new MyTime(1, 8, 0);
    public static MyMoney currentMoney = new MyMoney(6000);
    public static MyLocation currentLocation = new MyLocation("Chengdu", LocationDetail.PARKING);
    public static float currentHealth = 1f; // 0-1f
    public static float currentEnergy = 1f; // 0-1f
    public static float currentFuel = 1f;   // 0-1f
    public static float currentBattery = 1f;    // 0-1f

    public static string CheckConditions()
    {
        string res = "";
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            res += "Your health goes to zero.\n";
        }

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
            res += "Your energy goes to zero.\n";
        }

        if (currentFuel <= 0)
        {
            currentFuel = 0;
            res += "Your van is out of fuel.\n";
        }

        if (currentBattery <= 0)
        {
            currentBattery = 0;
            res += "Your van is out of electricity.\n";
        }


        return res;
    }

    // driving decrease energy and fuel
    public static void Driving(float energy_consumed, float fuel_consumed)
    {
        currentEnergy -= energy_consumed;
        currentFuel -= fuel_consumed;
        if (currentEnergy < 0)
            currentEnergy = 0;
        if (currentFuel < 0)
            currentFuel = 0;
    }
}
