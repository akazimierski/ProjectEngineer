using UnityEngine;
using System.Collections;

// 53.0817322,14.3644296
// 40.893774, -73.923017
// 42.385367, -71.035441
// float latiude = 53.4281462f;
// float longitude = 14.5646163f

public class StaticMapDownload {

    string startUrl = "https://maps.googleapis.com/maps/api/staticmap";
    float latiude = 40.893774f;
    float longitude = -73.923017f;
    string center = "?center=";
    string zoom = "&zoom=" + "14";
    string size = "&size=" + "512x512";
    string format = "&format=" + "png32";
    string style = "&style=" + "element:labels|visibility:off" + "&style=" + "feature:water|color:0x000000";
    string key = "&key=AIzaSyBWLfFwdWQUpZn0gJ6ljq67n8Y2A2x6OzE";

    public Texture2D downloadImage(string latLong = null, int _zoom = 14)
    {
        if (latLong != null)
        {
            string[] latLongArray = latLong.Split(',');
            latiude = float.Parse(latLongArray[0]);
            longitude = float.Parse(latLongArray[1]);
            zoom = "&zoom=" + _zoom.ToString();
        }
        
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
        return mapTex;
    }

    // Use this for initialization
    void Start () {
        
    }
    
    /*void OnDrawGizmosSelected()
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
    }*/
    
}
