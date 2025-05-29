using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitch : MonoBehaviour
{
    public GameObject[] targetObjects;
    public Material materialA_Main;
    public Material materialA_Outline;
    public Material materialB_Main;
    public Material materialB_Outline;

    public KeyCode switchKey = KeyCode.F; 

    private bool usingA = true;

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            SwitchMaterials();
        }
    }

    private void SwitchMaterials()
    {
        Material[] selectedMaterials = usingA
            ? new Material[] { materialB_Main, materialB_Outline }
            : new Material[] { materialA_Main, materialA_Outline };

        foreach (GameObject obj in targetObjects)
        {
            var renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.materials = selectedMaterials;
            }
        }

        usingA = !usingA;
    }
}
