using System;
using UnityEngine;

public class TriangleGeneration : MonoBehaviour
{
    [SerializeField] private Material red;
    private int[] _indices = { 0, 1, 2 };

    private GameObject _triangleOneGameObject;
    private GameObject _triangleTwoGameObject;
    private GameObject _triangleThreeGameObject;
    private GameObject _triangleFourGameObject;
    
    private static Vector3[] _point =
    {
        new Vector3(0, 12, 0),
        new Vector3(56, 8, 0),
        new Vector3(0, 0.5f, 56),
        new Vector3(56, 9, 56),
        new Vector3(0, 0.5f, 112),
        new Vector3(56, 11.5f, 112)
    };
    
    private Vector3[] _triangleOneVertices =
    {
        _point[0],
        _point[1],
        _point[2]
    };

    private Vector3[] _triangleTwoVertices =
    {
        _point[1],
        _point[2],
        _point[3]
    };

    private Vector3[] _triangleThreeVertices =
    {
        _point[2],
        _point[3],
        _point[5]
    };

    private Vector3[] _triangleFourVertices =
    {
        _point[2],
        _point[5],
        _point[4]
    };

    private void Start()
    {
        GenerateGameObjectChild();
        GenerateTriangle();
    }

    private void GenerateGameObjectChild()
    {
        _triangleOneGameObject = new GameObject("FirstTriangle");
        _triangleTwoGameObject = new GameObject("SecondTriangle");
        _triangleThreeGameObject = new GameObject("ThirdTriangle");
        _triangleFourGameObject = new GameObject("FourthTriangle");
        
        _triangleOneGameObject.transform.parent = transform;
        _triangleOneGameObject.transform.localPosition = Vector3.zero;
        _triangleTwoGameObject.transform.parent = transform;
        _triangleTwoGameObject.transform.localPosition = Vector3.zero; 
        _triangleThreeGameObject.transform.parent = transform;
        _triangleThreeGameObject.transform.localPosition = Vector3.zero;
        _triangleFourGameObject.transform.parent = transform;
        _triangleFourGameObject.transform.localPosition = Vector3.zero;
    }

    private void GenerateTriangle()
    {
        // Adds MeshFilter and MeshRenderer components to the GameObject
        _triangleOneGameObject.AddComponent<MeshFilter>();
        _triangleOneGameObject.AddComponent<MeshRenderer>();
        
        _triangleTwoGameObject.AddComponent<MeshFilter>();
        _triangleTwoGameObject.AddComponent<MeshRenderer>();
        
        _triangleThreeGameObject.AddComponent<MeshFilter>();
        _triangleThreeGameObject.AddComponent<MeshRenderer>();
        
        _triangleFourGameObject.AddComponent<MeshFilter>();
        _triangleFourGameObject.AddComponent<MeshRenderer>();
        
        // Creates a new mesh and defines the vertices and triangles (indices)
        var triangleOneMesh = new Mesh
        {
            vertices = _triangleOneVertices,
            triangles = _indices
        };
        var triangleTwoMesh = new Mesh
        {
            vertices = _triangleTwoVertices,
            triangles = _indices
        };
        var triangleThreeMesh = new Mesh
        {
            vertices = _triangleThreeVertices,
            triangles = _indices
        };
        var triangleFourMesh = new Mesh
        {
            vertices = _triangleFourVertices,
            triangles = _indices
        };
        
        // Recalculates normals for correct shading
        triangleOneMesh.RecalculateNormals();
        triangleTwoMesh.RecalculateNormals();
        triangleThreeMesh.RecalculateNormals();
        triangleFourMesh.RecalculateNormals();

        // Assigns the mesh to the MeshFilter component, and assigns the material to the MeshRenderer
        _triangleOneGameObject.GetComponent<MeshFilter>().mesh = triangleOneMesh;
        _triangleOneGameObject.GetComponent<MeshRenderer>().material = red;
        
        _triangleTwoGameObject.GetComponent<MeshFilter>().mesh = triangleTwoMesh;
        _triangleTwoGameObject.GetComponent<MeshRenderer>().material = red;
        
        _triangleThreeGameObject.GetComponent<MeshFilter>().mesh = triangleThreeMesh;
        _triangleThreeGameObject.GetComponent<MeshRenderer>().material = red;
        
        _triangleFourGameObject.GetComponent<MeshFilter>().mesh = triangleFourMesh;
        _triangleFourGameObject.GetComponent<MeshRenderer>().material = red;
    }
}
