using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasStationPanel : MonoBehaviour
{
    //[Header("Objects")]

    [Header("Settings")]
    public float fuelPrice = 8.6f;  // 0.01fuel->8.6yuan
    public float foodPrice = 20f;

    [Header("Prefabs")]
    public GameObject refuelPic;
    public Text popUpText;

    private Transform canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
    }

    // TODO: check affordable

    // refuel
    public void RefuelAll()
    {
        float amount = 1f - GlobalStates.currentFuel;
        GlobalStates.currentMoney.Spend(fuelPrice * amount * 100);
        GlobalStates.currentFuel = 1f;
        GlobalStates.currentTime.TimePass(new MyTime(0, 0, 20));
        // show pic
        GameObject obj = Instantiate(refuelPic, canvas);
        Destroy(obj, 3f);
    }

    // buy food and eat
    public void Eat()
    {
        GlobalStates.currentHealth = 1f;
        GlobalStates.currentTime.TimePass(new MyTime(0, 1, 0));
        GlobalStates.currentMoney.Spend(foodPrice);
        Text t = Instantiate(popUpText, canvas).GetComponent<Text>();
        t.text = "Eating...(cost 20)";
        Destroy(t.gameObject, 2f);
    }
}
