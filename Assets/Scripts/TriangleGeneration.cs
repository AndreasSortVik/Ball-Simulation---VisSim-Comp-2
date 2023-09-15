using System;
using UnityEngine;

public class TriangleGeneration : MonoBehaviour
{
    [SerializeField] private Material red;
    
    private Vector3[] _triangleVertices =
    {
        new Vector3(0, 12, 0),
        new Vector3(56, 8, 0),
        new Vector3(0, 0.5f, 56)
    };

    private void Start()
    {
        GenerateTriangle();
    }

    private void GenerateTriangle()
    {
        // Adds MeshFilter and MeshRenderer components to the GameObject
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        
        // Creates a new mesh
        var triangleMesh = new Mesh
        {
            // Defines the vertices and triangles (indices)
            vertices = _triangleVertices,
            triangles = new int[] { 0, 1, 2 }
        };
        
        // Recalculates normals for correct shading
        triangleMesh.RecalculateNormals();

        // Assigns the mesh to the MeshFilter component, and assigns the material to the MeshRenderer
        GetComponent<MeshFilter>().mesh = triangleMesh;
        GetComponent<MeshRenderer>().material = red;
    }
}
