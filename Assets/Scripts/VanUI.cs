using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VanUI : MonoBehaviour
{
    public GameObject waterPanel;

    private void Start()
    {
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
    }

    public void EmptyGreyBox(float duration)
    {
        waterPanel.SetActive(false);
    }

    public void EmptyBlackBox(float duration)
    {
        waterPanel.SetActive(false);
    }

    public void MaintainWaterSystem(float duration)
    {
        waterPanel.SetActive(false);
    }

}
