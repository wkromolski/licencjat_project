using System.Collections;
using UnityEngine;

public class RadioSounds : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource radioSource1;

    [Header("Radio Sound Clips")]
    [SerializeField] private AudioClip radioSound1; 

    [Header("Volume Settings")]
    [SerializeField, Range(0f, 1f)] private float radioSound1Volume = 0.5f; 

    private bool isSequencePlaying = false;

    public DialogueSO dialogue; 

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSequencePlaying)
        {
            Debug.Log("Trigger uruchomiony: " + gameObject.name);
            StartCoroutine(PlayRadioSequence());
        }
    }
    

    private IEnumerator PlayRadioSequence()
    {
        isSequencePlaying = true;
        
        if (radioSource1 != null && radioSound1 != null)
        {
            radioSource1.clip = radioSound1;
            radioSource1.volume = radioSound1Volume;
            radioSource1.Play();
        }

        if (dialogue != null)
        {
            DialogueManager.Instance.StartDialogue(dialogue);
        }
        while ((radioSource1 != null && radioSource1.isPlaying))
        {
            yield return null;
        }
        isSequencePlaying = false;
    }
}
