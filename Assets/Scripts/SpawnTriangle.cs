using System;
using UnityEngine;

public class SpawnTriangle : MonoBehaviour
{
    private Vector3[] _triangleVertices =
    {
        new Vector3(0, 12, 0),
        new Vector3(56, 8, 0),
        new Vector3(0, 0.5f, 56)
    };

    private void Start()
    {
        // Creates a new mesh
        Mesh mesh = new Mesh
        {
            // Define the vertices, normals and triangles (indices)
            vertices = _triangleVertices,
            normals = new Vector3[3] { Vector3.forward, Vector3.forward, Vector3.forward },
            triangles = new int[] { 0, 1, 2 }
        };

        // Assign the mesh to the MeshFilter component
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
