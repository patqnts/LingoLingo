using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulesHandler : MonoBehaviour
{
    public static ModulesHandler Instance;
    public GameObject currentGame;
    private void Start()
    {
        Instance = this;
    }


    [Header("Word Match")]
    public GameObject wordMatch;
    [Header("Quiz")]
    public GameObject quizMultipleChoice;
    [Header("Sentence Form Game")]
    public GameObject sentenceForm;
    [Header("Quiz3")]
    public GameObject quiz3;

    public void WordMatch()
    {
        currentGame = Instantiate(wordMatch, this.transform);
    }

    public void Quiz1()
    {
        currentGame = Instantiate(quizMultipleChoice, this.transform);
    }

    public void SentenceFormGame()
    {
        currentGame = Instantiate(sentenceForm, this.transform);
    }

}
