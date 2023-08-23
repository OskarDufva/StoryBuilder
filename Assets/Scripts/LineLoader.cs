using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineLoader : MonoBehaviour
{
    public List<Line> linePrefabs;
    public Transform lineParent;

    public void LoadLines()
    {
        ClearExistingLines();

        string json = PlayerPrefs.GetString("SavedLines");
        List<LineData> lineDataList = JsonUtility.FromJson<List<LineData>>(json);
        Debug.Log("Loaded " + lineDataList.Count + " lines");

        foreach (LineData lineData in lineDataList)
        {
            Line prefab = linePrefabs[lineData.linePrefabIndex];
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
