using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSaver : MonoBehaviour
{
    
    public List<Line> lines;

    public void SaveLines()
    {
        List<LineData> lineDataList = new List<LineData>();

        foreach (Line line in lines)
        {
            LineData lineData = new LineData();
            lineData.positions = line.GetPositions();

            lineDataList.Add(lineData);
            Debug.Log("Saved line with " + lineData.positions.Count + " points");
        }

        string json = JsonUtility.ToJson(lineDataList);
        Debug.Log(json);
        PlayerPrefs.SetString("SavedLines", json);
        Debug.Log("Saved " + lineDataList.Count + " lines");
        PlayerPrefs.Save();
    }

}
