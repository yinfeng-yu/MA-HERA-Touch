using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public class MapGenerator : MonoBehaviour
    {
        public Texture2D heightMap;
        public float colorThreshold = 0.5f;
        public float wallHeight = 0.5f;

        [Range(0f, 1f)]
        public float mapScale = 0.9f;

        // public Material wallMaterial;
        public Transform wallsParent;

        public GameObject wallPrefab;
        private int _size;

        private float _scaleMultiplier = 1;

        private List<MeshFilter> _wallsMeshList;

        public GameObject wallMesh;

        public NavigationBaker navigationBaker;

        // Start is called before the first frame update
        void Start()
        {
            if (heightMap.width != heightMap.height)
            {
                Debug.LogError("The input height map is required to be square!");
            }
            _size = heightMap.width;

            _wallsMeshList = new List<MeshFilter>();

            _scaleMultiplier = 10f * mapScale / _size;

            CreateWalls();

            // Combine Meshes

            // MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[_wallsMeshList.Count];

            int i = 0;
            while (i < _wallsMeshList.Count)
            {
                combine[i].mesh = _wallsMeshList[i].sharedMesh;
                combine[i].transform = _wallsMeshList[i].transform.localToWorldMatrix;
                _wallsMeshList[i].gameObject.SetActive(false);

                i++;
            }

            Mesh mesh = new Mesh();
            mesh.CombineMeshes(combine);
            wallMesh.GetComponent<MeshFilter>().sharedMesh = mesh;
            wallMesh.gameObject.SetActive(true);

            wallMesh.GetComponent<MeshCollider>().sharedMesh = mesh;

            wallsParent.gameObject.SetActive(false);

            navigationBaker.BakeNavMesh();
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
            // var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var go = Instantiate(wallPrefab, wallsParent);

            go.transform.position = position;
            go.transform.localScale = scale;
            go.name = "Wall Unit (" + index.ToString() + ")";
            // color is controlled like this
            // go.GetComponent<MeshRenderer>().material = wallMaterial;
            _wallsMeshList.Add(go.GetComponent<MeshFilter>());
        }

    }

}
