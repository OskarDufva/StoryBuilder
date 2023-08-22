using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public static LineManager Instance { get; private set; }

    private List<Line> lines = new List<Line>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddLine(Line line)
    {
        lines.Add(line);
    }

    public List<Line> GetLines()
    {
        return lines;
    }
}
