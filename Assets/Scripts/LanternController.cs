using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    [Header("Lantern Settings")]
    [SerializeField] private Light lanternLight; 
    [SerializeField] private GameObject layout1; 
    [SerializeField] private GameObject layout2; 

    public static bool IsLanternOn = true;
    
    [Header("Key Bindings")]
    [SerializeField] private KeyCode toggleKey = KeyCode.F; 

    private void Start()
    {
        IsLanternOn = true;
        if (lanternLight) lanternLight.enabled = true;
        if (layout1) layout1.SetActive(true);
        if (layout2) layout2.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            IsLanternOn = !IsLanternOn;

            if (lanternLight) lanternLight.enabled = IsLanternOn;
            if (layout1) layout1.SetActive(IsLanternOn);
            if (layout2) layout2.SetActive(!IsLanternOn);
        }
    }
}
