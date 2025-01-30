using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Footstep Sounds")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioSource audioSource;
    private int lastPlayedIndex = -1; 

    public void PlayFootstep()
    {
        if (footstepSounds.Length == 0 || audioSource == null)
            return;

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, footstepSounds.Length);
        } while (randomIndex == lastPlayedIndex);

        lastPlayedIndex = randomIndex;
        audioSource.clip = footstepSounds[randomIndex];
        audioSource.Play();
    }
}