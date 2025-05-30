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
    [SerializeField] private AudioSource creakingSource;
    
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
    
    [Header("Creaking Sound")]
    [SerializeField] private AudioClip[] creakingSound;
    private int lastPlayedCreakIndex = -1;
    [SerializeField] [Range(0f, 1f)] private float creaksSoundVolume = 0.5f;
    
    [Header("Breath Sound")]
    [SerializeField] private AudioClip[] breathSound;
    private int lastPlayedBreathIndex = -1;
    [SerializeField] [Range(0f, 1f)] private float breathSoundVolume = 0.5f;
    
    
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

    public void PlayBreathSound()
    {
        if (breathSound.Length == 0 || breathSource == null)
            return;

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, breathSound.Length);
        } while (randomIndex == lastPlayedBreathIndex);

        lastPlayedBreathIndex = randomIndex;
        breathSource.clip = breathSound[randomIndex];
        breathSource.Play();
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
    
    public void PlayCreakSound()
    {
        if (creakingSound.Length == 0 || creakingSource == null)
            return;

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, breathSound.Length);
        } while (randomIndex == lastPlayedCreakIndex);

        lastPlayedCreakIndex = randomIndex;
        creakingSource.clip = creakingSound[randomIndex];
        creakingSource.Play();
    }

    private IEnumerator BreathSoundCoroutine()
    {
        while (true)
        {
            float randomDelay = Random.Range(10f, 35f);
            yield return new WaitForSeconds(randomDelay);

            if (!LanternController.IsLanternOn)
            {
                PlayBreathSound();
            }

            yield return new WaitForSeconds(10f - randomDelay);
        }
    }

    private IEnumerator NailsSoundCoroutine()
    {
        while (true)
        {
            float randomDelay = Random.Range(5f, 20f);
            yield return new WaitForSeconds(randomDelay);

            if (!LanternController.IsLanternOn)
            {
                PlayNails();
            }

            yield return new WaitForSeconds(20f - randomDelay);
        }
    }
    
    
    private IEnumerator CreakingSoundCoroutine()
    {
        while (true)
        {
            float randomDelay = Random.Range(5f, 20f);
            yield return new WaitForSeconds(randomDelay);
                PlayCreakSound();
            yield return new WaitForSeconds(20f - randomDelay);
        }
    }

    private void Start()
    {
        StartCoroutine(BreathSoundCoroutine());
        StartCoroutine(NailsSoundCoroutine());
        StartCoroutine(CreakingSoundCoroutine());
    }
}