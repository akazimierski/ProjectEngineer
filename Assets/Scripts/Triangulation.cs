using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

public class Triangulation : ScriptableObject {

    int[] triangles;
    HashSet<Triangle> trianglesArray = new HashSet<Triangle>();

    public int[] triangulate(Vector3[] vertices)
    {
        float minX = vertices[0].x;
        float minY = vertices[0].z;
        float maxX = minX;
        float maxY = minY;

        foreach (Vector3 point in vertices)
        {
            if (point.x < minX) minX = point.x;
            if (point.z < minY) minY = point.z;
            if (point.x > maxX) maxX = point.x;
            if (point.z > maxY) maxY = point.z;
        }

        float dx = maxX - minX;
        float dy = maxY - minY;
        float deltaMax = Mathf.Max(dx, dy);
        float midx = (minX + maxX) / 2.0f;
        float midy = (minY + maxY) / 2.0f;

        IdxVertex p1 = new IdxVertex(new Vector3(midx - 20 * deltaMax, 0.0f,midy - deltaMax));
        IdxVertex p2 = new IdxVertex(new Vector3(midx, 0.0f, midy + 20 * deltaMax));
        IdxVertex p3 = new IdxVertex(new Vector3(midx + 20 * deltaMax, 0.0f, midy - deltaMax));

        trianglesArray.Add(new Triangle(p1, p2, p3)); // idx ??

        for (int i = 0; i < vertices.Length; i++)
        {
            IdxVertex p = new IdxVertex(vertices[i], i);

            HashSet<Triangle> badTrianglesArray = new HashSet<Triangle>();
            Dictionary<float, Edge> polygon = new Dictionary<float, Edge>();

            foreach (Triangle t in trianglesArray)
            {
                if (t.circumCircleContains(p.vertex))
                {
                    badTrianglesArray.Add(t);
                    float key1 = t.e1.getKey();
                    float key2 = t.e2.getKey();
                    float key3 = t.e3.getKey();
                    if (!polygon.ContainsKey(key1))
                        polygon.Add(key1, t.e1);
                    else
                        polygon.Remove(key1);

                    if (!polygon.ContainsKey(key2))
                        polygon.Add(key2, t.e2);
                    else
                        polygon.Remove(key2);

                    if (!polygon.ContainsKey(key3))
                        polygon.Add(key3, t.e3);
                    else
                        polygon.Remove(key3);
                }
            }
            
            foreach (Triangle bt in badTrianglesArray)
            {
                trianglesArray.Remove(bt);
            }

            HashSet<Triangle> tempTriangles = new HashSet<Triangle>();
            foreach (var e in polygon)
            {
                tempTriangles.Add(new Triangle(e.Value.p1, e.Value.p2, p));
            }
            tempTriangles = doFlipIfNeeded(tempTriangles);

            foreach (Triangle tmp in tempTriangles)
            {
                trianglesArray.Add(tmp);
            }

            badTrianglesArray.Clear();
            polygon.Clear();
        }

        HashSet<Triangle> badTriangles = new HashSet<Triangle>();
        foreach (Triangle t in trianglesArray)
        {
            if (t.p1.idx == -1 || t.p2.idx == -1 || t.p3.idx == -1)
            {
                badTriangles.Add(t);
            }
        }

        foreach (Triangle bt in badTriangles)
        {
            trianglesArray.Remove(bt);
        }

        int it = 0;
        triangles = new int[trianglesArray.Count * 3];
        foreach (Triangle t in trianglesArray)
        {
            triangles[it++] = t.p1.idx;
            triangles[it++] = t.p2.idx;
            triangles[it++] = t.p3.idx;
        }
        Debug.Log(triangles.Length);
        return triangles;
    }

    HashSet<Triangle> doFlipIfNeeded(HashSet<Triangle> triangles)
    {
        HashSet<Triangle> delaunayTriangles = new HashSet<Triangle>();
        foreach (Triangle t1 in triangles)
        {
            foreach (Triangle t2 in triangles)
            {
                if (t1 == t2) continue;
                Edge e;
                if (hasCommonEdge(t1, t2, out e))
                {
                    var tmpDT = getDelaunayTriangles(t1, t2, e);
                    foreach (var item in tmpDT)
                        if (!delaunayTriangles.Contains(item))
                            delaunayTriangles.Add(item);
                }
                    
            }
        }
        if (delaunayTriangles.Count == 0) return triangles;
        return delaunayTriangles;
    }

    private Triangle[] getDelaunayTriangles(Triangle t1, Triangle t2, Edge e)
    {
        HashSet<Triangle> result = new HashSet<Triangle>();
        IdxVertex origin1, origin2;
        if (t1.p1.idx != e.p1.idx && t1.p1.idx != e.p2.idx ) origin1 = t1.p1;
        else if (t1.p2.idx  != e.p1.idx && t1.p2.idx  != e.p2.idx ) origin1 = t1.p2 ;
        else origin1 = t1.p3 ;

        if (t2.p1.idx != e.p1.idx && t2.p1.idx != e.p2.idx ) origin2 = t2.p1;
        else if (t2.p2.idx  != e.p1.idx && t2.p2.idx  != e.p2.idx ) origin2 = t2.p2 ;
        else origin2 = t2.p3 ;

        float angle1 = Vector3.Angle(e.p1.vertex - origin1.vertex, e.p2.vertex - origin1.vertex);
        float angle2 = Vector3.Angle(e.p1.vertex - origin2.vertex, e.p2.vertex - origin2.vertex);

        if ((angle1 + angle2) >= 180f)
        {
            result.Add(new Triangle(origin1, origin2, e.p1));
            result.Add(new Triangle(origin1, origin2, e.p2));
            return result.ToArray();
        }
        result.Add(t1);
        result.Add(t2);
        return result.ToArray();
    }

    private bool hasCommonEdge(Triangle t1, Triangle t2, out Edge e)
    {
        
        if (t1.e1 == t2.e1 || t1.e1 == t2.e2 || t1.e1 == t2.e3)
        {
            e = t1.e1;
            return true;
        }
        if (t1.e2 == t2.e1 || t1.e2 == t2.e2 || t1.e2 == t2.e3)
        {
            e = t1.e2;
            return true;
        }
        if (t1.e3 == t2.e1 || t1.e3 == t2.e2 || t1.e3 == t2.e3)
        {
            e = t1.e3;
            return true;
        }
        e = null;
        return false;
    }
}
