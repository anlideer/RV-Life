using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderUI : MonoBehaviour
{
    public Text moneyText;
    public Text timeText;

    private void Update()
    {
        moneyText.text = GlobalStates.currentMoney.GetStringShown();
        timeText.text = GlobalStates.currentTime.GetStringShown();
    }
}
