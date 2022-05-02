using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle percentage;
    public Button saveButton;

    private void OnEnable()
    {
        GlobalStates.isStopped = true;
        int showPercentage = PlayerPrefs.GetInt("ShowPercentage", 1);
        percentage.isOn = (showPercentage == 1);
        if (GlobalStates.currentLocation.detail == LocationDetail.PARKING)
            saveButton.interactable = true;
        else
            saveButton.interactable = false;
    }

    public void ClosePanel()
    {
        GlobalStates.isStopped = false;
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

    public void SaveGame()
    {
        var sm = new StorageManager();
        sm.SaveToStorage();
        MyDialogManager.Show("Save done");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
