using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LemonzapUtils;
public class ViewCone : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    public float fov = 50f;
    public float viewDistance = 5f;
    public int rayCount = 50;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 origin = transform.position;
        float angle = transform.rotation.eulerAngles.z + fov/2;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector2[] polygonPoints = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = transform.InverseTransformPoint(origin);
        polygonPoints[0] = transform.InverseTransformPoint(origin);

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Utils.GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                //no hit
                vertex = origin + Utils.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //hit object
                vertex = new Vector3(raycastHit2D.point.x, raycastHit2D.point.y, transform.position.z);
            }
            vertices[vertexIndex] = transform.InverseTransformPoint(vertex);
            polygonPoints[vertexIndex] = vertices[vertexIndex];

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        GetComponent<PolygonCollider2D>().points = polygonPoints;
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
