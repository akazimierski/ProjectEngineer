using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class test_Triangulation : MonoBehaviour {

    public static int flipsDone = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	public static void test_TriangulationMesh(HashSet<Triangle> triangles)
    {
        doFlipIfNeeded(triangles);
    }

    static HashSet<Triangle> doFlipIfNeeded(HashSet<Triangle> triangles)
    {
        flipsDone = 0;
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

    private static Triangle[] getDelaunayTriangles(Triangle t1, Triangle t2, Edge e)
    {
        HashSet<Triangle> result = new HashSet<Triangle>();
        IdxVertex origin1, origin2;
        if (t1.p1.idx != e.p1.idx && t1.p1.idx != e.p2.idx) origin1 = t1.p1;
        else if (t1.p2.idx != e.p1.idx && t1.p2.idx != e.p2.idx) origin1 = t1.p2;
        else origin1 = t1.p3;

        if (t2.p1.idx != e.p1.idx && t2.p1.idx != e.p2.idx) origin2 = t2.p1;
        else if (t2.p2.idx != e.p1.idx && t2.p2.idx != e.p2.idx) origin2 = t2.p2;
        else origin2 = t2.p3;

        float angle1 = Vector3.Angle(e.p1.vertex - origin1.vertex, e.p2.vertex - origin1.vertex);
        float angle2 = Vector3.Angle(e.p1.vertex - origin2.vertex, e.p2.vertex - origin2.vertex);

        if ((angle1 + angle2) >= 180f)
        {
            flipsDone++;
            result.Add(new Triangle(origin1, origin2, e.p1));
            result.Add(new Triangle(origin1, origin2, e.p2));
            return result.ToArray();
        }
        result.Add(t1);
        result.Add(t2);
        return result.ToArray();
    }

    private static bool hasCommonEdge(Triangle t1, Triangle t2, out Edge e)
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
