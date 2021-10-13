using System.Collections.Generic;
using UnityEngine;

public class ManualResterizer : MonoBehaviour
{
    [SerializeField] Vector3[] corners = new Vector3[4];
    [SerializeField] MeshFilter[] filters;
    [SerializeField] List<Indices> indices = new List<Indices>();

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        filters = FindObjectsOfType<MeshFilter>();

        foreach (var item in filters)
        {
            Indices ind = new Indices { indices = item.mesh.GetIndices(0) };
            indices.Add(ind);
        }
    }

    private void OnDrawGizmos()
    {
        if(cam != null)
        {

            // Punto medio
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(cam.transform.forward * cam.farClipPlane, cam.farClipPlane * .05f);

            float frustumHeight = 2.0f * cam.farClipPlane * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float frustumWidth = frustumHeight * cam.aspect;

            // Esquinas del frustum
            corners = new Vector3[4];
            corners[0] = cam.transform.position + (cam.transform.forward * cam.farClipPlane) + (cam.transform.right * (-frustumWidth *.5f)) + (cam.transform.up * (frustumHeight * .5f));   // izquierda arriba
            corners[1] = cam.transform.position + (cam.transform.forward * cam.farClipPlane) + (cam.transform.right * (frustumWidth * .5f)) + (cam.transform.up * (frustumHeight * .5f));   // derecha arriba
            corners[2] = cam.transform.position + (cam.transform.forward * cam.farClipPlane) + (cam.transform.right * (-frustumWidth * .5f)) + (cam.transform.up * (-frustumHeight * .5f));   // izquierda abajo
            corners[3] = cam.transform.position + (cam.transform.forward * cam.farClipPlane) + (cam.transform.right * (frustumWidth * .5f)) + (cam.transform.up * (-frustumHeight * .5f));   // derecha abajo

            for (int i = 0; i < corners.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(cam.transform.position, corners[i]);
                Gizmos.DrawSphere(corners[i], cam.farClipPlane * .05f);
            }
        }

        //foreach (var item in filters)
        //{
        //    for (int i = 0; i < item.mesh.GetIndices(0).Length; i++)
        //    {

        //    }
        //}
    }

    private Vector3 NormalFromTriangle(Vector3 a, Vector3 b, Vector3 c) 
    {
        Vector3 dir = Vector3.Cross(b - a, c - a);
        Vector3 norm = Vector3.Normalize(dir);
        return norm;
    }
}

[System.Serializable]
public struct Indices
{
    public int[] indices;
}
