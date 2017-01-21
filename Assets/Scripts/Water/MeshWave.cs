using UnityEngine;
using System.Collections;

public class MeshWave : MonoBehaviour
{

    public float waveHeight = 10.0f;
    public float speed = 1.0f;
    public float waveLength = 1.0f;
    public float noiseStrength = 4.0f;
    public float noiseWalk = 1.0f;
    public bool diagonalWaves = false;

    private Vector3[] baseHeight;
    private Vector3[] vertices;
    private Vector3 tmpV1 = new Vector3();
    private Vector3 tmpV2 = new Vector3();
    private Vector3 tmpV3 = new Vector3();
    private Vector3 best = new Vector3();
    private Vector3 p1 = new Vector3();
    private Vector3 p2 = new Vector3();
    private Vector3 p3 = new Vector3();
    private Mesh mesh;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        if (baseHeight == null)
        {
            baseHeight = mesh.vertices;
        }
    }

    void Update()
    {
        if (vertices == null)
        {
            vertices = new Vector3[baseHeight.Length];
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseHeight[i];
            if (diagonalWaves)
            {
                vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x * waveLength + baseHeight[i].y * waveLength + baseHeight[i].z * waveLength) * waveHeight;
            }
            else
            {
                vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x * waveLength + baseHeight[i].y * waveLength) * waveHeight;
            }
            vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();


    }

    public float getHeightAtPosition(Ray _ray)
    {
        bool hit = false;
        if (vertices !=null) {
            int[] triangles = mesh.triangles;
            for (int triangle = 0; triangle < triangles.Length;)
            {
                p1 = vertices[triangles[triangle++]];
                p2 = vertices[triangles[triangle++]];
                p3 = vertices[triangles[triangle++]];

                tmpV1 = MeshTools.intersectTriangle(_ray, p1, p2, p3);
                //bool intersects = MeshTools.Intersect(p1, p2, p3, _ray);
                if ( p1.x>-10 && p1.x < 10  && p1.z > -10 && p1.z < 10 )
                {
                  //  Debug.Log("near zero");
                }

                if (tmpV1.y!=-99999)
                {
                    hit = true;
                    //tmpV1 = ((p1 + p2 + p3) / 3);
                    Debug.Log("intersects!");
                    return -tmpV1.x;
                }
            }


 
        }

        return 30f;

    }
}