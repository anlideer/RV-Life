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
            Route r = node.routes[i];
            GUILayout.BeginHorizontal();
            GUILayout.Label("Route " + i.ToString());
            if (GUILayout.Button("Delete"))
                DeleteRoute(i);
            GUILayout.EndHorizontal();
            r.destination = EditorGUILayout.ObjectField("Destination", r.destination, typeof(Node), true) as Node;
            r.distance = EditorGUILayout.IntField("Distance (km)", r.distance);
            r.routeType = (RouteType)EditorGUILayout.EnumPopup("Road type", r.routeType);
            r.beauty = EditorGUILayout.FloatField("Road beauty", r.beauty);
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
