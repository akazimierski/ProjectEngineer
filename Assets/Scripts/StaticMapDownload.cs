using UnityEngine;
using System.Collections;

// 53.0817322,14.3644296
// 40.893774, -73.923017
// float latiude = 53.4281462f;
// float longitude = 14.5646163f

public class StaticMapDownload : MonoBehaviour {

    string startUrl = "https://maps.googleapis.com/maps/api/staticmap";
    float latiude = 40.893774f;
    float longitude = -73.923017f;
    string center = "?center=";
    string zoom = "&zoom=" + "16";
    string size = "&size=" + "512x512";
    string format = "&format=" + "png32";
    string style = "&style=" + "element:labels|visibility:off" + "&style=" + "feature:water|color:0x000000";
    string key = "&key=AIzaSyBWLfFwdWQUpZn0gJ6ljq67n8Y2A2x6OzE";
    public GameObject plane;

    Vector3[] vertices = new Vector3[1];

    // Use this for initialization
    void Start () {
        
        string url = startUrl + center + latiude + "," + longitude + zoom + size + format + style + key;
        WWW www = new WWW(url);

        while (!www.isDone) ;
        Color[] pixels = www.texture.GetPixels();

        Texture2D mapTex = new Texture2D(www.texture.width, www.texture.height);

        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i] == Color.black)
            {
                pixels[i] = new Color(0, 0, 0, 0);
            }
        }
        mapTex.SetPixels(pixels);
        mapTex.Apply();
             
        var renderer = plane.GetComponent<Renderer>();
        renderer.material.mainTexture = mapTex;
        renderer.material.shader = Shader.Find("Unlit/Transparent");
        

        MercatorProjection mp = new MercatorProjection();
        Vector2[] corners = mp.getCorners(new Vector2(latiude, longitude), 16, 512, 512);
        Debug.Log(corners[0] + ", " + corners[1]);

        KMLParser parser = (KMLParser)ScriptableObject.CreateInstance(typeof(KMLParser));
        parser.getXmlData(corners);
        vertices = parser.getVertices();
        
        Triangulation delaunay = (Triangulation)ScriptableObject.CreateInstance(typeof(Triangulation));
        var triangles = delaunay.Triangulate(vertices);

        

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = vertices;
        Debug.Log(Mathf.Min(triangles) + " " + Mathf.Max(triangles));
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        /*
        var normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = Vector3.Reflect(normals[i]., Vector3.up);
        mesh.normals = normals;
        */
    }
    
    void OnDrawGizmosSelected()
    {
        if (vertices != null)
        {
            foreach (var item in vertices)
            {
                if (item.z == -5f)
                {
                    Gizmos.color = Color.black;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }
                Gizmos.DrawSphere(item, 0.3f);
            }
        }
    }
    
}
