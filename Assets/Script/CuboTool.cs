using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CuboTool : MonoBehaviour
{
    [SerializeField] Color connectionColor = default;
    [SerializeField] float radius = .1f;
    [SerializeField] Vector3[] points = new Vector3[3];

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);


        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(points[1], radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(points[2], radius);

        
        Handles.color = Color.cyan;
        points[3] = Vector3.Cross(points[1], points[2]);
        Gizmos.DrawSphere(points[3], radius);

        Handles.DrawSolidArc(points[0], points[3], points[2], Vector3.Angle(points[0], points[1]), 1);

        ColorUtility.TryParseHtmlString("#3900AB", out Color color);

        Gizmos.color = color;
        Gizmos.DrawSphere(points[1], radius);

        points[3] = points[0] + points[1];
        Gizmos.DrawSphere(points[3], radius);

        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = connectionColor;
            Gizmos.DrawLine(transform.position, points[i]);
        }
    }
#endif
}
