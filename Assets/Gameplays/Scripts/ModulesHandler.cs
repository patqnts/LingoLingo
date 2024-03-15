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
    [Header("Quiz1")]
    public GameObject quiz1;
    [Header("Quiz2")]
    public GameObject quiz2;
    [Header("Quiz3")]
    public GameObject quiz3;

    public void StartGame()
    {
        currentGame = Instantiate(wordMatch, this.transform);
    }

}
