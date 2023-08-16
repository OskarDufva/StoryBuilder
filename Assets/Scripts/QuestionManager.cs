using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class QuestionData
{
    public List<Question> questions;
}

[System.Serializable]
public class Question
{
    public string question;
}

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionTextUI;
    public TMP_Text feedbackText;

    public List<Question> questions;
    private List<int> usedQuestionIndices = new List<int>();
    private int currentQuestionIndex = 0;
    private int round = 0;

    private enum GameState
    {
        Drawing,
        Answering
    }
    private GameState currentState;

    private void Start()
    {
        LoadQuestions();
        StartDrawingRound();
    }

    private void LoadQuestions()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Questions");
        QuestionData data = JsonUtility.FromJson<QuestionData>(jsonFile.text);
        questions = data.questions;
    }

    private void StartDrawingRound()
    {
        currentState = GameState.Drawing;
        DisplayQuestion();
        feedbackText.text = "Start drawing!";
    }

    public void SubmitDrawing()
    {
        if (currentState == GameState.Drawing)
        {
            if (round < 2)
            {
                usedQuestionIndices.Add(currentQuestionIndex);
                round++;
                StartDrawingRound();
            }
            else
            {
                currentState = GameState.Answering;
                feedbackText.text = "Time's up! Submit your answer.";
            }
        }
        else if (currentState == GameState.Answering)
        {
            feedbackText.text = "Answer submitted! Round complete.";
            NextQuestion();
            round = 0;
            usedQuestionIndices.Clear();
            StartDrawingRound();
        }
    }

    private void DisplayQuestion()
    {
        if (currentState == GameState.Drawing)
        {
            // Find a new question index that hasn't been used in this round
            do
            {
                currentQuestionIndex = Random.Range(0, questions.Count);
            }
            while (usedQuestionIndices.Contains(currentQuestionIndex));
            
            questionTextUI.text = questions[currentQuestionIndex].question;
        }
        else
        {
            questionTextUI.text = "Guess the drawing!";
        }
    }

    private void NextQuestion()
    {
        // No need to increment the question index here, as it will be done in DisplayQuestion()
    }
}
