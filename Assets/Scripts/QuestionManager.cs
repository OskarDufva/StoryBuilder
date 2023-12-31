using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

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

    [SerializeField]
    private QuestionData questionData;

    private List<string> displayedQuestions = new List<string>();

    public TextMeshProUGUI[] questionTexts;
    private Dictionary<string, List<Question>> questionDictionary;

    private bool hasChangedQuestion = false;
    public Button[] changeButtons;

    private void Start()
    {
        if (questionData == null){
            Debug.LogError("QuestionData is null!");
            return;
        }
        questionData.selectedQuestions.Clear();
        LoadQuestionsFromResources();
        for (int i = 0; i < questionTexts.Length; i++)
        {
            DisplayQuestion(i);
        }
    }

    private void LoadQuestionsFromResources()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("questions"); 
        Debug.Log("Loaded JSON: " + jsonFile.text);
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

            if(!displayedQuestions.Contains(randomQuestion.question))
            {
                questionData.selectedQuestions.Add(randomQuestion);
                displayedQuestions.Add(randomQuestion.question);
            }
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
                string currentQuestion = questionTexts[index].text;

                Question randomQuestion = null;
                do
                {
                    randomQuestion = categoryQuestions[Random.Range(0, categoryQuestions.Count)];
                } while (randomQuestion.question == currentQuestion);

                questionTexts[index].text = randomQuestion.question;
                hasChangedQuestion = true;

                DisableChangeButtons();

                Question selectedQuestion = new Question();
                selectedQuestion.category = randomQuestion.category;
                selectedQuestion.question = randomQuestion.question;

                questionData.SaveSelectedQuestions(selectedQuestion);
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
