using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public static SoundHandler instance;
    public AudioSource audioSource;
    public AudioClip[] clip;

    // Enum to define different game states
    public enum GameState
    {
        Default,
        Conversation,
        Quiz
    }

    // Variable to store the current game state
    public GameState state;

    void Start()
    {
        instance = this;
    }

    // Method to set the game state
    public void SetGameState(GameState gameState)
    {
        state = gameState;
        PlayAudioForState();
    }

    public void SetGameStateDefault()
    {
        state = GameState.Default;
        PlayAudioForState();
    }

    public void SetGameStateConvo()
    {
        state = GameState.Conversation;
        PlayAudioForState();
    }

    public void SetGameStateQuiz()
    {
        state = GameState.Quiz;
        PlayAudioForState();
    }
    // Method to play audio based on the current game state
    private void PlayAudioForState()
    {
        // Ensure that the audio source and clips are set
        if (audioSource != null && clip != null)
        {
            // Check if the index for the current state is within the array bounds
            if ((int)state >= 0 && (int)state < clip.Length && clip[(int)state] != null)
            {
                audioSource.clip = clip[(int)state];
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Audio clip for the current state is not set!");
            }
        }
        else
        {
            Debug.LogWarning("Audio source or clip is not assigned!");
        }
    }
}
