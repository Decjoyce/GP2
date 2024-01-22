using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MESSINGMESHING : MonoBehaviour
{
    Mesh mesh;
    MeshFilter mf;

    [SerializeField] Vector2 planeSize;
    [SerializeField] int planeResolution;
    [SerializeField] bool isGerstner;
    [SerializeField] Vector4 waveA, waveB, waveC;

    List<Vector3> vertices;
    List<int> triangles;

    private Vector3[] baseVertices;

    private void Awake()
    {
        mesh = new Mesh();
        mf = GetComponent<MeshFilter>();
        mf.mesh = mesh;
                planeResolution = Mathf.Clamp(planeResolution, 1, 50);
        GeneratePlane(planeSize, planeResolution);
        AssignMesh();
    }

    void GeneratePlane(Vector2 size, int resolution)
    {
        vertices = new List<Vector3>();
        float xStep = size.x / resolution;
        float yStep = size.y / resolution;
        for(int y = 0; y < resolution + 1; y++)
        {
            for (int x = 0; x < resolution + 1; x++)
            {
                vertices.Add(new Vector3(x * xStep, 0, y * yStep));
            }
        }

        triangles = new List<int>();
        for (int row = 0; row < resolution; row++)
        {
            for (int column = 0; column < resolution; column++)
            {
                int i = (row * resolution) + row + column;

                triangles.Add(i);
                triangles.Add(i + resolution + 1);
                triangles.Add(i + resolution + 2);

                triangles.Add(i);
                triangles.Add(i + resolution + 2);
                triangles.Add(i + 1);
            }
        }
    }
    
    void AssignMesh()
    {
        //mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    private void FixedUpdate()
    {
        planeResolution = Mathf.Clamp(planeResolution, 1, 50);
        GeneratePlane(planeSize, planeResolution);
        CalculateWave();
        AssignMesh();
    }

    void CalculateWave()
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 p = vertices[i];
            if (isGerstner)
            {
                p += GerstnerWave(waveA, p);
                p += GerstnerWave(waveB, p);
                p += GerstnerWave(waveC, p);
            }

            else
                p.y = waveA.z * Mathf.Sin(p.x - waveA.w * Time.time);
            vertices[i] = p;
        }
    }

    Vector3 GerstnerWave(Vector4 wave, Vector3 p)
    {
        float steepness = wave.z;
        float wavelength = wave.w;
        float k = 2 * Mathf.PI / wavelength;
        float c = Mathf.Sqrt(9.8f / k);
        Vector2 d = Vector3.Normalize(new Vector3(wave.x, wave.y, 0.0f));
        float f = k * (Vector2.Dot(d, new Vector2(p.x, p.z)) - c * Time.fixedUnscaledTime);
        float a = steepness / k;

        return new Vector3(d.x * (a * Mathf.Cos(f)), a * Mathf.Sin(f), d.y * (a * Mathf.Cos(f)));
    }
}
