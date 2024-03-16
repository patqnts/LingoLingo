using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{

    public static QuizManager instance;
    public QuestionScriptableObject[] questionScriptableObjects;
    public TextMeshProUGUI timer;
    public Text currentQuestion;

    public Transform gameObjectcontainer;
    public GameObject levelCompleteUI;

    public Transform AnswerParent;
    public int currentScore;
    public int maxScore;
    public int currentQuestionIndex = 0;
    public float gameDuration;

    private void Start()
    {
        GameTimerScript.instance.FinishGameEvent += FinalizeQuiz;
        instance = this;      
        maxScore = questionScriptableObjects.Length;
        Debug.Log(questionScriptableObjects.Length);
        LoadQuestion(currentQuestionIndex);
        GameTimerScript.instance.StartTimer(gameDuration);
        GameTimerScript.instance.timerText = timer;
    }
    private void OnDestroy()
    {
        GameTimerScript.instance.FinishGameEvent -= FinalizeQuiz;
    }

    public void LoadQuestion(int Index)
    {

        foreach (Transform child in AnswerParent.transform)
        {
            Destroy(child.gameObject);
        }

        currentQuestion.text = questionScriptableObjects[Index].question;

        int answerId = 0;
        foreach (string answer in questionScriptableObjects[Index].answers)
        {
            GameObject questionItem = Instantiate(questionScriptableObjects[Index].answerButtonPrefab, AnswerParent);
            questionItem.GetComponentInChildren<TextMeshProUGUI>().text = answer;

            questionItem.GetComponentInChildren<AnswerButton>().Id = answerId;

            questionItem.GetComponent<Button>().onClick.AddListener(() => AnswerHandler(questionItem.GetComponentInChildren<AnswerButton>().Id));
            answerId++;
        }
        
    }

    public void AnswerHandler(int Id)
    {
        if (Id == 0)
        {
            currentScore++;
        }

        currentQuestionIndex++; // Increment the index here

        if (currentQuestionIndex < questionScriptableObjects.Length) // Check if index is within bounds
        {
            LoadQuestion(currentQuestionIndex); // Load the next question
        }
        else
        {
            FinalizeQuiz(); // Finalize the quiz if all questions have been answered
        }
    }

    private IEnumerator CloseThisGame()
    {
        yield return new WaitForSeconds(3f);
        PixelCrushers.DialogueSystem.Sequencer.Message("CloseGame");
        Destroy(gameObject);
    }

    public void FinalizeQuiz() 
    {
        GameTimerScript.instance.StopTimer();
        GameObject result = Instantiate(levelCompleteUI, gameObjectcontainer);
        result.GetComponentInChildren<TextMeshProUGUI>().text = $"Your score {currentScore}/{maxScore}";
        StartCoroutine(CloseThisGame());
    }
}
