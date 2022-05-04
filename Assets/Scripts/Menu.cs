using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Menu : MonoBehaviour
{
    public GameObject aboutPanel;
    public Button continueButton;
    public GameObject settingPanel;

    private void Awake()
    {
        aboutPanel.SetActive(false);
        settingPanel.SetActive(false);
        if (!File.Exists(Application.dataPath + "/Storage.txt"))
            continueButton.interactable = false;
        else
            continueButton.interactable = true;
    }

    public void StartNewGame()
    {
        // TODO: load the scene with opening story

        GlobalStates.seed = Random.Range(1, 10000);
        SceneManager.LoadScene("Map");
    }

    public void ContinueGame()
    {
        var sm = new StorageManager();
        sm.LoadFromStorage();
        SceneManager.LoadScene("Map");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenAbout()
    {
        aboutPanel.SetActive(true);
    }

    public void CloseAbout()
    {
        aboutPanel.SetActive(false);
    }

    public void OpenSetting()
    {
        settingPanel.SetActive(true);
    }

}
