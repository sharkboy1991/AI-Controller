using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AI_Points))]
public class AI_PointsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AI_Points trg = (AI_Points)target;

        GUILayout.Space(25);

        GUILayout.Label("POINTS LIST", EditorStyles.boldLabel);

        for (int i=0; i< trg._points.Count; i++) 
        {
            GUILayout.BeginHorizontal();

            EditorGUILayout.ObjectField("POINT_" + (i + 1), trg._points[i], typeof(Object));

            if (GUILayout.Button("REMOVE POINT"))
            {
                trg.RemovePoint(i);
            }

            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("ADD POINT"))
        {
            trg.AddPoint();
        }
    }
}
