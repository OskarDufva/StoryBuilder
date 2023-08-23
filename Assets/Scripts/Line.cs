using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Drawing;

public class Line : MonoBehaviour
{   
    public LineRenderer lineRenderer;

    List<Vector3> points;

    public MeshCollider meshCollider;   

    public float precision = 0.1f;

    public List<Vector3> positions = new List<Vector3>();

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

        positions.Add(position);

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

    public List<Vector3> GetPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        foreach (var point in points)
        {
            positions.Add(point);
        }

        return positions;
    }

    public void SetPositions(List<Vector3> positions)
    {
        points.Clear();
        this.lineRenderer.positionCount = 0;

        foreach (Vector3 position in positions)
        {
            UpdateLine(position);
        }
    }
}
