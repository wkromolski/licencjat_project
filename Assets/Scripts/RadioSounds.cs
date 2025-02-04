using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSounds : MonoBehaviour
{ 
    [Header("Audio Sources")]
    [SerializeField] private AudioSource radioSource1; // Źródło dla pierwszego dźwięku
    [SerializeField] private AudioSource radioSource2; // Źródło dla drugiego dźwięku

    [Header("Radio Sound Clips")]
    [SerializeField] private AudioClip radioSound1; // Pierwszy dźwięk
    [SerializeField] private AudioClip radioSound2; // Drugi dźwięk

    [Header("Volume Settings")]
    [SerializeField, Range(0f, 1f)] private float radioSound1Volume = 0.5f; // Głośność pierwszego dźwięku
    [SerializeField, Range(0f, 1f)] private float radioSound2Volume = 0.5f; // Głośność drugiego dźwięku

    // Flaga blokująca ponowne uruchomienie sekwencji, dopóki aktualna sekwencja się nie zakończy
    private bool isSequencePlaying = false;

    // Wywoływane, gdy gracz wejdzie w trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSequencePlaying)
        {
            StartCoroutine(PlayRadioSequence());
        }
    }
    
    private IEnumerator PlayRadioSequence()
    {
        isSequencePlaying = true;

        // Odtwarzamy pierwszy dźwięk od razu
        if (radioSource1 != null && radioSound1 != null)
        {
            radioSource1.clip = radioSound1;
            radioSource1.volume = radioSound1Volume;
            radioSource1.Play();
        }

        // Czekamy 0,5 sekundy przed odtworzeniem drugiego dźwięku
        yield return new WaitForSeconds(2f);

        // Odtwarzamy drugi dźwięk
        if (radioSource2 != null && radioSound2 != null)
        {
            radioSource2.clip = radioSound2;
            radioSource2.volume = radioSound2Volume;
            radioSource2.Play();
        }

        // Czekamy, aż oba źródła zakończą odtwarzanie
        while ((radioSource1 != null && radioSource1.isPlaying) || (radioSource2 != null && radioSource2.isPlaying))
        {
            yield return null;
        }

        // Sekwencja zakończona – możemy ponownie ją uruchomić
        isSequencePlaying = false;
    }
}