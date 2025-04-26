using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAnomaly : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToDisable;
    
    private void OnEnable()
    {
        ActivateAnomaly();
    }

    private void ActivateAnomaly()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    private void OnDisable()
    {
        DisactivateAnomaly();
    }

    private void DisactivateAnomaly()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            if(obj != null)
                obj.SetActive(true);
        }
    }
}
