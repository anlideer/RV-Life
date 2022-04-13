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
}
