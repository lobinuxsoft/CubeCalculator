using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CuboTool : MonoBehaviour
{
    [SerializeField] Gradient gradient = default;
    [SerializeField] GUIStyle style = default;
    [SerializeField] int fontSize = 5;
    [SerializeField] Color connectionColor = Color.cyan;
    [SerializeField] float radius = .1f;
    [SerializeField] float heightCutPoint = .3f;
    [SerializeField] float planeSize = 1f;
    [SerializeField] Vector3[] points = new Vector3[8];

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        #region Calculo del cubo

        points[0] = Vector3.Normalize(points[0]);
        points[1] = Vector3.Normalize(points[1]);
        points[2] = Vector3.Normalize(points[2]);
        points[3] = points[0] + points[1] + points[2];
        points[4] = points[0] + Vector3.Normalize((Vector3.Cross(points[1], points[2])));
        points[5] = points[4] + points[1];
        points[6] = points[2] + points[4];
        points[7] = points[2] + points[5];

        // Punto de corte
        Vector3 cutPoint = Vector3.up * heightCutPoint;

        #endregion

        style.fontSize = Mathf.RoundToInt(Vector3.Distance(Camera.current.transform.position, this.transform.position)) * fontSize;

        style.normal.textColor = Color.black;

        // Grafico punto de corte
        Handles.color = Color.black;
        Handles.SphereHandleCap(0, cutPoint, Quaternion.identity, radius, EventType.Repaint);
        Handles.Label(cutPoint + Camera.current.transform.right * .25f, $"X={cutPoint.x:0.00} \n Y={cutPoint.y:0.00} \n Z={cutPoint.z:0.00}");
        Handles.DrawWireCube(cutPoint, new Vector3(planeSize, 0, planeSize));
        Handles.DrawLine(points[0], cutPoint, .01f);

        for (int i = 0; i < points.Length; i++)
        {
            style.normal.textColor = gradient.Evaluate((float)i / (points.Length - 1));
            Handles.color = gradient.Evaluate((float)i / (points.Length - 1));
            Handles.SphereHandleCap(0, points[i], Quaternion.identity, radius, EventType.Repaint);
            Handles.Label(points[i] + Camera.current.transform.right * .25f, $"X={points[i].x:0.00} \n Y={points[i].y:0.00} \n Z={points[i].z:0.00}");
        }

        Handles.color = gradient.Evaluate((float)7 / (points.Length - 1));
        Handles.DrawSolidArc(points[0], points[4], points[1], Vector3.Angle(points[1], points[2]), .25f);

        Handles.color = connectionColor;
        Handles.DrawLine(points[0], points[1]);
        Handles.DrawLine(points[1], points[3]);
        Handles.DrawLine(points[3], points[2]);
        Handles.DrawLine(points[2], points[0]);

        Handles.DrawLine(points[0], points[2]);
        Handles.DrawLine(points[2], points[6]);
        Handles.DrawLine(points[6], points[4]);
        Handles.DrawLine(points[4], points[0]);

        Handles.DrawLine(points[1], points[3]);
        Handles.DrawLine(points[3], points[7]);
        Handles.DrawLine(points[7], points[5]);
        Handles.DrawLine(points[5], points[1]);

        Handles.DrawLine(points[4], points[5]);
        Handles.DrawLine(points[6], points[7]);
    }
#endif
}
