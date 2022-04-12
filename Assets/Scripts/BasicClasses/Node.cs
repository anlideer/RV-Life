using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Node : MonoBehaviour
{
    //[Header("Normal attributes")]
    private float textOffset = 0.08f;
    private GameObject textInstance;

    [Header("Node attributes")]
    public string cityName;
    public List<Route> routes = new List<Route>();

    private void Start()
    {
        ShowName();
    }

    private void Update()
    {
        ShowName();   
    }

    public void ShowName()
    {
        if (textInstance == null)
        {
            Transform mapLayer = GameObject.FindGameObjectWithTag("MapUILayer").transform;
            GameObject cityNameText = Resources.Load("CityNameText") as GameObject;
            GameObject instance = Instantiate(cityNameText, mapLayer);
            textInstance = instance;
            Text instanceText = instance.GetComponent<Text>();
            instanceText.text = cityName;
        }

        Vector3 pos = transform.position;
        pos.z = 0;
        pos.y -= textOffset;
        textInstance.transform.position = Camera.main.WorldToScreenPoint(pos);
    }
}
