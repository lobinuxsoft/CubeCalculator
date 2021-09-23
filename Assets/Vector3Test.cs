using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;

public class Vector3Test : MonoBehaviour
{
    [SerializeField] Gradient gradient = default;
    [SerializeField] private List<Vector3> points = new List<Vector3>();

    int lastSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(points.Count < 4)
        {
            points.Clear();
            points.Add(Vector3.up);
            points.Add(Vector3.up + Vector3.right);
            points.Add(Vector3.zero);
            points.Add(Vector3.right);
        }

        Vector3Debugger.AddVector(points[0], points[1], $"Rect 1");
        Vector3Debugger.AddVector(points[2], points[3], $"Rect 2");

        Vector3Debugger.EnableEditorView();

        lastSize = points.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastSize < points.Count) 
        {
            Vector3Debugger.AddVector(points[points.Count - 2], points[points.Count - 1], $"Point {points.Count-1}");
            lastSize = points.Count;
        }

        for (int i = 1; i < points.Count; i++)
        {
            Vector3Debugger.UpdateColor($"Point {i}", gradient.Evaluate((float)i / (points.Count - 1)));
            Vector3Debugger.UpdatePosition($"Point {i}", points[i-1], points[i]);
        }

        if (points.Count >= 4)
        {
            RectCalculator(points[0], points[1], points[2], points[3]);
        }
    }

    void RectCalculator(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        float slope1 = GetSlope(p1, p2);
        float slope2 = GetSlope(p3, p4);

        Debug.Log($"Pendiente 1 = {slope1}, Pendiente 2 = {slope2}");
    }

    float GetSlope(Vector2 start, Vector2 end)
    {
        return end.y - start.y / end.x - start.x;
    }
}
