using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LineGenerator lineGenerator;

    public LineManager lineManager;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if  (this != instance)
                Destroy(gameObject);
        }
    }

    void Start()
    {
        
        List<Line> lines = LineManager.Instance.GetLines();

        foreach (Line line in lines)
        {
            Line newLine = Instantiate(lineGenerator.linePrefabs[lineGenerator.linePrefabIndex]);
            
            // Assuming 'positions' is a public field or property within the Line class
            foreach (Vector3 position in line.positions)
            {
                newLine.positions.Add(position);
            }

            lineGenerator.AddLine(newLine); // Add the new line to the LineGenerator's list
        }
    }

    public void ClearLinesAndReset()
    {
        lineGenerator.ClearTemporaryLines();
    }
}
