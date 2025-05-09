using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DoorsController : MonoBehaviour
{
    public string doorType;

    private bool playerInRange = false;
    private LoopManager loopManager;
    private float    lastInteractTime = -Mathf.Infinity;
    [SerializeField] private GameObject pressEUI;
    [SerializeField] private float interactCooldown = 10f;

    //Wojtkowy kod
    [Header("Cutscene - exit the door")]
    [SerializeField] private PlayableDirector cutsceneTimeline;
    [SerializeField] private float cutsceneTime = 3;

    void Start()
    {
        loopManager = FindObjectOfType<LoopManager>();
        
        if (pressEUI != null)
            pressEUI.SetActive(false);
    }

    void Update()
    {
        if (!playerInRange) return;
        
        if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime >= interactCooldown)
        {
            lastInteractTime = Time.time;

            //kod Sigmy
            //loopManager.ProcessDoorChoice(doorType);

            //Wojtkowy kod
            StartCoroutine(CutsceneRoutine());
            cutsceneTimeline.Play();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
        if (pressEUI != null)
            pressEUI.SetActive(true);
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
        playerInRange = true;
        if (pressEUI != null)
            pressEUI.SetActive(false);
    }

    //Wojtkowy kod
    IEnumerator CutsceneRoutine()
    {
        yield return new WaitForSeconds(cutsceneTime);
        loopManager.ProcessDoorChoice(doorType);
    }
}