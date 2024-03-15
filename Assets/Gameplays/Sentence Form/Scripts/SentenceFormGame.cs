using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SentenceFormGame : MonoBehaviour
{
    public static SentenceFormGame instance;
    public SentenceFormScriptableObject scriptableObject;
    public Transform sentenceContainer;
    public Transform wordsContainer;
    private List<SentenceLine> sentenceLine;
    private void Start()
    {
        GameTimerScript.instance.FinishGameEvent += FinalizeForm;
        instance = this;
        LoadChallenge();
    }

    public void FinalizeForm()
    {

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

            sentenceIndex++;
        }
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
