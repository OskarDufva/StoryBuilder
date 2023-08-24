using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Question = QuestionManager.Question;
using UnityEngine.SceneManagement;

public class LoadQuestion : MonoBehaviour
{
    [SerializeField]
    private QuestionData questionData;
    void Start()
    {
        string serializedData = PlayerPrefs.GetString("SelectedQuestions");
        QuestionData loadedData = JsonUtility.FromJson<QuestionData>(serializedData);
        List<Question> loadedQuestions = loadedData.selectedQuestions;
    }
}
