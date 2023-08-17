using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineGenerator : MonoBehaviour
{
    public List<Line> linePrefabs;
    public int linePrefabIndex = 0;

    public GameObject eraserButton; // Reference to the eraser toggle button
    public Camera Cam;

    Line activeLine;
    bool eraserMode = false; // Flag to indicate eraser mode

    List<Line> lines = new List<Line>();

    public float zDistance = 10f;

        public void SetLine(int index)
    {
        linePrefabIndex = index;
    }

    void Update()
    {

        if (eraserMode && Input.GetMouseButton(0))
        {
            // Perform raycast to detect lines for erasing
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Line lineToRemove = hit.collider.GetComponent<Line>();
                if (lineToRemove != null)
                {
                    Destroy(lineToRemove.gameObject); // Erase the line
                }
            }
        }
        else if (!eraserMode)
        {
        

            if (Input.GetMouseButtonDown(0))
            {
                activeLine = Instantiate(linePrefabs[linePrefabIndex]);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosA = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance);
                Vector3 mousePos = Cam.ScreenToWorldPoint(mousePosA);
                activeLine.UpdateLine(mousePos);
            }

            if (Input.GetMouseButtonUp(0))
            {
                activeLine = null;
            }
        }
    }

    // Call this function when the eraser button is clicked/toggled
    public void ToggleEraserMode()
    {
        eraserMode = !eraserMode;
    }
}


