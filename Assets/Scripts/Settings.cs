using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle percentage;
    public Button saveButton;
    public Toggle normalSpeed;
    public Toggle fastSpeed;
    public Toggle veryFastSpeed;
    public Toggle muteMusic;

    private void OnEnable()
    {
        GlobalStates.isStopped = true;
        // percentage
        int showPercentage = PlayerPrefs.GetInt("ShowPercentage", 1);
        percentage.isOn = (showPercentage == 1);
        if (GlobalStates.currentLocation.detail == LocationDetail.PARKING)
            saveButton.interactable = true;
        else
            saveButton.interactable = false;
        // game speed
        int i = PlayerPrefs.GetInt("Speed", 1);
        if (i == 1)
            normalSpeed.isOn = true;
        else if (i == 2)
            fastSpeed.isOn = true;
        else
            veryFastSpeed.isOn = true;

        GameObject obj = GameObject.FindGameObjectWithTag("Music");
        muteMusic.isOn = obj.GetComponent<AudioSource>().mute;
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

    public void OnSpeedValueChange()
    {
        if (normalSpeed.isOn)
            PlayerPrefs.SetInt("Speed", 1);
        else if (fastSpeed.isOn)
            PlayerPrefs.SetInt("Speed", 2);
        else if (veryFastSpeed.isOn)
            PlayerPrefs.SetInt("Speed", 3);
    }

    public void OpenHelp()
    {
        MyDialogManager.Show(new List<string> {
            "The goal is to go to Hongkong (somewhere south near the sea) and Mohe (the northest) with limited money.",
            "In this setting panel, you can set a higher speed to make the test quicker.",
            "You can drag and zoom the map and click to select destination. The info is shown in the right bottom corner.",
            "You can enter the van's page (left bottom corner) when you stop.",
            "In the van's page, you can rest and do many things. There are also some more activities if you click the phone (right bottom corner).",
            "There is a simple weather system, but the possibility of rain or rainstorm is pretty low. If you encounter that, you're lucky...",
            "Watch out your body's conditions and your van's states.",
        }) ;
    }

    public void MuteMusic()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Music");
        
        if (muteMusic.isOn)
        {
            obj.GetComponent<AudioSource>().mute = true;
            PlayerPrefs.SetInt("Music", 0);
        }
            
        else
        {
            obj.GetComponent<AudioSource>().mute = false;
            PlayerPrefs.SetInt("Music", 1);
        }
            
    }
}
