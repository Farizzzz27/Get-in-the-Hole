using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CurvePlane : MonoBehaviour
{
    public float curveStrength = 1f; // kekuatan lengkungan

    void Start()
    {
        // Mendapatkan mesh dari plane
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        // Melengkungkan setiap vertex di sumbu Z berdasarkan sumbu X
        for (int i = 0; i < vertices.Length; i++)
        {
            float xPos = vertices[i].x;
            vertices[i].z += Mathf.Sin(xPos) * curveStrength;
        }

        // Memperbarui posisi vertex pada mesh
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
