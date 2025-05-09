using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DoorsController : MonoBehaviour
{
    public string doorType;

    private bool playerInRange = false;
    private bool  canInteract = false; 
    private LoopManager loopManager;
    private float    lastInteractTime = -Mathf.Infinity;
    [SerializeField] private GameObject pressEUI;
    [SerializeField] private float interactCooldown = 10f;
    [SerializeField] private float triggerDelay = 5f;
    private Collider triggerCol; 

    //Wojtkowy kod
    [Header("Cutscene - exit the door")]
    [SerializeField] private PlayableDirector cutsceneTimeline;
    [SerializeField] private float cutsceneTime = 3;

    void Start()
    {
        loopManager = FindObjectOfType<LoopManager>();
        triggerCol   = GetComponent<Collider>();
        if (pressEUI != null) pressEUI.SetActive(false);

        Invoke(nameof(EnableTrigger), triggerDelay);
    }
    private void EnableTrigger() => canInteract = true;
    private void OnTriggerEnter(Collider other)
    {
        if (!canInteract) return;
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        if (pressEUI) pressEUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        if (pressEUI) pressEUI.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange || !canInteract) return;

        if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractTime >= interactCooldown)
        {
            lastInteractTime = Time.time;
            canInteract      = false; 
            playerInRange    = false;

            if (pressEUI) pressEUI.SetActive(false);
            
            loopManager.ProcessDoorChoice(doorType);
            
            if (cutsceneTimeline) cutsceneTimeline.Play();
            
            if (triggerCol) triggerCol.enabled = false;

            StartCoroutine(WaitCutscene());
        }
    }

    private System.Collections.IEnumerator WaitCutscene()
    {
        yield return new WaitForSeconds(cutsceneTime);
    }
}