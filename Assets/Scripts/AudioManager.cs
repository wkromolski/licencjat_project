using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("Footstep Sounds")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioSource audioSource;
    private int lastPlayedIndex = -1; 
    
    [SerializeField] private AudioClip loriSound;
    [SerializeField] [Range(0f, 1f)] private float loriSoundVolume = 0.5f;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
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

    public void PlayLoriSound()
    {
        if (loriSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(loriSound, loriSoundVolume);
        }
    }
}