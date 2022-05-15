using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;

public class MyDialogManager : MonoBehaviour
{
    public static void ShowandGameover(List<string> plainTexts)
    {
        SetGameStop(true);
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = obj.GetComponent<DialogManager>();

        List<DialogData> datas = new List<DialogData>();
        for (int i = 0; i < plainTexts.Count; i++)
        {
            var tmp = new DialogData(plainTexts[i]);
            if (i == plainTexts.Count - 1)
                tmp.Callback = () => EndGame();
            datas.Add(tmp);
        }

        manager.Show(datas);
    }

    // callback
    private static void EndGame()
    {
        DialogFinished();
        SceneManager.LoadScene("Menu");
        GlobalStates.currentTime = new MyTime(1, 8, 0);
        GlobalStates.currentMoney = new MyMoney(7000f);
        GlobalStates.currentLocation = new MyLocation("Chengdu", LocationDetail.PARKING);
        GlobalStates.currentWeather = new Weather();
        GlobalStates.currentHealth = 1f; // 0-1f
        GlobalStates.currentEnergy = 1f; // 0-1f
        GlobalStates.currentFuel = 1f;   // 0-1f
        GlobalStates.currentBattery = 1f;    // 0-1f
        GlobalStates.currentClean = 1f;  // 0-1f
        GlobalStates.isStopped = false;
        GlobalStates.isDriving = false;
        GlobalStates.isSleeping = false;
        VanStates.waterTank = 1f;
        VanStates.greyTank = 0f;
        VanStates.blackTank = 0f;
        VanStates.greyCondition = 1f;
        VanStates.blackCondition = 1f;
        VanStates.shown = false;
}

    // win the game
    public static void WinGame()
    {
        SetGameStop(true);
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = obj.GetComponent<DialogManager>();

        DialogData d = new DialogData("Success! You win the game! (sorry this is a little crude)");
        manager.Show(d);
        d.Callback = () => EndGame();

    }

    public static void Show(List<string> plainTexts)
    {
        SetGameStop(true);
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = obj.GetComponent<DialogManager>();

        List<DialogData> datas = new List<DialogData>();
        for (int i = 0; i < plainTexts.Count; i++)
        {
            var tmp = new DialogData(plainTexts[i]);
            if (i == plainTexts.Count - 1)
                tmp.Callback = () => DialogFinished();
            datas.Add(tmp);
        }
            
        manager.Show(datas);
    }

    public static void Show(string plainText)
    {
        SetGameStop(true);
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = obj.GetComponent<DialogManager>();

        DialogData d = new DialogData(plainText);
        manager.Show(d);
        d.Callback = () => DialogFinished();
    }

    // callback
    private static void DialogFinished()
    {
        SetGameStop(false);
    }

    private static void SetGameStop(bool isStop)
    {
        GlobalStates.isStopped = isStop;
    }

    // sleep option dialog
    public static void SleepDialog()
    {
        List<DialogData> dd = new List<DialogData>();
        DialogData d1 = new DialogData("How long do you want to sleep or rest?");
        d1.SelectList.Add("1h", "1 hour");
        d1.SelectList.Add("8h", "8 hours");
        d1.SelectList.Add("8am", "Until tomorrow 8am");
        d1.SelectList.Add("no", "No, I don't want to sleep");
        d1.Callback = () => SleepCallback();
        dd.Add(d1);
        // show
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = obj.GetComponent<DialogManager>();
        manager.Show(dd);
    }

    // callback for sleep
    private static void SleepCallback()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = obj.GetComponent<DialogManager>();

        if (manager.Result == "1h")
        {
            GlobalStates.isSleeping = true;
            Show("Resting for one hour/speed:down/.../speed:init/");
            GlobalStates.currentTime.TimePass(new MyTime(0, 1, 0));
            GlobalStates.ChangeEnergy(0.1f);
            GlobalStates.isSleeping = false;
        }
        else if (manager.Result == "8h")
        {
            GlobalStates.isSleeping = true;
            Show("Sleeping for eight hours/speed:down/....../speed:init/");
            GlobalStates.currentTime.TimePass(new MyTime(0, 8, 0));
            GlobalStates.ChangeEnergy(1f);
            GlobalStates.isSleeping = false;
        }
        else if (manager.Result == "8am")
        {
            GlobalStates.isSleeping = true;
            Show("Sleeping until 8am/speed:down/....../speed:init/");
            float hours = GlobalStates.currentTime.TimePassUntil(new MyTime(GlobalStates.currentTime.day + 1, 8, 0));
            GlobalStates.ChangeEnergy(0.125f * hours);
            GlobalStates.isSleeping = false;
        }
        else
        {
            Show("Cancel rest operation");
        }

    }

    // refuel option dialog
    public static void RefuelDialog()
    {
        List<DialogData> dd = new List<DialogData>();
        DialogData d1 = new DialogData("How much do you want to refuel?");
        d1.SelectList.Add("0.1", "Refuel 10%");
        d1.SelectList.Add("0.5", "Refuel 50%");
        d1.SelectList.Add("full", "Refuel to full");
        d1.SelectList.Add("cancel", "Cancel");
        d1.Callback = () => RefuelCallback();
        dd.Add(d1);
        // show
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = obj.GetComponent<DialogManager>();
        manager.Show(dd);
    }

    // refuel callback
    private static void RefuelCallback()
    {
        float fuelPrice = 5.6f;  // 0.01fuel->5.6yuan
        Transform canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;

        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = obj.GetComponent<DialogManager>();
        float amount = 1f;

        if (manager.Result == "0.1")
        {
            amount = 0.1f;
        }
        else if (manager.Result == "0.5")
        {
            amount = 0.5f;
        }
        else if (manager.Result == "full")
        {
            amount = 1f - GlobalStates.currentFuel;
        }
        else
        {
            Show("Cancel rest operation");
            return;
        }

        float cost = fuelPrice * amount * 100;
        // check affordable
        if (GlobalStates.currentMoney.Affordable(cost))
        {
            GlobalStates.currentTime.TimePass(new MyTime(0, 0, 20));
            GlobalStates.currentMoney.Spend(cost);
            GlobalStates.ChangeFuel(amount);
            // show pic
            GameObject picObj = Instantiate(Resources.Load("FuelPic") as GameObject, canvas);
            Destroy(picObj, 3f);
            MyDialogManager.Show(string.Format("Cost ¥{0}.", (int)cost));
        }
        else
        {
            MyDialogManager.Show(string.Format("It will cost ¥{0}. I don't have enough money", (int)cost));
        }
    }


    // phone dialog
    public static void PhoneDialog()
    {
        List<DialogData> dd = new List<DialogData>();
        DialogData d1 = new DialogData("Choose option");
        if (GlobalStates.currentLocation.detail == LocationDetail.PARKING)
            d1.SelectList.Add("food", "Buy food delivery (30)");
        d1.SelectList.Add("weather", "See weather forecast");
        d1.SelectList.Add("work", "Online part-time job");
        d1.SelectList.Add("exit", "Stop using phone");
        d1.Callback = () => PhoneCallback();
        dd.Add(d1);
        // show
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = obj.GetComponent<DialogManager>();
        manager.Show(dd);
    }

    // phone callback
    private static void PhoneCallback()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = obj.GetComponent<DialogManager>();
        obj.SetActive(true);

        if (manager.Result == "food")
        {
            BuyFoodDelivery();
        }
        else if (manager.Result == "weather")
        {
            Show(GlobalStates.currentWeather.GetForecast());
            GlobalStates.currentTime.TimePass(new MyTime(0, 0, 10));
        }
        else if (manager.Result == "work")
        {
            WorkDialog();
        }
        else
        {
            DialogFinished();
        }
    }

    private static void BuyFoodDelivery()
    {
        if (GlobalStates.currentMoney.Affordable(30))
        {
            MyDialogManager.Show("Eating......Happy to eat local food. (Cost 30)");
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

    // online part-time work dialog options
    private static void WorkDialog()
    {
        List<DialogData> dd = new List<DialogData>();
        DialogData d1 = new DialogData("Choose work option");
        d1.SelectList.Add("2", "2 hours. Profit: ¥15-20.");
        d1.SelectList.Add("4", "4 hours. Profit: ¥35-45.");
        d1.SelectList.Add("6", "6 hours. Profit: ¥60-80.");
        d1.SelectList.Add("exit", "Stop using phone");
        d1.Callback = () => WorkCallback();
        dd.Add(d1);
        // show
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        else
        {
            Destroy(obj);
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        }
        DialogManager manager = obj.GetComponent<DialogManager>();
        manager.Show(dd);
    }

    private static void WorkCallback()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (!obj)
            obj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = obj.GetComponent<DialogManager>();

        Random.InitState(System.Environment.TickCount);
        if (manager.Result == "2")
        {
            int profit = Random.Range(15, 21);
            Show(string.Format("Working... Earn ¥{0}.", profit));
            GlobalStates.currentTime.TimePass(new MyTime(0, 2, 0));
            GlobalStates.ChangeEnergy(-0.05f);
            GlobalStates.currentMoney.Earn(profit);
        }
        else if (manager.Result == "4")
        {
            int profit = Random.Range(35, 46);
            Show(string.Format("Working... Earn ¥{0}.", profit));
            GlobalStates.currentTime.TimePass(new MyTime(0, 4, 0));
            GlobalStates.ChangeEnergy(-0.1f);
            GlobalStates.currentMoney.Earn(profit);
        }
        else if (manager.Result == "6")
        {
            int profit = Random.Range(60, 81);
            Show(string.Format("Working... Earn ¥{0}.", profit));
            GlobalStates.currentTime.TimePass(new MyTime(0, 6, 0));
            GlobalStates.ChangeEnergy(-0.15f);
            GlobalStates.currentMoney.Earn(profit);
        }
        else
        {
            DialogFinished();
        }
    }

}
