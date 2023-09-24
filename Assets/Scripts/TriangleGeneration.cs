using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TriangleGeneration : MonoBehaviour
{
    [SerializeField] private Material red;
    public Vector3 oldNormalVector;
    public Vector3 normalVector;
    public int currentTriangleIndex;
    public int oldTriangleIndex;
    public float height;
    
    private static Vector3[] _vertices =
    {
        new Vector3(0, 12, 0),
        new Vector3(0, 0.5f, 56),
        new Vector3(0, 0.5f, 112),
        new Vector3(56, 8, 0),
        new Vector3(56, 9, 56),
        new Vector3(56, 11.5f, 112)
    };
    
    private int[] _indices =
    {
        0, 1, 3,
        1, 4, 3,
        1, 5, 4,
        1, 2, 5
    };

    private void Start()
    {
        GenerateTriangle();
    }

    private void GenerateTriangle()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        var triangleMesh = new Mesh
        {
            vertices = _vertices,
            triangles = _indices
        };

        GetComponent<MeshFilter>().mesh = triangleMesh;
        GetComponent<MeshRenderer>().material = red;
    }
    
    public Vector3 GetBarycentricPoint(Vector2 ballPos)
    {
        Vector3 v1 = new Vector3();
        Vector3 v2 = new Vector3();
        Vector3 v3 = new Vector3();
        Vector3 barycentric = new Vector3();

        // Loops through all triangles to find the correct one
        for (int i = 0; i < _indices.Length / 3; i++)
        {
            int i1 = _indices[i * 3];
            int i2 = _indices[i * 3 + 1];
            int i3 = _indices[i * 3 + 2];
            
            v1 = _vertices[i1];
            v2 = _vertices[i2];
            v3 = _vertices[i3];
            
            barycentric = GetBaryCoords(new Vector2(v1.x, v1.z), new Vector2(v2.x, v2.z), new Vector2(v3.x, v3.z), ballPos);

            if (barycentric is { x: >= 0, y: >= 0, z: >= 0 })
            {
                currentTriangleIndex = i;
                height = barycentric.x * v1.y + barycentric.y * v2.y + barycentric.z * v3.y;
                break;
            }
        }
        
        // Checks if the ball is in a new triangle
        if (oldTriangleIndex != currentTriangleIndex)
        {
            // Calculate normal of previous triangle
            int oldI1 = _indices[oldTriangleIndex];
            int oldI2 = _indices[oldTriangleIndex + 1];
            int oldI3 = _indices[oldTriangleIndex + 2];

            Vector3 oldV1 = _vertices[oldI1];
            Vector3 oldV2 = _vertices[oldI2];
            Vector3 oldV3 = _vertices[oldI3];
            
            oldNormalVector = CalculateNormalVector(oldV1, oldV2, oldV3);
            
            // Calculate normal of current triangle
            oldTriangleIndex = currentTriangleIndex;
            
            print("Collided with new triangle: " + currentTriangleIndex);
        }
        normalVector = CalculateNormalVector(v1, v2, v3);
        return barycentric.x * v1 + barycentric.y * v2 + barycentric.z * v3;
    }

    private Vector3 GetBaryCoords(Vector2 v1, Vector2 v2, Vector2 v3, Vector2 p)
    {
        Vector2 v21 = v2 - v1;
        Vector2 v31 = v3 - v1;
        Vector2 vp1 = p - v1;
        
        float d00 = Vector2.Dot(v21, v21);
        float d01 = Vector2.Dot(v21, v31);
        float d11 = Vector2.Dot(v31, v31);
        float d20 = Vector2.Dot(vp1, v21);
        float d21 = Vector2.Dot(vp1, v31);
        float denom = d00 * d11 - d01 * d01;
        
        float v = (d11 * d20 - d01 * d21) / denom;
        float w = (d00 * d21 - d01 * d20) / denom;
        float u = 1f - v - w;
        
        return new Vector3(u, v, w);
    }

    private Vector3 CalculateNormalVector(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // Calculates two vector along the triangle's edge
        Vector3 v1 = p2 - p1;
        Vector3 v2 = p3 - p1;

        // Calculates the cross product of the two vectors to get the normal vector
        normalVector = Vector3.Cross(v1, v2).normalized;
        return normalVector;
    }
}
