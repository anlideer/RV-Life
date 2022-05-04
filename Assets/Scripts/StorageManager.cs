using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// ****can only be saved when at the city! (so we don't need to consider route...)
[Serializable]
public class StorageManager
{
    // global states
    public float currentHealth = 1f; 
    public float currentEnergy = 1f; 
    public float currentFuel = 1f; 
    public float currentBattery = 1f; 
    public float currentClean = 1f; 
    public bool isStopped = false;
    public bool isDriving = false;
    public bool isSleeping = false;

    // global states complex attributes will be handled specially
    public float money;
    public int day;
    public int hour;
    public int minute;
    public string cityName;
    public float disFromStart;
    public int locationDetail;

    // van states
    public float waterTank = 1f;
    public float greyTank = 0f;
    public float blackTank = 0f;

    // seed! (for random events that cannot be changed via sl, like weather)
    public int seed;

    
    public void LoadFromStorage()
    {
        string s = File.ReadAllText(Application.dataPath + "/Storage.txt");
        var a = JsonUtility.FromJson<StorageManager>(s);

        GlobalStates.seed = a.seed;

        VanStates.waterTank = a.waterTank;
        VanStates.greyTank = a.greyTank;
        VanStates.blackTank = a.blackTank;

        GlobalStates.currentHealth = a.currentHealth;
        GlobalStates.currentEnergy = a.currentEnergy;
        GlobalStates.currentFuel = a.currentFuel;
        GlobalStates.currentBattery = a.currentBattery;
        GlobalStates.currentClean = a.currentClean;
        GlobalStates.isSleeping = a.isSleeping;
        GlobalStates.isDriving = a.isDriving;
        GlobalStates.isStopped = false;
        GlobalStates.currentMoney.money = a.money;
        GlobalStates.currentTime = new MyTime(a.day, a.hour, a.minute);

        // special
        GlobalStates.currentLocation.cityName = a.cityName;
        GlobalStates.currentLocation.distanceFromStart = a.disFromStart;
        if (a.locationDetail == 1)
            GlobalStates.currentLocation.detail = LocationDetail.PARKING;
        else if (a.locationDetail == 2)
            GlobalStates.currentLocation.detail = LocationDetail.SERVICE;
        else if (a.locationDetail == 3)
            GlobalStates.currentLocation.detail = LocationDetail.ROAD;
        else if (a.locationDetail == 4)
            GlobalStates.currentLocation.detail = LocationDetail.ATTRACTION;
    }

    public void SaveToStorage()
    {
        seed = GlobalStates.seed;

        waterTank = VanStates.waterTank;
        greyTank = VanStates.greyTank;
        blackTank = VanStates.blackTank;

        currentHealth = GlobalStates.currentHealth;
        currentEnergy = GlobalStates.currentEnergy;
        currentFuel = GlobalStates.currentFuel;
        currentBattery = GlobalStates.currentBattery;
        currentClean = GlobalStates.currentClean;
        isSleeping = GlobalStates.isSleeping;
        isDriving = GlobalStates.isDriving;
        isStopped = GlobalStates.isStopped;
        money = GlobalStates.currentMoney.money;
        var ti = GlobalStates.currentTime;
        day = ti.day;
        hour = ti.hour;
        minute = ti.minute;

        // specail
        var location = GlobalStates.currentLocation;
        cityName = location.cityName;
        disFromStart = location.distanceFromStart;
        if (location.detail == LocationDetail.PARKING)
            locationDetail = 1;
        else if (location.detail == LocationDetail.SERVICE)
            locationDetail = 2;
        else if (location.detail == LocationDetail.ROAD)
            locationDetail = 3;
        else if (location.detail == LocationDetail.ATTRACTION)
            locationDetail = 4;

        
        string s = JsonUtility.ToJson(this);
        File.WriteAllText(Application.dataPath + "/Storage.txt", s);

    }
    
}
