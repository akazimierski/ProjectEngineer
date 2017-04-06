using UnityEngine;
using System.Collections;

public class IdxVertex {

    public Vector3 vertex;
    public int idx;

    public IdxVertex(Vector3 v)
    {
        vertex = v;
        idx = -1;
    }

    public IdxVertex(Vector3 v, int i)
    {
        vertex = v;
        idx = i;
    }
}
