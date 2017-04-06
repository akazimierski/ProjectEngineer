using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class KMLParser : ScriptableObject {

    public ArrayList points = new ArrayList();
    
    private void Start()
    {
        //getXmlData();
        //Debug.Log(points.Count);
    }

    public void getXmlData(Vector2[] corners)
    {
        string depthString = "";
        string coordinatesString = "";
        Vector3 pointCoord;
        var doc = new XmlDocument();
        XmlTextReader reader = new XmlTextReader(UnityEngine.Application.dataPath + "/KMLData/depareusa.kml");
        reader.Read();
        doc.Load(reader);

        XmlNode folder = doc.ChildNodes[1].ChildNodes[0].ChildNodes[1];
        for (int i = 1; i < folder.ChildNodes.Count; i++)
        {
            XmlNode placemark = folder.ChildNodes[i];
            XmlNode schemaData = placemark.ChildNodes[1].FirstChild;
            XmlNode polygon = placemark.ChildNodes[2];

            depthString = schemaData.ChildNodes[9].InnerText;
            coordinatesString = polygon.FirstChild.FirstChild.FirstChild.InnerText;

            string[] coordinates = coordinatesString.Split(' ');
            foreach (string item in coordinates)
            {
                string[] latLong = item.Split(',');
                pointCoord = new Vector3(float.Parse(latLong[1]), float.Parse(latLong[0]), float.Parse(depthString));
                if (pointCoord.z < 0 || pointCoord.z > 6f) continue;
                if (pointCoord.x >= corners[0].x && pointCoord.y >= corners[0].y && 
                    pointCoord.x <= corners[1].x && pointCoord.y <= corners[1].y)
                {
                    points.Add(normFromMinus1ToPlus1(pointCoord, corners[0], corners[1]));
                }
                
            }
        }
        reader.Close();
        Debug.Log(points.Count);
    }
    
    Vector3 normFromMinus1ToPlus1(Vector3 x, Vector2 minX, Vector2 maxX)
    {
        return new Vector3(-(10 * ( (x.y - minX.y) / (maxX.y - minX.y) ) - 5),
                            -x.z,
                            -(10 * ((x.x - minX.x) / (maxX.x - minX.x)) - 5));
    }

    public Vector3[] getVertices()
    {
        Vector3[] vertices = new Vector3[points.Count];
        for (int i = 0; i < points.Count; i++)
        {
            vertices[i] = (Vector3)points[i];
        }
        return vertices;
    }
}
