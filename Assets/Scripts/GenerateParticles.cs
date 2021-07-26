using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateParticles : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform[] models;
    [SerializeField] private ParticleSystem psPrefab;
    void Start()
    {
        foreach (Transform m in models)
        {
            MakeParticles(m);
        }
    }

    void MakeParticles(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = parent.GetChild(i).gameObject;
            MeshRenderer meshR = child.GetComponent<MeshRenderer>();

            ParticleSystem ps = Instantiate(psPrefab, transform.position, Quaternion.identity);
            ps.name = "ParticleSystem" + i.ToString();
            var sh = ps.shape;
            sh.enabled = true;
            sh.shapeType = ParticleSystemShapeType.MeshRenderer;
            sh.meshRenderer = meshR;

            MeshFilter meshF = child.GetComponent<MeshFilter>();
            float surfaceArea = getSurfaceArea(meshF.mesh);
            var emission = ps.emission;
            emission.enabled = true;
            emission.rateOverTime = surfaceArea;
            
            ps.maxParticles = (int) surfaceArea * 100;
        }
    }

    public float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }

    public float VolumeOfMesh(Mesh mesh)
    {
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        return Mathf.Abs(volume);
    }

    public float getSurfaceArea(Mesh mesh)
    {
        float result = 0f;
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;
        
        for(int p = 0; p < triangles.Length; p += 3)
        {
            result += (Vector3.Cross(vertices[triangles[p+1]] - vertices[triangles[p]],
                        vertices[triangles[p+2]] - vertices[triangles[p]])).magnitude;
        }
        return result * 0.5f;
    }
}
