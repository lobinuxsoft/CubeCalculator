using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CuboTool : MonoBehaviour
{
    [SerializeField] Gradient gradient = default;
    [SerializeField] Color connectionColor = Color.cyan;
    [SerializeField] float radius = .1f;
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

        #endregion

        Handles.color = gradient.Evaluate((float)0/(points.Length - 1));
        Handles.SphereHandleCap(0, points[0], Quaternion.identity, radius, EventType.Repaint);
        Handles.Label(points[0] + Camera.current.transform.right * .25f, $"X={points[0].x} \n Y={points[0].y} \n Z={points[0].z}");

        Handles.color = gradient.Evaluate((float)1 / (points.Length - 1));
        Handles.SphereHandleCap(0, points[1], Quaternion.identity, radius, EventType.Repaint);
        Handles.Label(points[1] + Camera.current.transform.right * .25f, $"X={points[1].x} \n Y={points[1].y} \n Z={points[1].z}");

        Handles.color = gradient.Evaluate((float)2 / (points.Length - 1));
        Handles.SphereHandleCap(0, points[2], Quaternion.identity, radius, EventType.Repaint);
        Handles.Label(points[2] + Camera.current.transform.right * .25f, $"X={points[2].x} \n Y={points[2].y} \n Z={points[2].z}");

        Handles.color = gradient.Evaluate((float)3 / (points.Length - 1));
        Handles.SphereHandleCap(0, points[3], Quaternion.identity, radius, EventType.Repaint);
        Handles.Label(points[3] + Camera.current.transform.right * .25f, $"X={points[3].x} \n Y={points[3].y} \n Z={points[3].z}");

        Handles.color = gradient.Evaluate((float)4 / (points.Length - 1));
        Handles.SphereHandleCap(0, points[4], Quaternion.identity, radius, EventType.Repaint);
        Handles.Label(points[4] + Camera.current.transform.right * .25f, $"X={points[4].x} \n Y={points[4].y} \n Z={points[4].z}");

        Handles.color = gradient.Evaluate((float)4 / (points.Length - 1));
        Handles.SphereHandleCap(0, points[5], Quaternion.identity, radius, EventType.Repaint);
        Handles.Label(points[5] + Camera.current.transform.right * .25f, $"X={points[5].x} \n Y={points[5].y} \n Z={points[5].z}");

        Handles.color = gradient.Evaluate((float)5 / (points.Length - 1));
        Handles.SphereHandleCap(0, points[6], Quaternion.identity, radius, EventType.Repaint);
        Handles.Label(points[6] + Camera.current.transform.right * .25f, $"X={points[6].x} \n Y={points[6].y} \n Z={points[6].z}");

        Handles.color = gradient.Evaluate((float)6 / (points.Length - 1));
        Handles.SphereHandleCap(0, points[7], Quaternion.identity, radius, EventType.Repaint);
        Handles.Label(points[7] + Camera.current.transform.right * .25f, $"X={points[7].x} \n Y={points[7].y} \n Z={points[7].z}");

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
