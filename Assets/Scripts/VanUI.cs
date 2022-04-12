using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class VanUI : MonoBehaviour
{
    public float destroyDuration = 3f;
    public GameObject waterPanel;
    public Text popUpPrefab;

    private Transform canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
        waterPanel.SetActive(false);
    }

    public void OpenWaterPanel()
    {
        waterPanel.SetActive(true);
    }

    public void CloseWaterPanel()
    {
        waterPanel.SetActive(false);
    }

    public void RefillWaterBox(float duration)
    {
        waterPanel.SetActive(false);
        Text popUp = Instantiate(popUpPrefab, canvas);
        popUp.text = "Refilling water tank...";
        Destroy(popUp.gameObject, destroyDuration);
        GlobalStates.currentTime.TimePass(new MyTime(0, 0, 30));
        VanStates.waterTank = 1f;
    }

    public void EmptyGreyBox(float duration)
    {
        waterPanel.SetActive(false);
        Text popUp = Instantiate(popUpPrefab, canvas);
        popUp.text = "Emptying grey tank...";
        Destroy(popUp.gameObject, destroyDuration);
        GlobalStates.currentTime.TimePass(new MyTime(0, 0, 30));
        VanStates.greyTank = 0f;
    }

    public void EmptyBlackBox(float duration)
    {
        waterPanel.SetActive(false);
        Text popUp = Instantiate(popUpPrefab, canvas);
        popUp.text = "Emptying black tank...";
        Destroy(popUp.gameObject, destroyDuration);
        GlobalStates.currentTime.TimePass(new MyTime(0, 0, 30));
        VanStates.blackTank = 0f;
    }

    public void MaintainWaterSystem(float duration)
    {
        waterPanel.SetActive(false);
        // TODO: maintain, check the water system
    }

    // back to map
    public void BackToMap()
    {
        SceneManager.LoadScene("Map");
    }

}
