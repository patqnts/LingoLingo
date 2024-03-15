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

    public TextMeshProUGUI currentQuestion;
    public Transform AnswerParent;
    public int currentScore;
    public int maxScore;
    private int currentQuestionIndex = 0;


    private void Start()
    {
        instance = this;
        //GameTimerScript.instance.FinishGameEvent += FinalizeQuiz;
        maxScore = questionScriptableObjects.Length;
        LoadQuestion();
    }
    private void OnDestroy()
    {
        GameTimerScript.instance.FinishGameEvent -= FinalizeQuiz;
    }

    public void LoadQuestion()
    {
        if(currentQuestionIndex <= questionScriptableObjects.Length)
        {
            currentQuestion.text = questionScriptableObjects[currentQuestionIndex].question;

            int answersCount = 0;
            foreach (string answer in questionScriptableObjects[currentQuestionIndex].answers)
            {
                GameObject questionItem = Instantiate(questionScriptableObjects[answersCount].answerButtonPrefab, AnswerParent);
                questionItem.GetComponentInChildren<TextMeshProUGUI>().text = answer;

                questionItem.GetComponentInChildren<AnswerButton>().Id = currentQuestionIndex;

                questionItem.GetComponent<Button>().onClick.AddListener(() => AnswerHandler(questionItem.GetComponentInChildren<AnswerButton>().Id));
                //answersCount++;
                currentQuestionIndex++;
            }
        }      
    }

    public void AnswerHandler(int Id)
    {
        if(Id == 0)
        {
            currentScore++;
        }

        if(currentQuestionIndex <= questionScriptableObjects.Length)
        {
            LoadQuestion();
        }
        else
        {
            FinalizeQuiz();
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
        Debug.Log($"Your score: {currentScore}/{maxScore}");
        StartCoroutine(CloseThisGame());
    }
}
