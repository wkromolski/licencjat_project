using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField] private bool isSecondDoor; 
    [SerializeField] private LoopManager loopManager;
    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered door trigger: " + gameObject.name);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited door trigger: " + gameObject.name);
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player pressed E near door: " + gameObject.name);
            if (isSecondDoor && loopManager != null)
            {
                Debug.Log("Correct door chosen.");
                loopManager.CorrectDecision();
            }
            else if (!isSecondDoor && loopManager != null)
            {
                Debug.Log("Wrong door chosen.");
                loopManager.WrongDecision();
            }
        }
    }
}
