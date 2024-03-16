using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SentenceFormGame : MonoBehaviour
{
    public static SentenceFormGame instance;
    public GameObject levelCompleteUI;
    public Transform gameObjectcontainer;
    public SentenceFormScriptableObject scriptableObject;
    public Transform sentenceContainer;
    public Transform wordsContainer;
    private List<SentenceLine> sentenceLine;
    public TextMeshProUGUI timer;
    public int currentScore;
    public int maxScore;
    public float gameDuration;
    public int module;
    public int challengeIndex;
    private void Start()
    {
        GameTimerScript.instance.FinishGameEvent += FinalizeForm;
        sentenceLine = new List<SentenceLine>();
        instance = this;

        GameTimerScript.instance.StartTimer(gameDuration);
        GameTimerScript.instance.timerText = timer;
        LoadChallenge();
    }

    public void FinalizeForm()
    {
        GameTimerScript.instance.StopTimer();
        GameObject result = Instantiate(levelCompleteUI, gameObjectcontainer);
        result.GetComponentInChildren<TextMeshProUGUI>().text = $"Your score {currentScore}/{maxScore}";
        PlayerDataHandler.instance.SetModuleValue(module, challengeIndex, currentScore);
        StartCoroutine(CloseThisGame());
    }

    public void LoadChallenge()
    {
        int sentenceIndex = 1;
        foreach(string word in scriptableObject.words)
        {
            GameObject sentence = Instantiate(scriptableObject.SentenceLine, sentenceContainer);
            SentenceLine line = sentence.GetComponent<SentenceLine>();

            sentence.name = sentenceIndex.ToString();

            line.SentenceId = sentenceIndex;
          

            GameObject wordObject = Instantiate(scriptableObject.Word, wordsContainer);
            wordObject.GetComponentInChildren<TextMeshProUGUI>().text = word;
            wordObject.GetComponent<DraggableWord>().Id = sentenceIndex;

            sentenceLine.Add(line);
            sentenceIndex++;
        }
    }

    public void AnswerHandler()
    {
        maxScore = sentenceLine.Count;
        foreach (SentenceLine obj in sentenceLine)
        {
            if (obj.Id == obj.SentenceId)
            {
                currentScore++;
            }
        }

        FinalizeForm(); // Finalize the quiz if all questions have been answered
        
    }

    private void OnDestroy()
    {
        GameTimerScript.instance.FinishGameEvent -= FinalizeForm;
    }

    private IEnumerator CloseThisGame()
    {
        yield return new WaitForSeconds(3f);
        PixelCrushers.DialogueSystem.Sequencer.Message("CloseGame");
        Destroy(gameObject);
    }
}
