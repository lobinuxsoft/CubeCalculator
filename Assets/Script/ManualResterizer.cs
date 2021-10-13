using UnityEngine;

public class ManualResterizer : MonoBehaviour
{
    [SerializeField] Vector3[] corners = new Vector3[4];
    [SerializeField] MeshFilter[] filters;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        filters = FindObjectsOfType<MeshFilter>();
    }

    private void OnDrawGizmos()
    {
        VisualizeFrustrum();

        foreach (var item in filters)
        {
            Matrix4x4 localToWorld = item.transform.localToWorldMatrix;
            int i = 0;

            // Para calcular las normales necesito el indice de grupo de vertices, para saber cuales forman una cara
            for (; i < item.mesh.GetIndices(0).Length;)
            {
                Vector3 v1 = item.mesh.vertices[item.mesh.GetIndices(0)[i]];
                Vector3 v2 = item.mesh.vertices[item.mesh.GetIndices(0)[i+1]];
                Vector3 v3 = item.mesh.vertices[item.mesh.GetIndices(0)[i+2]];

                // Normal de un triangulo
                Vector3 normal = NormalFromTriangle(v1,v2,v3);

                Gizmos.DrawSphere(localToWorld.MultiplyPoint3x4(normal), .1f);

                i += 3;
            }
        }
    }

    private void VisualizeFrustrum()
    {
        if (cam != null)
        {

            // Punto medio
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(cam.transform.position + cam.transform.forward * cam.farClipPlane, cam.farClipPlane * .05f);

            float frustumHeight = 2.0f * cam.farClipPlane * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float frustumWidth = frustumHeight * cam.aspect;

            // Esquinas del frustum
            corners = new Vector3[4];
            corners[0] = cam.transform.position + (cam.transform.forward * cam.farClipPlane) + (cam.transform.right * (-frustumWidth * .5f)) + (cam.transform.up * (frustumHeight * .5f));   // izquierda arriba
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
