using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVsAnomaly : MonoBehaviour
{   
    [SerializeField] private GameObject[] objectsToEnable;
    
    [SerializeField] private AudioSource tvSource;
    [SerializeField] private AudioClip tvSound;
    
    private void OnEnable()
    {
        ActivateAnomaly();
    }

    private void ActivateAnomaly()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        if (tvSource != null)
        {
            tvSource.clip = tvSound;
            tvSource.Play();
        }
    }

    private void OnDisable()
    {
        DisactivateAnomaly();
    }

    private void DisactivateAnomaly()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            if(obj != null)
                obj.SetActive(false);
        }
    }
}
