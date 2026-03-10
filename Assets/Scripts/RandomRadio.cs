using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRadio : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource radioSource;

    [Header("Radio Clips")]
    [SerializeField] private AudioClip[] radioClips;

    [SerializeField, Range(0f,1f)]
    private float volume = 0.6f;

    private static int lastPlayedIndex = -1;

    private bool isPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || isPlaying)
            return;

        StartCoroutine(PlayRadio());
    }

    private IEnumerator PlayRadio()
    {
        isPlaying = true;

        int chosenIndex = GetRandomClip();

        radioSource.clip = radioClips[chosenIndex];
        radioSource.volume = volume;
        radioSource.Play();

        while (radioSource.isPlaying)
            yield return null;

        isPlaying = false;
        gameObject.SetActive(false);
    }

    private int GetRandomClip()
    {
        int index;

        do
        {
            index = Random.Range(0, radioClips.Length);
        }
        while (index == lastPlayedIndex && radioClips.Length > 1);

        lastPlayedIndex = index;
        return index;
    }
}
