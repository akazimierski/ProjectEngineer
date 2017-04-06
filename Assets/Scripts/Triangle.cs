using UnityEngine;
using System.Collections;

public class Triangle : object
{

    public IdxVertex p1;
    public IdxVertex p2;
    public IdxVertex p3;

    public Edge e1;
    public Edge e2;
    public Edge e3;

    public Triangle(IdxVertex p1, IdxVertex p2, IdxVertex p3)
    {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
        makeFacingUp();
        e1 = new Edge(p1, p2);
        e2 = new Edge(p2, p3);
        e3 = new Edge(p3, p1);
    }

    public bool containsVertex(Vector3 v)
    {
	    return p1.vertex == v || p2.vertex == v || p3.vertex == v; 
    }

    void makeFacingUp()
    {
        var surfaceNormal = Vector3.Cross(p2.vertex - p1.vertex, p3.vertex - p1.vertex);
        if (surfaceNormal.y < 0f)
        {
            var tmp = p2;
            p2 = p3;
            p3 = tmp;
        }
    }

    public bool circumCircleContains(Vector3 v)
    {
	    float ab = (p1.vertex.x * p1.vertex.x) + (p1.vertex.z * p1.vertex.z);
        float cd = (p2.vertex.x * p2.vertex.x) + (p2.vertex.z * p2.vertex.z);
        float ef = (p3.vertex.x * p3.vertex.x) + (p3.vertex.z * p3.vertex.z);

        float circum_x = (ab * (p3.vertex.z - p2.vertex.z) + cd * (p1.vertex.z - p3.vertex.z) + ef * (p2.vertex.z - p1.vertex.z)) / (p1.vertex.x * (p3.vertex.z - p2.vertex.z) + p2.vertex.x * (p1.vertex.z - p3.vertex.z) + p3.vertex.x * (p2.vertex.z - p1.vertex.z)) / 2.0f;
        float circum_y = (ab * (p3.vertex.x - p2.vertex.x) + cd * (p1.vertex.x - p3.vertex.x) + ef * (p2.vertex.x - p1.vertex.x)) / (p1.vertex.z * (p3.vertex.x - p2.vertex.x) + p2.vertex.z * (p1.vertex.x - p3.vertex.x) + p3.vertex.z * (p2.vertex.x - p1.vertex.x)) / 2.0f;
        float circum_radius = Mathf.Sqrt(((p1.vertex.x - circum_x) * (p1.vertex.x - circum_x)) + ((p1.vertex.z - circum_y) * (p1.vertex.z - circum_y)));
        //if (circum_radius < 100.0f) return false;

        float dist = Mathf.Sqrt(((v.x - circum_x) * (v.x - circum_x)) + ((v.z - circum_y) * (v.z - circum_y)));
	    return dist <= circum_radius;
    }

    public override bool Equals(object obj)
    {
        // If parameter is null return false.
        if (obj == null)
        {
            return false;
        }

        // If parameter cannot be cast to Point return false.
        Triangle t2 = obj as Triangle;
        if ((object)t2 == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (p1.vertex == t2.p1.vertex || p1.vertex == t2.p2.vertex || p1.vertex == t2.p3.vertex) &&
                (p2.vertex == t2.p1.vertex || p2.vertex == t2.p2.vertex || p2.vertex == t2.p3.vertex) &&
                (p3.vertex == t2.p1.vertex || p3.vertex == t2.p2.vertex || p3.vertex == t2.p3.vertex);
    }

    public bool Equals(Triangle t2)
    {
        // If parameter is null return false:
        if ((object)t2 == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (p1.vertex == t2.p1.vertex || p1.vertex == t2.p2.vertex || p1.vertex == t2.p3.vertex) &&
                (p2.vertex == t2.p1.vertex || p2.vertex == t2.p2.vertex || p2.vertex == t2.p3.vertex) &&
                (p3.vertex == t2.p1.vertex || p3.vertex == t2.p2.vertex || p3.vertex == t2.p3.vertex);
    }

    public override int GetHashCode()
    {
        return p1.idx * p2.idx * p3.idx;
    }

    public static bool operator ==(Triangle t1, Triangle t2)
    {
	    return	(t1.p1.vertex == t2.p1.vertex || t1.p1.vertex == t2.p2.vertex|| t1.p1.vertex == t2.p3.vertex) &&
			    (t1.p2.vertex == t2.p1.vertex || t1.p2.vertex == t2.p2.vertex|| t1.p2.vertex == t2.p3.vertex) && 
			    (t1.p3.vertex == t2.p1.vertex || t1.p3.vertex == t2.p2.vertex|| t1.p3.vertex == t2.p3.vertex);
    }

    public static bool operator !=(Triangle t1, Triangle t2)
    {
        return !(t1 == t2);
    }
}
