using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStates : MonoBehaviour
{
    public static MyTime currentTime = new MyTime(1, 8, 0);
    public static MyMoney currentMoney = new MyMoney(6000);
    public static MyLocation currentLocation = new MyLocation("Beijing-Parking Lot");
    public static float currentHealth = 1f; // 0-1f
    public static float currentEnergy = 1f; // 0-1f
}
