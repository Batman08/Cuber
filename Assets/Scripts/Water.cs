using UnityEngine;
using System.Collections;



public class Water : MonoBehaviour
{
    Vector3 waveSource1 = new Vector3(2.0f, 0.0f, 2.0f);
    public float WaveFrequency = 0.53f;
    public float WaveHeight = 0.48f;
    public float WaveLength = 0.71f;
    public bool EdgeBlend = true;
    public bool ForceFlatShading = true;

    private Mesh _mesh;
    private Vector3[] _verts;

    void Start()
    {
        Camera.main.depthTextureMode |= DepthTextureMode.Depth;
        MeshFilter mf = GetComponent<MeshFilter>();
        MakeMeshLowPoly(mf);

    }

    MeshFilter MakeMeshLowPoly(MeshFilter mf)
    {
        _mesh = mf.sharedMesh;//Change to sharedmesh? 
        Vector3[] oldVerts = _mesh.vertices;
        int[] triangles = _mesh.triangles;
        Vector3[] vertices = new Vector3[triangles.Length];
        for (int i = 0; i < triangles.Length; i++)
        {
            vertices[i] = oldVerts[triangles[i]];
            triangles[i] = i;
        }
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
        _verts = _mesh.vertices;
        return mf;
    }

    void SetEdgeBlend()
    {
        if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
        {
            EdgeBlend = false;
        }
        if (EdgeBlend)
        {
            Shader.EnableKeyword("WATER_EDGEBLEND_ON");
            if (Camera.main)
            {
                Camera.main.depthTextureMode |= DepthTextureMode.Depth;
            }
        }
        else
        {
            Shader.DisableKeyword("WATER_EDGEBLEND_ON");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalcWave();
        SetEdgeBlend();
    }

    void CalcWave()
    {
        for (int i = 0; i < _verts.Length; i++)
        {
            Vector3 v = _verts[i];
            v.y = 0.0f;
            float dist = Vector3.Distance(v, waveSource1);
            dist = (dist % WaveLength) / WaveLength;
            v.y = WaveHeight * Mathf.Sin(Time.time * Mathf.PI * 2.0f * WaveFrequency
            + (Mathf.PI * 2.0f * dist));
            _verts[i] = v;
        }
        _mesh.vertices = _verts;
        _mesh.RecalculateNormals();
        _mesh.MarkDynamic();

        GetComponent<MeshFilter>().mesh = _mesh;
    }
}