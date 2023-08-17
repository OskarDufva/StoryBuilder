using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class QuestionManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string category;
        public string question;
    }

    [System.Serializable]
    private class QuestionDataWrapper
    {
        public Question[] questions;
    }

    public TextMeshProUGUI[] questionTexts;
    private Dictionary<string, List<Question>> questionDictionary;
    private bool hasChangedQuestion = false;
    public Button[] changeButtons;

    private void Start()
    {
        LoadQuestionsFromResources();
        for (int i = 0; i < questionTexts.Length; i++)
        {
            DisplayQuestion(i);
        }
    }

    private void LoadQuestionsFromResources()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("questions"); // Assuming your JSON file is named "questions.json"
        QuestionDataWrapper wrapper = JsonUtility.FromJson<QuestionDataWrapper>(jsonFile.text);
        Question[] allQuestions = wrapper.questions;
        questionDictionary = allQuestions.GroupBy(q => q.category).ToDictionary(group => group.Key, group => group.ToList());
    }

    private void DisplayQuestion(int index)
    {
        string category = questionTexts[index].name;
        if (questionDictionary.TryGetValue(category, out List<Question> categoryQuestions))
        {
            Question randomQuestion = categoryQuestions[Random.Range(0, categoryQuestions.Count)];
            questionTexts[index].text = randomQuestion.question;
        }
        else
        {
            Debug.LogError("Category not found: " + category);
        }
    }

    public void ChangeQuestion(int index)
    {
        Debug.Log("ChangeQuestion called with index: " + index);
        if (!hasChangedQuestion)
        {
            string category = questionTexts[index].name;
            if (questionDictionary.TryGetValue(category, out List<Question> categoryQuestions))
            {
                Question randomQuestion = categoryQuestions[Random.Range(0, categoryQuestions.Count)];
                questionTexts[index].text = randomQuestion.question;
                hasChangedQuestion = true;

                DisableChangeButtons();
            }
            else
            {
                Debug.LogError("Category not found: " + category);
            }
        }
    }
    public void DisableChangeButtons()
    {
        foreach (Button button in changeButtons)
        {
            button.interactable = false;
            button.gameObject.SetActive(false);
        }
    }
}
