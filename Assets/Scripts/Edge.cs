using UnityEngine;
using System;
using System.Collections.Generic;

public class Edge : object
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

    public float getKey()
    {
        return p1.vertex.magnitude * p2.vertex.magnitude;
    }
    /*
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
        return (p1.idx == e2.p1.idx && p2.idx == e2.p2.idx) || 
            (p1.idx == e2.p2.idx && p2.idx == e2.p1.idx);
    }
    */
    /*
    public bool Equals(Edge e1, Edge e2)
    {
        // If parameter is null return false:
        if ((object)e2 == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (e1.p1.idx == e2.p1.idx && e1.p2.idx == e2.p2.idx) ||
            (e1.p1.idx == e2.p2.idx && e1.p2.idx == e2.p1.idx);
    }
    */
    /*
    bool IEqualityComparer<Edge>.Equals(Edge e1, Edge e2)
    {
        return (e1.p1.idx == e2.p1.idx && e1.p2.idx == e2.p2.idx) ||
            (e1.p1.idx == e2.p2.idx && e1.p2.idx == e2.p1.idx);
    }

    int IEqualityComparer<Edge>.GetHashCode(Edge obj)
    {
        return obj.p1.idx ^ obj.p2.idx;
    }
    */
    /*
    public override int GetHashCode()
    {
       return p1.idx * p2.idx;
    }
    
    public static bool operator ==(Edge e1, Edge e2)
    {
       return (e1.p1.idx == e2.p1.idx && e1.p2.idx == e2.p2.idx) || 
           (e1.p1.idx == e2.p2.idx && e1.p2.idx == e2.p1.idx);
    }

    public static bool operator !=(Edge e1, Edge e2)
    {
       return !(e1 == e2);
    }
    */

}
