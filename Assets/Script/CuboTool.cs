using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CuboTool : MonoBehaviour
{
    const int pointAmount = 11;

    [SerializeField] Gradient gradient = default;
    [SerializeField] GUIStyle style = default;
    [SerializeField] int fontSize = 5;
    [SerializeField] Color connectionColor = Color.cyan;
    [SerializeField] float radius = .1f;
    [SerializeField] float heightCutPoint = .2f;
    [SerializeField] float planeSize = 1f;
    [SerializeField] List<Vector3> points = new List<Vector3>();

    Vector3 PointCutCalculator(Vector3 begin, Vector3 end, float height)
    {
        Vector3 cutPoint;
        Vector3 vector = end - begin;
        Vector3 point = end;
        float landa = (height - point.y) / vector.y;
        cutPoint.x = point.x + vector.x * landa;
        cutPoint.z = point.z + vector.z * landa;
        cutPoint.y = height;
        return cutPoint;
    }

    float AreaCalculator(Vector3 point0, Vector3 point1, Vector3 point2, float height)
    {
        const int size = 3;
        Vector3[] pointsForBases = new Vector3[size];
        float[] bases = new float[size];
        pointsForBases[0] = point0;
        pointsForBases[1] = point1;
        pointsForBases[2] = point2;
        int k = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = i + 1; j < size; j++)
            {
                bases[k] = (pointsForBases[i] - pointsForBases[j]).magnitude;
                k++;
            }
        }
        float area = 0;
        for (int i = 0; i < size; i++)
        {
            area += ((bases[i] * height) / 2);
        }
        return area;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(points.Count < pointAmount) 
        {
            points.Add(Vector3.zero);
        }

        #region Calculo del cubo

        points[0] = Vector3.Normalize(points[0]);
        points[1] = Vector3.Normalize(points[1]);
        points[2] = Vector3.Normalize(points[2]);
        points[3] = points[0] + points[1] + points[2];
        points[4] = points[0] + Vector3.Normalize((Vector3.Cross(points[1], points[2])));
        points[5] = points[4] + points[1];
        points[6] = points[2] + points[4];
        points[7] = points[2] + points[5];
        points[8] = PointCutCalculator(points[0], points[4], heightCutPoint);
        points[9] = PointCutCalculator(points[0], points[1], heightCutPoint);
        points[10] = PointCutCalculator(points[0], points[2], heightCutPoint);

        // Punto de corte
        Vector3 cutPoint = Vector3.up * heightCutPoint;

        #endregion

        style.fontSize = Mathf.RoundToInt(Vector3.Distance(Camera.current.transform.position, this.transform.position)) * fontSize;

        style.normal.textColor = Color.black;

        // Grafico punto de corte
        Handles.color = Color.black;
        Handles.SphereHandleCap(0, cutPoint, Quaternion.identity, radius, EventType.Repaint);
        Handles.Label(cutPoint + Camera.current.transform.right * .1f, $"Total Area = {AreaCalculator(points[8], points[9], points[10], heightCutPoint)} \n X={cutPoint.x:0.00} \n Y={cutPoint.y:0.00} \n Z={cutPoint.z:0.00} \n");
        Handles.DrawWireCube(cutPoint, new Vector3(planeSize, 0, planeSize));
        Handles.DrawLine(points[0], cutPoint, .01f);

        for (int i = 0; i < points.Count; i++)
        {
            style.normal.textColor = gradient.Evaluate((float)i / (points.Count - 1));
            Handles.color = gradient.Evaluate((float)i / (points.Count - 1));
            Handles.SphereHandleCap(0, points[i], Quaternion.identity, radius, EventType.Repaint);
            Handles.Label(points[i] + Camera.current.transform.right * .1f, $"X={points[i].x:0.00} \n Y={points[i].y:0.00} \n Z={points[i].z:0.00}");
        }

        //Handles.color = gradient.Evaluate((float)7 / (points.Length - 1));
        //Handles.DrawSolidArc(points[0], points[4], points[1], Vector3.Angle(points[1], points[2]), .25f);

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

        Handles.color = Color.red;
        Handles.DrawLine(points[8], points[0]);
        Handles.DrawLine(points[9], points[0]);
        Handles.DrawLine(points[10], points[0]);
    }
#endif
}
