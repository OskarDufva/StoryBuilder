using UnityEngine;

public class QuestionDataHolder : MonoBehaviour
{
    public QuestionData questionData;

    private void Awake()
    {
        // Load the QuestionData Scriptable Object here (using Resources.Load or another method)
        questionData = Resources.Load<QuestionData>("QuestionData");
    }
}
