using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using TMPro;

public class LineGenerator : MonoBehaviour
{
    public List<Line> linePrefabs;
    public int linePrefabIndex = 0;

    public GameObject eraserButton; // Reference to the eraser toggle button
    public Camera Cam;

    public float timer = 240f;
    [SerializeField]TMP_Text timerText;

    Line activeLine;
    bool eraserMode = false; // Flag to indicate eraser mode

    private List<Line> lines = new List<Line>();

    public List<Line> SpawnedLines => lines;
    
    List<Line> temporaryLines = new List<Line>();

    public float zDistance = 10f;

    private bool playMode = false;

        public void SetLine(int index)
    {
        linePrefabIndex = index;
    }

    public void AddLine(Line line)
    {
        lines.Add(line);
    }

    void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            playMode = true;
        }

        if  (timer > 0)
        {
            timer -= Time.deltaTime;
            Debug.Log(Mathf.Round(timer));
            timerText.text = timer.ToString("F0");
        }

        if  (timer <= 0)
        {
            playMode = false;
            timer = 240f;
        }   


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
        else if (!eraserMode && playMode == true)
        {
        

            if (Input.GetMouseButtonDown(0))
            {
                activeLine = Instantiate(linePrefabs[linePrefabIndex]);
                activeLine.linePrefabIndex = linePrefabIndex;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosA = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance);
                Vector3 mousePos = Cam.ScreenToWorldPoint(mousePosA);
                activeLine.UpdateLine(mousePos);
            }

            if (Input.GetMouseButtonUp(0))

            {
            if (activeLine != null)
            {
                lines.Add(activeLine);
                temporaryLines.Add(activeLine); // Add line to temporary list
                LineManager.Instance.AddLine(activeLine);
                activeLine = null;
            }
            }

        }
    }

    // Call this function when the eraser button is clicked/toggled
    public void ToggleEraserMode()  
    {
        eraserMode = !eraserMode;
    }

    public void ClearTemporaryLines()
    {
        foreach (Line line in temporaryLines)
        {
            if (line != null)
            {
                Destroy(line.gameObject);       
            }
        }
        temporaryLines.Clear();
    }
}


