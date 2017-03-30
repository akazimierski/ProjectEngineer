using UnityEngine;
using System.Collections;

public class Edge {

    public Vector3 p1;
    public Vector3 p2;

    public Edge(Vector3 p1, Vector3 p2)
    {
        this.p1 = p1;
        this.p2 = p2;
    }

    public Edge(Edge e)
    {
        p1 = e.p1;
        p2 = e.p2;
    }
}
