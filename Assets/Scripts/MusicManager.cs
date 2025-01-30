using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Music Tracks")]
    [SerializeField] private AudioClip ambientClip; 
    [SerializeField] private float ambientVolume = 0.5f; 
    [SerializeField] private AudioSource audioSrc; 

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        if (audioSrc == null)
        {
            audioSrc = gameObject.AddComponent<AudioSource>();
        }

        PlayAmbient(); 
    }

    private void PlayAmbient()
    {
        if (audioSrc.isPlaying) return; 

        audioSrc.clip = ambientClip;
        audioSrc.volume = ambientVolume;
        audioSrc.loop = true;
        audioSrc.Play();
    }
}