using System.Collections.Generic;
using UnityEngine;
using Question = QuestionManager.Question;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Custom/QuestionData")]
public class QuestionData : ScriptableObject
{
    public List<Question> selectedQuestions = new List<Question>();

    public void SaveAllQuestionsToQuestionData(List<Question> newSelectedQuestions)
    {
        // Update only the selected questions that changed
        for (int i = 0; i < newSelectedQuestions.Count; i++)
        {
            if (newSelectedQuestions[i] != null && i < selectedQuestions.Count)
            {
                selectedQuestions[i] = newSelectedQuestions[i];
            }
        }
    }
}
