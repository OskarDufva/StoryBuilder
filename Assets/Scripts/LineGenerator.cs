using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;

    Line activeLine;

    public Camera camera;

    public float zDistance = 10f;
    public float zOffset = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newLine = Instantiate(linePrefab);
            activeLine = newLine.GetComponent<Line>();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosA = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, zDistance);
            Vector3 mousePos = camera.ScreenToWorldPoint(mousePosA);
            activeLine.UpdateLine(mousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }
    }
}
