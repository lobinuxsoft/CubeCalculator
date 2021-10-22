using UnityEngine;

public class ManualResterizer : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float fieldOfViewOffset = 1f;
    [SerializeField, Range(8, 512)] int rasterResolution = 32;
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

        UseDataFromMesh();
    }

    /// <summary>
    /// Obtengo los datos del frustum para visualizarlo.
    /// </summary>
    private void VisualizeFrustrum()
    {
        if (cam != null)
        {

            // Punto medio
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(cam.transform.position + cam.transform.forward * cam.farClipPlane, cam.farClipPlane * .05f);

            // Esquinas del frustum
            corners = new Vector3[4];

            cam.CalculateFrustumCorners(new Rect(0,0,1,1), cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, corners);

            for (int i = 0; i < corners.Length; i++)
            {
                corners[i] = cam.transform.TransformVector(corners[i]);
            }

            for (int i = 0; i < corners.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(cam.transform.position, corners[i]);
                Gizmos.DrawSphere(corners[i], cam.farClipPlane * .05f);
            }

            RasterGrid(corners[0], corners[1], corners[2], corners[3], rasterResolution);
        }
    }

    /// <summary>
    /// Uso todos los datos de las mesh que estan dentro de la escena para hacer los respectivos calculos (prendo o apago el game object)
    /// </summary>
    private void UseDataFromMesh()
    {
        foreach (var item in filters)
        {
            Matrix4x4 localToWorld = item.transform.localToWorldMatrix;
            int i = 0;

            int counter = 0;

            // Para calcular las normales necesito el indice de grupo de vertices, para saber cuales forman una cara
            for (; i < item.mesh.GetIndices(0).Length;)
            {
                // Tomo los vertices ordenados proporcionados por unity

                Vector3 v1 = item.mesh.vertices[item.mesh.GetIndices(0)[i]];
                Vector3 v2 = item.mesh.vertices[item.mesh.GetIndices(0)[i + 1]];
                Vector3 v3 = item.mesh.vertices[item.mesh.GetIndices(0)[i + 2]];

                // Calculo el punto medio de la cara.
                Vector3 middlePoint = localToWorld.MultiplyPoint3x4((v1 + v2 + v3) / 3);

                // Normal de un triangulo
                Vector3 normal = NormalFromTriangle(v1, v2, v3);
                Vector3 normalInWorld = localToWorld.MultiplyPoint3x4(normal);

                Gizmos.color = Color.red;

                // Reviso si uno de los vertices esta dentro de la camara para que se visualice el objeto.
                bool vertexInCam = InCamView((cam.transform.position - localToWorld.MultiplyPoint3x4(v1)).normalized, -cam.transform.forward) ||
                                   InCamView((cam.transform.position - localToWorld.MultiplyPoint3x4(v2)).normalized, -cam.transform.forward) ||
                                   InCamView((cam.transform.position - localToWorld.MultiplyPoint3x4(v3)).normalized, -cam.transform.forward);

                // Muestro los vertices de las mesh
                if (vertexInCam)
                {
                    Gizmos.DrawSphere(localToWorld.MultiplyPoint3x4(v1), .05f);
                    Gizmos.DrawSphere(localToWorld.MultiplyPoint3x4(v2), .05f);
                    Gizmos.DrawSphere(localToWorld.MultiplyPoint3x4(v3), .05f);

                    // Muestro el pumto medio de la cara
                    if (InCamView((cam.transform.position - middlePoint).normalized, -cam.transform.forward))
                    {
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawSphere(middlePoint, .05f);
                    }

                    // Muestro la normal de las caras que se estarian viendo
                    if (InCamView((cam.transform.position - normalInWorld).normalized, -cam.transform.forward))
                    {
                        // Calculo el angulo de la normal de la cara con respecto a donde mira la camara para saber si es visible o no.

                        float normalAngle = Vector3.Dot(-cam.transform.forward, (item.transform.position - normalInWorld).normalized);

                        if (normalAngle < 0)
                        {
                            counter++;
                            Gizmos.color = Color.blue;
                            Gizmos.DrawSphere(normalInWorld, .05f);
                        }
                    }
                }

                i += 3;
            }

            item.gameObject.SetActive(counter > 0);
        }
    }

    /// <summary>
    /// Reviso que la coordenada que le paso esta dentro del frustrum
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    bool InCamView(Vector3 from, Vector3 to)
    {
        float angle = Vector3.Angle(from, to);

        if (angle < cam.fieldOfView * fieldOfViewOffset && angle > -cam.fieldOfView * fieldOfViewOffset)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Dibujo la grilla del frustrum solo para visualizar
    /// </summary>
    /// <param name="leftDown"></param>
    /// <param name="leftUp"></param>
    /// <param name="rightUp"></param>
    /// <param name="rightDown"></param>
    /// <param name="resolution"></param>
    void RasterGrid(Vector3 leftDown, Vector3 leftUp, Vector3 rightUp, Vector3 rightDown, int resolution)
    {
        Vector3[] upV = new Vector3[resolution];
        Vector3[] downV = new Vector3[resolution];
        Vector3[] leftV = new Vector3[resolution/2];
        Vector3[] rightV = new Vector3[resolution/2];

        Gizmos.color = Color.green;

        // Busco los puntos de la grilla de arriba. 
        for (int i = 0; i < resolution; i++)
        {
            upV[i] = Vector3.Lerp(leftUp, rightUp, (float)i / resolution);
        }

        // Busco los puntos de la grilla de abajo
        for (int i = 0; i < resolution; i++)
        {
            downV[i] = Vector3.Lerp(leftDown, rightDown, (float)i / resolution);
        }

        // Busco los puntos de la grilla de la izquierda
        for (int i = 0; i < resolution/2; i++)
        {
            leftV[i] = Vector3.Lerp(leftUp, leftDown, (float)i / (resolution/2));
        }

        // Busco los puntos de la grilla de la derecha
        for (int i = 0; i < resolution / 2; i++)
        {
            rightV[i] = Vector3.Lerp(rightUp, rightDown, (float)i / (resolution / 2));
        }

        // dibujo las lineas verticales
        for (int i = 0; i < resolution; i++)
        {
            Gizmos.DrawLine(upV[i], downV[i]);
        }

        // dibujo las lineas horizontales
        for (int i = 0; i < resolution/2; i++)
        {
            Gizmos.DrawLine(leftV[i], rightV[i]);
        }
    }

    /// <summary>
    /// Calculo la normal del triangulo en base a los vertices ordenados
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    private Vector3 NormalFromTriangle(Vector3 a, Vector3 b, Vector3 c) 
    {
        Vector3 dir = Vector3.Cross(b - a, c - a);
        Vector3 norm = Vector3.Normalize(dir);
        return norm;
    }
}