using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineLoader : MonoBehaviour
{
    public Transform lineParent;

    public LineGenerator lineGenerator;

    public void LoadLines()
    {
        ClearExistingLines();

        List<LineData> lineDataList = LineSaver.savedLines["SavedLines"];
        Debug.Log("Loaded " + lineDataList.Count + " lines");

        foreach (LineData lineData in lineDataList)
        {
            Line prefab = lineGenerator.linePrefabs[lineData.linePrefabIndex];
            Line newLine = Instantiate(prefab, lineParent);
            newLine.SetPositions(lineData.positions);
            Debug.Log("Loaded line with " + lineData.positions.Count + " points");
        }
    }

    public void ClearExistingLines()
    {
        foreach (Transform child in lineParent)
        {
            Destroy(child.gameObject);
        }
    }
}
