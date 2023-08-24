using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Question = QuestionManager.Question;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadQuestion : MonoBehaviour
{
    [SerializeField]
    private QuestionData questionData;
    public TextMeshProUGUI[] savedQuestionTexts;
    void Start()
    {
        
        for (int i = 0; i < questionData.selectedQuestions.Count && i < savedQuestionTexts.Length; i++)
        {
            savedQuestionTexts[i].text = questionData.selectedQuestions[i].question;
        }
        
    }
}
