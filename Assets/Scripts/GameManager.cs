using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 60;   // fix 60hz
        LoadMap();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }


    private void LoadMap()
    {
        // fetch the data back to the editor
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Node");
        foreach (var obj in objs)
        {
            try
            {
                Node n = obj.GetComponent<Node>();
                n.cityName = obj.name;
                string s = Resources.Load<TextAsset>("NodeData/" + n.name).text;
                var contents = s.Split('\n');
                foreach(var content in contents)
                {
                    // resolve route data
                    string[] words = content.Split(',');
                    if (words.Length < 4)
                        continue;
                    Route tmpr = new Route();
                    tmpr.destination = GameObject.Find(words[0]).GetComponent<Node>();
                    tmpr.distance = float.Parse(words[1]);
                    tmpr.routeType = (RouteType)int.Parse(words[2]);
                    tmpr.beauty = float.Parse(words[3]);
                    n.routes.Add(tmpr);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(string.Format("Error when reading data for city {0}: {1}", obj.name, e));
            }
        }
        Debug.Log("Load map data done");
    }
}
