using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class MyDialogManager : MonoBehaviour
{
    public static void Show(List<string> plainTexts)
    {
        SetGameStop(true);
        GameObject obj = GameObject.FindGameObjectWithTag("Dialog");
        if (obj)
            Destroy(obj);
        GameObject dialogObj = Instantiate(Resources.Load("DialogAsset") as GameObject);
        DialogManager manager = dialogObj.GetComponent<DialogManager>();

        List<DialogData> datas = new List<DialogData>();
        foreach (string s in plainTexts)
            datas.Add(new DialogData(s));
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
        DialogData d1 = new DialogData("How long do you want to sleep/rest?");
        d1.SelectList.Add("1h", "1 hour");
        d1.SelectList.Add("8h", "8 hours");
        d1.SelectList.Add("8am", "Until tomorrow 8am");
        d1.SelectList.Add("full", "Until energy becomes full");
        d1.SelectList.Add("no", "No, I don't want to sleep");
        d1.Callback = () => SleepCallback();
        // TODO: call manager show...
    }

    // callback for sleep
    public static void SleepCallback()
    {
        
    }
}
