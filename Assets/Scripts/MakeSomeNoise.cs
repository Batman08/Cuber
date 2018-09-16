using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSomeNoise : MonoBehaviour
{
    public float Power = 3;
    public float Scale = 1;
    public float TimeScale = 1;

    private float _offsetX;
    private float _offsetY;
    private MeshFilter _meshFilter;

    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        MakeNoise();
    }

    void Update()
    {
        MakeNoise();
        _offsetX += Time.deltaTime * TimeScale;
        _offsetY += Time.deltaTime * TimeScale;
    }

    private void MakeNoise()
    {
        Vector3[] vertices = _meshFilter.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = CalculateHeight(vertices[i].x, vertices[i].z * Power);

            _meshFilter.mesh.vertices = vertices;
        }
    }

    private float CalculateHeight(float x, float y)
    {
        float xCord = x * Scale + _offsetX;
        float yCord = y * Scale + _offsetY;

        return Mathf.PerlinNoise(xCord, yCord);
    }
}
