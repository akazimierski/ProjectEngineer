using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class KMLParser : MonoBehaviour {

    ArrayList points = new ArrayList();
    
    private void Start()
    {
        getXmlData();
        Debug.Log(points.Count);
    }

    public void getXmlData()
    {
        string depthString = "";
        string coordinatesString = "";
        Vector3 pointCoord;
        var doc = new XmlDocument();
        XmlTextReader reader = new XmlTextReader(UnityEngine.Application.dataPath + "/KMLData/depare.kml");
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
                string[] longLat = item.Split(',');
                pointCoord = new Vector3(float.Parse(longLat[0]), float.Parse(longLat[1]), float.Parse(depthString));
                points.Add(pointCoord);
            }
        }
        reader.Close();
    }
    

}
