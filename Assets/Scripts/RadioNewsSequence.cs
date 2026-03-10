using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioNewsSequence : MonoBehaviour
{
    [SerializeField] private AudioSource radioSource;
    [SerializeField] private AudioClip radioSound; 
    [SerializeField, Range(0f, 1f)] private float radioSoundVolume = 0.5f; 

    private bool isSequencePlaying = false;

    public DialogueSO dialogue; 

    private void OnEnable()
    {
        if (!isSequencePlaying)
            StartCoroutine(PlayNewsSequence());
    }
    
    private IEnumerator PlayNewsSequence()
    {
        isSequencePlaying = true;
        
        if (radioSource != null && radioSound != null)
        {
            radioSource.clip = radioSound;
            radioSource.volume = radioSoundVolume;
            radioSource.Play();
        }

        if (dialogue != null)
        {
            DialogueManager.Instance.StartDialogue(dialogue);
        }
        while ((radioSource != null && radioSource.isPlaying))
        {
            yield return null;
        }
        isSequencePlaying = false;
        
        gameObject.SetActive(false);
    }
}
