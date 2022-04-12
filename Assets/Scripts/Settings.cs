using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle percentage;

    private void OnEnable()
    {
        int showPercentage = PlayerPrefs.GetInt("ShowPercentage", 1);
        percentage.isOn = (showPercentage == 1);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void OnTogglePercentageChanged()
    {
        if (percentage.isOn)
        {
            PlayerPrefs.SetInt("ShowPercentage", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShowPercentage", 0);
        }
    }
}
