using UnityEngine;
using System.Collections;

public class Triangulation : MonoBehaviour {

    public Vector3[] triangles;
    ArrayList trianglesArray = new ArrayList();

    public void Triangulate(Vector3[] vertices)
    {
        float minX = vertices[0].x;
        float minY = vertices[0].y;
        float maxX = minX;
        float maxY = minY;

        foreach (Vector3 point in vertices)
        {
            if (point.x < minX) minX = point.x;
            if (point.y < minY) minY = point.y;
            if (point.x > maxX) maxX = point.x;
            if (point.y > maxY) maxY = point.y;
        }

        float dx = maxX - minX;
        float dy = maxY - minY;
        float deltaMax = Mathf.Max(dx, dy);
        float midx = (minX + maxX) / 2.0f;
        float midy = (minY + maxY) / 2.0f;

        Vector3 p1 = new Vector3(midx - 20 * deltaMax, midy - deltaMax);
        Vector3 p2 = new Vector3(midx, midy +20 * deltaMax);
        Vector3 p3 = new Vector3(midx + 20 * deltaMax, midy - deltaMax);

        trianglesArray.Add(new Triangle(p1, p2, p3)); // idx ??


        for (int i = 0; i < vertices.Length; i++)
        {
            ArrayList badTrianglesArray = new ArrayList();
            ArrayList polygon = new ArrayList();

            foreach (Triangle t in trianglesArray)
            {
                if (t.circumCircleContains(vertices[i]))
                {
                    badTrianglesArray.Add(t);
                    polygon.Add(t.e1);
                    polygon.Add(t.e3);
                    polygon.Add(t.e2);
                }
            }

            foreach (Triangle bt in badTrianglesArray)
            {
                foreach (Triangle t in trianglesArray)
                {
                    if (bt.containsVertex(t.p1) && bt.containsVertex(t.p2) && bt.containsVertex(t.p3))
                    {
                        trianglesArray.Remove(t);
                    }
                }
            }

            ArrayList badEdges = new ArrayList();

            for (int e1 = 0; e1 < polygon.Count; e1++)
            {
                for (int e2 = 0; e2 < polygon.Count; e2++)
                {
                    if (e1 == e2) continue;
                    if (polygon[e1] == polygon[e2])
                    {
                        badEdges.Add(polygon[e1]);
                        badEdges.Add(polygon[e2]);
                    }
                }
            }

            foreach (Edge e in polygon)
            {
                foreach (Edge be in badEdges)
                {
                    if ((e.p1 == be.p1 && e.p2 == be.p2) || (e.p1 == be.p2 && e.p2 == be.p1))
                    {
                        polygon.Remove(e);
                    }
                }
            }

            foreach (Edge e in polygon)
            {
                trianglesArray.Add(new Triangle(e.p1, e.p2, vertices[i]));
            }
        }
    }
}
