using System.Collections.Generic;
using UnityEngine;

# if UNITY_EDITOR
using UnityEditor;
#endif


public class AI_Points : MonoBehaviour
{
    [Header("POINTS GIZMOS")]
    public bool gizmos = false;
    public Color gizmosColor;
    [Range (0, 1)] public float gizmosScale = 1;

    [HideInInspector] public List<Transform> _points = new List<Transform>();

    public void AddPoint() 
    {
        //create gameObject point
        GameObject pointClone = new GameObject();
        pointClone.transform.SetParent(transform);
        pointClone.transform.localPosition = Vector3.zero;
        pointClone.name = "Point_" + (_points.Count + 1);

        //add point to list
        _points.Add(pointClone.transform);

        //rename points
        for (int i = 0; i < _points.Count; i++)
            _points[i].name = "POINT_" + (i+1);
    }

    public void RemovePoint(int index)
    {
        DestroyImmediate(_points[index].gameObject);
        _points.Remove(_points[index]);

        //rename points
        for (int i = 0; i < _points.Count; i++)
            _points[i].name = "POINT_" + (i+1);
    }

    public int GetNearestPointIndex(Vector3 agentPos) 
    {
        float dis = 9999;
        int index = 0;

        for (int i = 0; i < _points.Count; i++)
        {
            if (Vector3.Distance(_points[i].position, agentPos) < dis)
            {
                dis = Vector3.Distance(_points[i].position, agentPos);
                index = i;
            }
        }

        return index;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        Gizmos.color = gizmosColor;
        Handles.color = gizmosColor;

        for (int i = 0; i < _points.Count; i++)
        {
            Gizmos.DrawWireSphere(_points[i].position, gizmosScale);

            Handles.Label(_points[i].position, "Point_" + (i+1));

            Handles.CircleCap(0, _points[i].position + (Vector3.up * 2.5f), Quaternion.Euler(90, 0, 0), gizmosScale);

            Handles.DrawDottedLine(_points[i].position, _points[i].position + (Vector3.up * 2.5f), 2f);

            if(i <= _points.Count - 2)
                Handles.DrawDottedLine(_points[i].position + (Vector3.up * 2.5f), _points[i+1].position + (Vector3.up * 2.5f), 2f);
        }
    }
#endif

}
