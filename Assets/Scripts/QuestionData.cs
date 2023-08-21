using System.Collections.Generic;
using UnityEngine;
using Question = QuestionManager.Question;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Custom/QuestionData")]
public class QuestionData : ScriptableObject
{
    public List<Question> selectedQuestions = new List<Question>();

    public void SaveSelectedQuestions(Question question)
    {
        // Check if the question already exists in the list
        int existingIndex = selectedQuestions.FindIndex(q => q.category == question.category);

        if (existingIndex >= 0)
        {
            // Replace the existing question with the new one
            selectedQuestions[existingIndex] = question;
        }
        else
        {
            // Add the question to the list if it doesn't exist
            selectedQuestions.Add(question);
        }

        // Convert the list of selected questions to a serialized format (JSON, for example)
        string serializedData = JsonUtility.ToJson(this);

        // Save the serialized data to player preferences
        PlayerPrefs.SetString("SelectedQuestions", serializedData);
        PlayerPrefs.Save();
    }
}
