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
        for (int i = 0; i < points.Count; i++)
        {
            Vector3Debugger.AddVector(points[i], $"Point {i}");
        }

        Vector3Debugger.EnableEditorView();

        lastSize = points.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastSize < points.Count) 
        {
            Vector3Debugger.AddVector(points[points.Count - 1], $"Point {points.Count-1}");
            lastSize = points.Count;
        }

        for (int i = 0; i < points.Count; i++)
        {
            Vector3Debugger.UpdateColor($"Point {i}", gradient.Evaluate((float)i / (points.Count - 1)));
            Vector3Debugger.UpdatePosition($"Point {i}", points[i]);
        }
    }
}
