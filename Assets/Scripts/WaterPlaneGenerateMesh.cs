using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlaneGenerateMesh : MonoBehaviour
{
    public float Size = 1;      //104
    public int GridSize = 16;   //52 OR 38

    private MeshFilter _filter;

    void Start()
    {
        _filter = GetComponent<MeshFilter>();
        _filter.mesh = GenerateMesh();
    }

    private Mesh GenerateMesh()
    {
        Mesh m = new Mesh();

        List<Vector3> vertices = new List<Vector3>(); //Stores vertices x,y,z
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>(); //Just stores 2 values (x,y)

        for (int x = 0; x < GridSize + 1; x++)
        {
            for (int y = 0; y < GridSize + 1; y++)
            {
                float _x = -Size * 0.5f + Size * (x / ((float)GridSize));
                float _y = 0f;
                float _z = -Size * 0.5f + Size * (y / ((float)GridSize));
                vertices.Add(new Vector3(_x, _y, _z));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float)GridSize, y / (float)GridSize));
            }
        }

        List<int> triangles = new List<int>();
        int vertCount = GridSize + 1;

        for (int i = 0; i < vertCount * vertCount - vertCount; i++)
        {
            if ((i + 1) % vertCount == 0)
            {
                continue;
            }

            triangles.AddRange(new List<int>() {
                i+1+vertCount, i+vertCount, i,
                i, i+1, i+vertCount+1
            });
        }

        m.SetVertices(vertices);
        m.SetNormals(normals);
        m.SetUVs(0, uvs);
        m.SetTriangles(triangles, 0);

        return m;
    }
}
