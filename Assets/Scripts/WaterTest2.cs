using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterTest2 : MonoBehaviour
{
    [SerializeField] MeshFilter mf1, mf2;    
    Mesh mesh1, mesh2;
    [SerializeField] Vector4 waveA, waveB, waveC;
    private Vector3[] baseVertices;
    private Vector3[] vertices;
    [SerializeField] bool gerstner = true;

    private void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalcNoise();
    }
    void CalcNoise()
    {
        mesh1 = mf1.mesh;
        mesh2 = mf2.mesh;
        if (baseVertices == null)
            baseVertices = mesh1.vertices;
        vertices = new Vector3[baseVertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 p = baseVertices[i];
            if (gerstner)
            {
                p += GerstnerWave(waveA, p);
                p += GerstnerWave(waveB, p);
                p += GerstnerWave(waveC, p);
            }

            else
                p.y = waveA.z * Mathf.Sin(p.x - waveA.w * Time.time);
            vertices[i] = p;
        }
        mesh1.vertices = vertices;
        mesh2.vertices = vertices;

        mesh1.RecalculateNormals();
        mesh2.RecalculateNormals();
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


