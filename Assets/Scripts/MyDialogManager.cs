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
        if (obj)
            Destroy(obj);
        GameObject dialogObj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = dialogObj.GetComponent<DialogManager>();

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
    public static void EndGame()
    {
        DialogFinished();
        SceneManager.LoadScene("Menu");
    }

    public static void Show(List<string> plainTexts)
    {
        SetGameStop(true);
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (obj)
            Destroy(obj);
        GameObject dialogObj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = dialogObj.GetComponent<DialogManager>();

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
        if (obj)
            Destroy(obj);
        GameObject dialogObj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = dialogObj.GetComponent<DialogManager>();

        DialogData d = new DialogData(plainText);
        manager.Show(d);
        d.Callback = () => DialogFinished();
    }

    // callback
    public static void DialogFinished()
    {
        SetGameStop(false);
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (obj)
            Destroy(obj);
    }

    public static void SetGameStop(bool isStop)
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
        if (obj)
            Destroy(obj);
        GameObject dialogObj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = dialogObj.GetComponent<DialogManager>();
        manager.Show(dd);
    }

    // callback for sleep
    public static void SleepCallback()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        DialogManager manager = obj.GetComponent<DialogManager>();

        if (manager.Result == "1h")
        {
            GlobalStates.isSleeping = true;
            Show("Resting for one hours/speed:down/.../speed:init/");
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
        if (obj)
            Destroy(obj);
        GameObject dialogObj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = dialogObj.GetComponent<DialogManager>();
        manager.Show(dd);
    }

    // refuel callback
    public static void RefuelCallback()
    {
        float fuelPrice = 5.6f;  // 0.01fuel->5.6yuan
        Transform canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;

        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
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

}
