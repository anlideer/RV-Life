using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class NodeWindow : EditorWindow
{
    [MenuItem("Tools/NodeWindow")]
    static void Init()
    {
        GetWindow(typeof(NodeWindow));
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Load&Save");
        if (GUILayout.Button("Load Data"))
            LoadFromPersistence();
        if (GUILayout.Button("Save Data"))
            SaveToPersistence();
        if (GUILayout.Button("Backup Data"))
            BackUpData();
        if (GUILayout.Button("Recover from backup"))
            RecoverFromBackup();
        GUILayout.EndVertical();
    }


    private void LoadFromPersistence()
    {
        string path = "Assets/Resources/NodeData";

        // fetch the data back to the editor
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Node");
        foreach (var obj in objs)
        {
            try
            {
                Node n = obj.GetComponent<Node>();
                string filePath = Path.Combine(path, n.name + ".txt");
                if (File.Exists(filePath))
                {
                    n.cityName = obj.name;
                    n.routes = new List<Route>();
                    StreamReader reader = new StreamReader(filePath);
                    string content = "";
                    content = reader.ReadLine();
                    while (content !=null && content.Length > 2)
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
                        content = reader.ReadLine();
                    }
                }
                else
                {
                    Debug.Log("Unable to find the data for " + obj.name);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(string.Format("{0}: {1}", obj.name, e));
            }
        }
        Debug.Log("Load done");
    }

    private void SaveToPersistence()
    {
        string path = "Assets/Resources/NodeData";
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Node");
        foreach (var obj in objs)
        {
            try
            {
                string s = "";
                Node n = obj.GetComponent<Node>();
                foreach (Route r in n.routes)
                {
                    s += r.destination.cityName + ",";
                    s += r.distance.ToString() + ",";
                    s += ((int)r.routeType).ToString() + ",";
                    s += r.beauty.ToString() + "\n";
                }
                File.WriteAllText(Path.Combine(path, obj.name + ".txt"), s);
            }
            catch { }
        }

        Debug.Log("Save done");
    }

    // simply copy the persistence to be a backup (only one backup exists)
    private void BackUpData()
    {
        string path = "Assets/Resources/NodeData";
        string backuppath = "Assets/Editor/NodeData";
        string[] files = Directory.GetFiles(path);
        foreach (string f in files)
        {
            if (Path.GetExtension(f) == ".txt")
            {
                string tmps = File.ReadAllText(f);
                File.WriteAllText(Path.Combine(backuppath, Path.GetFileNameWithoutExtension(f) + ".backup"), tmps);
            }
        }
        Debug.Log("Back up done");
    }

    // save from back up (back up to normal)
    private void RecoverFromBackup()
    {
        string path = "Assets/Resources/NodeData";
        string backuppath = "Assets/Editor/NodeData";
        string[] files = Directory.GetFiles(backuppath);
        foreach(string f in files)
        {
            if (Path.GetExtension(f) == ".backup")
            {
                string tmps = File.ReadAllText(f);
                Debug.Log(f);
                File.WriteAllText(Path.Combine(path, Path.GetFileNameWithoutExtension(f)+".txt"), tmps);
            }
        }
        Debug.Log("Recover done");
    }

}
