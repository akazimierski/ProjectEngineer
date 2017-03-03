using UnityEngine;
using System.Collections;

public class StaticMapDownload : MonoBehaviour {

    string startUrl = "https://maps.googleapis.com/maps/api/staticmap";
    float latiude = 53.4281462f;
    float longitude = 14.5646163f;
    string center = "?center=";
    string zoom = "&zoom=" + "16";
    string size = "&size=" + "512x512";
    string format = "&format=" + "png32";
    string style = "&style=" + "element:labels|visibility:off" + "&style=" + "feature:water|color:0x000000";
    string key = "&key=AIzaSyBWLfFwdWQUpZn0gJ6ljq67n8Y2A2x6OzE";
    public GameObject plane;

    // Use this for initialization
    void Start () {
        /*
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
        */
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
