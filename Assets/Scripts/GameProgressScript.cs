using Michsky.MUIP;
using UnityEngine;
using UnityEngine.Events;

public class GameProgressScript : MonoBehaviour
{
    [SerializeField] private ProgressBar[] ModuleProgress;
    public int[] percentage;

    private void Start()
    {
        PlayerDataProgress();
        
    }


    private void HandleZeroPercent()
    {
        foreach (ProgressBar progressBar in ModuleProgress)
        {
            int index = System.Array.IndexOf(ModuleProgress, progressBar);
            if (index >= 0 && index < percentage.Length && percentage[index] == 0)
            {
                progressBar.invert = true;
            }
            else
            {
                progressBar.invert = false;
            }
        }
    }
    public void PlayerDataProgress()
    {
        int currentIndex = 0;
        foreach (PlayerDataHandler.Module module in PlayerDataHandler.instance.modules)
        {
            int sum = module.challenges[0] + module.challenges[1] + module.challenges[2];
            int maxScore = PlayerDataHandler.instance.ChallengeMaxScore[currentIndex];
            if(sum <= 1)
            {
                percentage[currentIndex] = 0;
            }
            else
            {
                percentage[currentIndex] = Mathf.RoundToInt((float)sum / maxScore * 100);
            }           
            currentIndex++;
        }

        InitializeProgressBars();
    }
    private void InitializeProgressBars()
    {
        foreach (ProgressBar progressBar in ModuleProgress)
        {
            progressBar.onValueChanged.AddListener(delegate { OnValueChanged(progressBar); });
        }

        HandleZeroPercent();
    }

    private void OnValueChanged(ProgressBar progressBar)
    {
        int index = System.Array.IndexOf(ModuleProgress, progressBar);
        if (index >= 0 && index < percentage.Length)
        {
            float value = progressBar.currentPercent;
            if (value >= percentage[index])
            {
                progressBar.isOn = false;
                progressBar.currentPercent = percentage[index];
                progressBar.onValueChanged.RemoveAllListeners();
            }
            Debug.Log("Current value: " + value.ToString());
        }
    }

    private void OnEnable()
    {
        foreach (ProgressBar progressBar in ModuleProgress)
        {
            progressBar.isOn = true;
            progressBar.currentPercent = 0f; // Reset progress to 0 when script is enabled

        }
        PlayerDataProgress();
    }

    private void OnDisable()
    {
        foreach (ProgressBar progressBar in ModuleProgress)
        {
            progressBar.isOn = false;
            progressBar.currentPercent = 0f; // Reset progress to 0 when script is disabled
        }
    }
}
