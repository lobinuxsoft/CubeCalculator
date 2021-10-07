using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;

public class Vector3Test : MonoBehaviour
{
    [SerializeField] Gradient gradient = default;
    [SerializeField] private List<Vector3> points = new List<Vector3>();

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
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Count != 4)
        {
            Vector3Debugger.DeleteVector($"Rect 1");
            Vector3Debugger.DeleteVector($"Rect 2");
            points.Clear();
            points.Add(Vector3.up);
            points.Add(Vector3.up + Vector3.right);
            points.Add(Vector3.zero);
            points.Add(Vector3.right);

            Vector3Debugger.AddVector(points[0], points[1], gradient.Evaluate((float)1 / (points.Count - 1)), $"Rect 1");
            Vector3Debugger.AddVector(points[2], points[3], gradient.Evaluate((float)3 / (points.Count - 1)), $"Rect 2");
        }
        else
        {
            Vector3Debugger.UpdatePosition($"Rect 1", points[0], points[1]);
            Vector3Debugger.UpdateColor($"Rect 1", gradient.Evaluate((float)1/(points.Count -1)));
            Vector3Debugger.UpdatePosition($"Rect 2", points[2], points[3]);
            Vector3Debugger.UpdateColor($"Rect 2", gradient.Evaluate((float)3 / (points.Count - 1)));
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
