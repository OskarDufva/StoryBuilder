using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

[System.Serializable]
public class QuestionData
{
    public List<Question> questions;
}

[System.Serializable]
public class Question
{
    public string category;
    public string question;
}

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionTextUI, feedbackText;
    public List<Question> questions;
    private List<string> categories = new List<string> { "places", "things", "verbs" };
    private int currentCategoryIndex = 0, currentQuestionIndex = 0, drawingsRemaining = 3;
    private GameState currentState;

    private enum GameState { Drawing, Guessing }

    private void Start()
    {
        LoadQuestions();
        StartDrawingPhase();
    }

    private void LoadQuestions()
    {
        QuestionData data = JsonUtility.FromJson<QuestionData>(Resources.Load<TextAsset>("Questions").text);
        questions = data.questions;
    }

    private void StartDrawingPhase()
    {
        currentState = GameState.Drawing;
        feedbackText.text = "Start drawing!";
        ShuffleQuestions();
        DisplayQuestion();
    }

    public void SubmitAction()
    {
        if (currentState == GameState.Drawing) NextDrawing();
        else if (currentState == GameState.Guessing)
        {
            feedbackText.text = "Answer submitted!";
            StartDrawingPhase();
        }
    }

    private void NextDrawing()
    {
        drawingsRemaining--;
        if (drawingsRemaining <= 0) StartGuessingPhase();
        else
        {
            NextCategory();
            StartDrawingPhase();
        }
    }

    private void DisplayQuestion()
    {
        questionTextUI.text = currentState == GameState.Drawing && currentCategoryIndex < categories.Count && currentQuestionIndex < questions.Count
            ? questions[currentQuestionIndex].question
            : currentState == GameState.Drawing ? "All drawings completed!" : "Guess the drawing!";
    }

    private void StartGuessingPhase()
    {
        currentState = GameState.Guessing;
        feedbackText.text = "Guess the drawing!";
        DisplayQuestion();
    }

    private void NextCategory() { currentCategoryIndex++; currentQuestionIndex = 0; }

    private void ShuffleQuestions() { questions = questions.OrderBy(q => Random.value).ToList(); }
}
