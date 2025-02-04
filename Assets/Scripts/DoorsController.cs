using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    public string doorType;

    private bool playerInRange = false;
    private LoopManager loopManager;
    [SerializeField] private GameObject pressEUI;

    void Start()
    {
        loopManager = FindObjectOfType<LoopManager>();
        
        if (pressEUI != null)
            pressEUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            loopManager.ProcessDoorChoice(doorType);
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
}