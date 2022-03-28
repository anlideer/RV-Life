using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node))]
[CanEditMultipleObjects]
public class NodeEditor : Editor
{
    Node node;

    private void OnEnable()
    {
        node = (Node)target;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        serializedObject.Update();    
        GUILayout.Space(10f);
        // the route list
        GUILayout.BeginVertical();
        GUILayout.Label("Edit routes");
        if (GUILayout.Button("Add a route"))
            AddList();
        for (int i = 0; i < node.routes.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Route " + i.ToString());
            if (GUILayout.Button("Delete"))
                DeleteRoute(i);
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

    private void AddList()
    {
        node.routes.Add(new Route());
    }

    private void DeleteRoute(int index)
    {
        node.routes.RemoveAt(index);
    }
}
