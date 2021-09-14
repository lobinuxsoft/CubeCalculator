using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CuboTool : MonoBehaviour
{
    [SerializeField] Color firstVectorColor = Color.red;
    [SerializeField] Color secondVectorColor = Color.green;
    [SerializeField] Color thirdVectorColor = Color.blue;
    [SerializeField] Color firstAddVectorColor = Color.magenta;
    [SerializeField] Color secondAddVectorColor = Color.white;
    [SerializeField] Color crossVectorColor = Color.yellow;
    [SerializeField] Color angleColor = Color.grey;
    [SerializeField] Color connectionColor = Color.cyan;
    [SerializeField] float radius = .1f;
    [SerializeField] Vector3[] points = new Vector3[8];

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        points[3] = points[0] + points[1] + points[2];
        points[4] = points[0] + Vector3.Cross(points[1], points[2]);
        points[5] = points[4] + points[1];
        points[6] = points[2] + points[4];
        points[7] = points[2] + points[5];

        Gizmos.color = firstVectorColor;
        Gizmos.DrawSphere(points[0], radius);

        Gizmos.color = secondVectorColor;
        Gizmos.DrawSphere(points[1], radius);

        Gizmos.color = thirdVectorColor;
        Gizmos.DrawSphere(points[2], radius);

        Gizmos.color = firstAddVectorColor;
        Gizmos.DrawSphere(points[3], radius);

        Gizmos.color = crossVectorColor;
        Gizmos.DrawSphere(points[4], radius);

        Gizmos.color = secondAddVectorColor;
        Gizmos.DrawSphere(points[5], radius);

        Gizmos.color = secondAddVectorColor;
        Gizmos.DrawSphere(points[6], radius);

        Gizmos.color = secondAddVectorColor;
        Gizmos.DrawSphere(points[7], radius);

        Handles.color = angleColor;
        Handles.DrawSolidArc(points[0], points[4], points[1], Vector3.Angle(points[1], points[2]), 1);

        Gizmos.color = connectionColor;
        Gizmos.DrawLine(points[0], points[1]);
        Gizmos.DrawLine(points[1], points[3]);
        Gizmos.DrawLine(points[3], points[2]);
        Gizmos.DrawLine(points[2], points[0]);

        Gizmos.DrawLine(points[0], points[2]);
        Gizmos.DrawLine(points[2], points[6]);
        Gizmos.DrawLine(points[6], points[4]);
        Gizmos.DrawLine(points[4], points[0]);

        Gizmos.DrawLine(points[1], points[3]);
        Gizmos.DrawLine(points[3], points[7]);
        Gizmos.DrawLine(points[7], points[5]);
        Gizmos.DrawLine(points[5], points[1]);

        Gizmos.DrawLine(points[4], points[5]);
        Gizmos.DrawLine(points[6], points[7]);
    }
#endif
}
