using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(MeshFilter))]
public class MapGenerator : MonoBehaviour
{
    public Texture2D heightMap;
    public float colorThreshold = 0.5f;
    public float wallHeight = 0.5f;

    [Range(0f, 1f)]
    public float mapScale = 0.9f;

    public Material wallMaterial;
    public Transform wallsParent;

    // private Mesh _mesh;

    private int _size;

    // private Vector3[] _vertices;
    // private int[] _triangles;

    private float _scaleMultiplier = 1;

    // private List<Vector3> _verticesList;
    private List<GameObject> _cubesList;

    // Start is called before the first frame update
    void Start()
    {
        if (heightMap.width != heightMap.height)
        {
            Debug.LogError("The input height map is required to be square!");
        }
        _size = heightMap.width;

        _cubesList = new List<GameObject>();

        _scaleMultiplier = 10f * mapScale / _size;

        //_mesh = new Mesh();
        //GetComponent<MeshFilter>().mesh = _mesh;

        CreateWalls();
        // UpdateMesh();
        
    }

    void CreateWalls()
    {
        int index = 0;
        for (int z = 0; z < _size; z++)
        {
            for (int x = 0; x < _size; x++)
            {
                Color color = heightMap.GetPixel(x, z);

                if (color.r > colorThreshold)
                {
                    CreateWallUnit(new Vector3(x * _scaleMultiplier, wallHeight / 2, z * _scaleMultiplier) - new Vector3(5f * mapScale, 0, 5f * mapScale), 
                                   new Vector3(_scaleMultiplier, wallHeight, _scaleMultiplier),
                                   index);
                    index++;
                }
            }
        }
    }

    void CreateWallUnit(Vector3 position, Vector3 scale, int index)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(wallsParent);

        cube.transform.position = position;
        cube.transform.localScale = scale;
        cube.name = "Wall Unit (" + index.ToString() + ")";
        // color is controlled like this
        cube.GetComponent<MeshRenderer>().material = wallMaterial;
        _cubesList.Add(cube);
    }

    // void CreateMesh()
    // {
    //     _vertices = new Vector3[(_width + 1) * (_height + 1)];
    // 
    //     int index = 0;
    //     for (int z = 0; z < _height + 1; z++) 
    //     {
    //         for (int x = 0; x < _width + 1; x++)
    //         {
    //             Color color_1 = heightMap.GetPixel(x, z);
    //             Color color_2 = heightMap.GetPixel(x + 1, z);
    //             Color color_3 = heightMap.GetPixel(x, z + 1);
    //             Color color_4 = heightMap.GetPixel(x + 1, z + 1);
    // 
    //             // Check the color around the vertex
    //             int color_1_gt = color_1.r > colorThreshold ? 1 : 0;
    //             int color_2_gt = color_2.r > colorThreshold ? 1 : 0;
    //             int color_3_gt = color_3.r > colorThreshold ? 1 : 0;
    //             int color_4_gt = color_4.r > colorThreshold ? 1 : 0;
    // 
    //             int color_sum = color_1_gt + color_2_gt + color_3_gt + color_4_gt;
    // 
    //             if (color_sum > 0) // If the vertex has a wall around.
    //             {
    //                 if (color_sum != 4) // If the vertex is not surrounded by walls, add an extra vertex.
    //                 {
    //                     if (_verticesList.Count > 0 && _verticesList[_verticesList.Count - 1].y == wallHeight)
    //                     {
    //                         _verticesList.Add(new Vector3(x, wallHeight, z));
    //                         _verticesList.Add(new Vector3(x,          0, z));
    //                     }
    //                     else
    //                     {
    //                         _verticesList.Add(new Vector3(x,          0, z));
    //                         _verticesList.Add(new Vector3(x, wallHeight, z));
    //                     }
    //                 }
    //                 else
    //                 {
    //                     _verticesList.Add(new Vector3(x, 0, z));
    //                 }
    //             }
    // 
    //             index++;
    //         }
    //     }
    // 
    //     _triangles = new int[6 * _width * _height];
    // 
    //     int vert = 0;
    //     int tris = 0;
    // 
    //     for (int z = 0; z < _height; z++)
    //     {
    //         for (int x = 0; x < _width; x++)
    //         {
    //             _triangles[tris] =     vert + 0;
    //             _triangles[tris + 1] = vert + _width + 1;
    //             _triangles[tris + 2] = vert + 1;
    //             _triangles[tris + 3] = vert + 1;
    //             _triangles[tris + 4] = vert + _width + 1;
    //             _triangles[tris + 5] = vert + _width + 2;
    // 
    //             vert++;
    //             tris += 6;
    //         }
    //         vert++;
    //     }
    // }

    // void UpdateMesh()
    // {
    //     _mesh.Clear();
    //     // _mesh.vertices = _verticesList.ToArray();
    //     _mesh.triangles = _triangles;
    // }

    // private void OnDrawGizmos()
    // {
    //     for (int i = 0; i < _verticesList.Count; i++)
    //     {
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawSphere(_verticesList.ToArray()[i], 0.1f);
    //     }
    // }
}
