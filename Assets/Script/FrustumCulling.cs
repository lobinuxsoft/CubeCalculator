using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FrustumCulling : MonoBehaviour
{
    const uint maxVertexPerPlane = 4;
    [SerializeField] Color frustumLineColor = Color.green;
    [SerializeField] Color frustumPlaneColor = Color.green;
    Vector3[] frustumCornerFar = new Vector3[maxVertexPerPlane];
    Vector3[] frustumCornerNear = new Vector3[maxVertexPerPlane];
    Vector3[] frustumCornerLeft = new Vector3[maxVertexPerPlane];
    Vector3[] frustumCornerRight = new Vector3[maxVertexPerPlane];
    Vector3[] frustumCornerUp = new Vector3[maxVertexPerPlane];
    Vector3[] frustumCornerDown = new Vector3[maxVertexPerPlane];

    Camera cam = default;

    private void Start()
    {
        cam = Camera.main;

        CalculateFrustum();
    }

    private void Update()
    {
        CalculateFrustum();
    }

    private void CalculateFrustum()
    {
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCornerFar);
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCornerNear);

        // Se calcula los vertices del plano izquierdo del frustum
        frustumCornerLeft[0] = frustumCornerNear[0];
        frustumCornerLeft[1] = frustumCornerFar[0];
        frustumCornerLeft[2] = frustumCornerFar[1];
        frustumCornerLeft[3] = frustumCornerNear[1];

        // Se calcula los vertices del plano derecho del frustum
        frustumCornerRight[0] = frustumCornerNear[2];
        frustumCornerRight[1] = frustumCornerFar[2];
        frustumCornerRight[2] = frustumCornerFar[3];
        frustumCornerRight[3] = frustumCornerNear[3];

        // Se calcula los vertices del plano superior del frustum
        frustumCornerUp[0] = frustumCornerNear[1];
        frustumCornerUp[1] = frustumCornerFar[1];
        frustumCornerUp[2] = frustumCornerFar[2];
        frustumCornerUp[3] = frustumCornerNear[2];

        // Se calcula los vertices del plano inferior del frustum
        frustumCornerDown[0] = frustumCornerNear[3];
        frustumCornerDown[1] = frustumCornerFar[3];
        frustumCornerDown[2] = frustumCornerFar[0];
        frustumCornerDown[3] = frustumCornerNear[0];

        for (int i = 0; i < maxVertexPerPlane; i++)
        {
            frustumCornerFar[i] =  cam.transform.localToWorldMatrix.MultiplyPoint3x4(frustumCornerFar[i]);
            frustumCornerNear[i] = cam.transform.localToWorldMatrix.MultiplyPoint3x4(frustumCornerNear[i]);
            frustumCornerLeft[i] = cam.transform.localToWorldMatrix.MultiplyPoint3x4(frustumCornerLeft[i]);
            frustumCornerRight[i] = cam.transform.localToWorldMatrix.MultiplyPoint3x4(frustumCornerRight[i]);
            frustumCornerUp[i] = cam.transform.localToWorldMatrix.MultiplyPoint3x4(frustumCornerUp[i]);
            frustumCornerDown[i] = cam.transform.localToWorldMatrix.MultiplyPoint3x4(frustumCornerDown[i]);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3[] tempDrawVertex = new Vector3[5];

        // Visualizo el Frustum far plane
        Handles.color = frustumPlaneColor;
        Handles.DrawAAConvexPolygon(frustumCornerFar);
        Handles.color = frustumLineColor;
        tempDrawVertex = new Vector3[] { frustumCornerFar[0] , frustumCornerFar[1] , frustumCornerFar[2] , frustumCornerFar[3] , frustumCornerFar[0] };
        Handles.DrawAAPolyLine(tempDrawVertex);

        // Visualizo el Frustum near plane
        Handles.color = frustumPlaneColor;
        Handles.DrawAAConvexPolygon(frustumCornerNear);
        Handles.color = frustumLineColor;
        tempDrawVertex = new Vector3[] { frustumCornerNear[0], frustumCornerNear[1], frustumCornerNear[2], frustumCornerNear[3], frustumCornerNear[0] };
        Handles.DrawAAPolyLine(tempDrawVertex);

        // Visualizo el Frustum left plane
        Handles.color = frustumPlaneColor;
        Handles.DrawAAConvexPolygon(frustumCornerLeft);
        Handles.color = frustumLineColor;
        tempDrawVertex = new Vector3[] { frustumCornerLeft[0], frustumCornerLeft[1], frustumCornerLeft[2], frustumCornerLeft[3], frustumCornerLeft[0] };
        Handles.DrawAAPolyLine(tempDrawVertex);

        // Visualizo el Frustum right plane
        Handles.color = frustumPlaneColor;
        Handles.DrawAAConvexPolygon(frustumCornerRight);
        Handles.color = frustumLineColor;
        tempDrawVertex = new Vector3[] { frustumCornerRight[0], frustumCornerRight[1], frustumCornerRight[2], frustumCornerRight[3], frustumCornerRight[0] };
        Handles.DrawAAPolyLine(tempDrawVertex);

        // Visualizo el Frustum up plane
        Handles.color = frustumPlaneColor;
        Handles.DrawAAConvexPolygon(frustumCornerUp);
        Handles.color = frustumLineColor;
        tempDrawVertex = new Vector3[] { frustumCornerUp[0], frustumCornerUp[1], frustumCornerUp[2], frustumCornerUp[3], frustumCornerUp[0] };
        Handles.DrawAAPolyLine(tempDrawVertex);

        // Visualizo el Frustum down plane
        Handles.color = frustumPlaneColor;
        Handles.DrawAAConvexPolygon(frustumCornerDown);
        Handles.color = frustumLineColor;
        tempDrawVertex = new Vector3[] { frustumCornerDown[0], frustumCornerDown[1], frustumCornerDown[2], frustumCornerDown[3], frustumCornerDown[0] };
        Handles.DrawAAPolyLine(tempDrawVertex);
    }
#endif
}
