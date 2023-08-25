using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSaver : MonoBehaviour
{
    
    public LineGenerator lineGenerator;

    public static Dictionary<string, List<LineData>> savedLines = new();

    private void Start() {
        savedLines.Clear();
    }

    public void SaveLines()
    {
        List<LineData> lineDataList = new();

        foreach (Line line in lineGenerator.SpawnedLines)
        {
            LineData lineData = new()
            {
                positions = line.GetPositions(),
                linePrefabIndex = line.linePrefabIndex
            };

            lineDataList.Add(lineData);
            Debug.Log("Saved line with " + lineData.positions.Count + " points");
        }

        savedLines["SavedLines"] = lineDataList;

    }

}
