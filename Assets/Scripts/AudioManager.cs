using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("Spatial Audio Sources")]
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioSource tickSource;
    [SerializeField] private AudioSource nailsSource;
    [SerializeField] private AudioSource breathSource;
    [SerializeField] private AudioSource loriSource;
    
    
    [Header("Footstep Sounds")]
    [SerializeField] AudioClip[] stepSounds;
    private int lastPlayedStepIndex = -1;
    [SerializeField] [Range(0f, 1f)] private float stepsSoundVolume = 0.5f;

    [Header("Tick Sounds")] [SerializeField]
    private AudioClip[] ticksSounds;
    private int lastPlayedTickIndex = -1;
    [SerializeField] [Range(0f, 1f)] private float ticksSoundVolume = 0.5f;
    
    [Header("Creepy Sounds")] 
    [SerializeField] private AudioClip[] creepySounds;
    private int lastPlayedCreepyIndex = -1;
    [SerializeField] [Range(0f, 1f)] private float creepySoundVolume = 0.5f;
    
    [Header("Breath Sound")]
    [SerializeField] private AudioClip breathSound;
    [SerializeField] [Range(0f, 1f)] private float breathSoundVolume = 0.5f;
    
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
        if (stepSounds.Length == 0 || footstepSource == null)
            return;

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, stepSounds.Length);
        } while (randomIndex == lastPlayedStepIndex);

        lastPlayedStepIndex = randomIndex;
        footstepSource.clip = stepSounds[randomIndex];
        footstepSource.Play();
    }

    public void PlayTickSpatial()
    {
        if (ticksSounds.Length == 0)
            return;

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, ticksSounds.Length);
        } while (randomIndex == lastPlayedTickIndex);

        lastPlayedTickIndex = randomIndex;
        
        AudioSource.PlayClipAtPoint(ticksSounds[randomIndex], transform.position, 1f);
    }

    public void PlayLoriSound()
    {
        if (loriSound != null && loriSource != null)
        {
            loriSource.PlayOneShot(loriSound, loriSoundVolume);
        }
    }

    public void PlayBreathSound()
    {
        if (breathSound != null && breathSource != null)
        {
            breathSource.PlayOneShot(breathSound, breathSoundVolume);
        }
    }
    
    public void PlayNails()
    {
        if (creepySounds.Length == 0 || nailsSource == null)
            return;

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, creepySounds.Length);
        } while (randomIndex == lastPlayedCreepyIndex);

        lastPlayedCreepyIndex = randomIndex;
        nailsSource.clip = creepySounds[randomIndex];
        nailsSource.Play();
    }
    
    private IEnumerator BreathSoundCoroutine()
    {
        while (true)
        {
            float randomDelay = Random.Range(5f, 30f);
            yield return new WaitForSeconds(randomDelay);
            
            if (!LanternController.IsLanternOn)
            {
                PlayBreathSound();
            }
            
            yield return new WaitForSeconds(120f - randomDelay);
        }
    }
    
    private IEnumerator NailsSoundCoroutine()
    {
        while (true)
        {
            float randomDelay = Random.Range(15f, 50f);
            yield return new WaitForSeconds(randomDelay);
            
            if (!LanternController.IsLanternOn)
            {
                PlayNails();
            }
            
            yield return new WaitForSeconds(120f - randomDelay);
        }
    }
    
    private void Start()
    {
        StartCoroutine(BreathSoundCoroutine());
        StartCoroutine(NailsSoundCoroutine());
    }
}