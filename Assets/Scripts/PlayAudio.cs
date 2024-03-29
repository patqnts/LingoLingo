using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip consVowsClip;
   public void PlayAudioMethod(AudioClip clips)
    {
        audioSource.clip = clips;
        audioSource.Play();
    }

    public void PlayConsVows()
    {
        audioSource.clip = consVowsClip;
        audioSource.Play();
    }

    public void CloseConsVow()
    {
        PixelCrushers.DialogueSystem.Sequencer.Message("CloseGame");
        Destroy(transform.parent.gameObject);
    }
}
