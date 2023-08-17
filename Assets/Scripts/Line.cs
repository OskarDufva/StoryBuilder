using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour
{   
    public LineRenderer lineRenderer;

    List<Vector3> points;

    public MeshCollider meshCollider;

    public float precision = 0.1f;

    public void UpdateLine(Vector3 position)
    {
        if (points == null)
        {
            points = new List<Vector3>();
            SetPoint(position);
            return;
        }

        if (Vector3.Distance(points.Last(), position) > precision)
        {
            SetPoint(position);
        } 

    }

    void SetPoint(Vector3 point)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);
        var newMesh = new Mesh();
        lineRenderer.BakeMesh(newMesh, true);
        meshCollider.sharedMesh = newMesh;
    }
}
