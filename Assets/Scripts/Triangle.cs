using UnityEngine;
using System.Collections;

public class Triangle {

    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;

    public Edge e1;
    public Edge e2;
    public Edge e3;

    public Vector3 p1Idx;
    public Vector3 p2Idx;
    public Vector3 p3Idx;

    public Triangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
        e1 = new Edge(p1, p2);
        e2 = new Edge(p2, p3);
        e3 = new Edge(p3, p1);
    }

    public bool containsVertex(Vector3 v)
    {
	    return p1 == v || p2 == v || p3 == v; 
    }

    public bool circumCircleContains(Vector3 v)
    {
	    float ab = (p1.x * p1.x) + (p1.y * p1.y);
        float cd = (p2.x * p2.x) + (p2.y * p2.y);
        float ef = (p3.x * p3.x) + (p3.y * p3.y);

        float circum_x = (ab * (p3.y - p2.y) + cd * (p1.y - p3.y) + ef * (p2.y - p1.y)) / (p1.x * (p3.y - p2.y) + p2.x * (p1.y - p3.y) + p3.x * (p2.y - p1.y)) / 2.0f;
        float circum_y = (ab * (p3.x - p2.x) + cd * (p1.x - p3.x) + ef * (p2.x - p1.x)) / (p1.y * (p3.x - p2.x) + p2.y * (p1.x - p3.x) + p3.y * (p2.x - p1.x)) / 2.0f;
        float circum_radius = Mathf.Sqrt(((p1.x - circum_x) * (p1.x - circum_x)) + ((p1.y - circum_y) * (p1.y - circum_y)));

        float dist = Mathf.Sqrt(((v.x - circum_x) * (v.x - circum_x)) + ((v.y - circum_y) * (v.y - circum_y)));
	    return dist <= circum_radius;
    }
}
