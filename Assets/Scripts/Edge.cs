using UnityEngine;
using System.Collections;

public class Edge : Object
{

    public IdxVertex p1;
    public IdxVertex p2;

    public Edge(IdxVertex p1, IdxVertex p2)
    {
        this.p1 = p1;
        this.p2 = p2;
    }

    public Edge(Edge e)
    {
        p1 = e.p1;
        p2 = e.p2;
    }

    public override bool Equals(object obj)
    {
        // If parameter is null return false.
        if (obj == null)
        {
            return false;
        }

        // If parameter cannot be cast to Point return false.
        Edge e2 = obj as Edge;
        if ((object)e2 == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (p1.vertex == e2.p1.vertex && p2.vertex == e2.p2.vertex) || 
            (p1.vertex == e2.p2.vertex && p2.vertex == e2.p1.vertex);
    }

    public bool Equals(Edge e2)
    {
        // If parameter is null return false:
        if ((object)e2 == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (p1.vertex == e2.p1.vertex && p2.vertex == e2.p2.vertex) ||
            (p1.vertex == e2.p2.vertex && p2.vertex == e2.p1.vertex);
    }

    public override int GetHashCode()
    {
        return p1.idx * p2.idx;
    }

    public static bool operator ==(Edge e1, Edge e2)
    {
        return (e1.p1.vertex == e2.p1.vertex && e1.p2.vertex == e2.p2.vertex) || 
            (e1.p1.vertex == e2.p2.vertex && e1.p2.vertex == e2.p1.vertex);
    }

    public static bool operator !=(Edge e1, Edge e2)
    {
        return !(e1 == e2);
    }


}
