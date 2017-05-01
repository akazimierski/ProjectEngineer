using UnityEngine;
using System.Collections;

public class MapMaker : MonoBehaviour
{
    public GameObject plane;
    Vector3[] vertices = new Vector3[1];

    // Use this for initialization

    public void makeMap(string latLong, int zoom = 14, string kmlpath = "depareusa")
    {
        StaticMapDownload mapDownloader = new StaticMapDownload();
        var mapTex = mapDownloader.downloadImage(latLong, zoom);
        var renderer = plane.GetComponent<Renderer>();
        renderer.material.mainTexture = mapTex;
        renderer.material.shader = Shader.Find("Unlit/Transparent");

        MercatorProjection mp = new MercatorProjection();
        string[] latLongArray = latLong.Split(',');
        Vector2[] corners = mp.getCorners(new Vector2(float.Parse(latLongArray[0]), float.Parse(latLongArray[1])), zoom, 512, 512);
        Debug.Log(corners[0] + ", " + corners[1]);
        var distance = mp.distanceBetweenCoord(corners[0], corners[1]);
        Debug.Log("Distance " + distance);

        float heightRatio = (10 * Mathf.Sqrt(2)) / distance;

        KMLParser parser = (KMLParser)ScriptableObject.CreateInstance(typeof(KMLParser));
        parser.getXmlData(corners, kmlpath);
        vertices = parser.getVertices(heightRatio);

        Triangulation delaunay = (Triangulation)ScriptableObject.CreateInstance(typeof(Triangulation));
        var triangles = delaunay.triangulate(vertices);

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = vertices;
        Debug.Log(Mathf.Min(triangles) + " " + Mathf.Max(triangles));
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    
    void Start () {
        makeMap("40.893774,-73.923017");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
