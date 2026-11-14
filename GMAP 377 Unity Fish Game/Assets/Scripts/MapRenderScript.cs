using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MapRenderScript : MonoBehaviour
{
    void Start()
    {
        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        Mesh mesh = poly.CreateMesh(true, true);
        meshFilter.mesh = mesh;
    }
}
