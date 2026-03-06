using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvingControllerNonCoroutine : MonoBehaviour
{
    [Header("All character MeshRenderers")]
    [SerializeField] private List<SkinnedMeshRenderer> skinnedMeshRenderer;
    private List<Material> allSkinnedMaterials;

    // Zmieniamy podej?cie: zamiast sta?ych kroków, u?ywamy czasu (sekund)
    [SerializeField] private float dissolveSpeed = 0.5f; // Czas trwania efektu w sekundach
    private float dissolveCounter = 0;
    public bool isDissolving = false;

    private CapsuleCollider npcCollider;

    void Start()
    {
        npcCollider = GetComponent<CapsuleCollider>();
        allSkinnedMaterials = new List<Material>();

        if (skinnedMeshRenderer != null && skinnedMeshRenderer.Count > 0)
        {
            foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderer)
            {
                if (renderer != null)
                {
                    // U?ywamy .sharedMaterials lub pobieramy kopie, 
                    // zale?nie od tego, czy chcesz zmienia? materia? globalnie czy lokalnie
                    allSkinnedMaterials.AddRange(renderer.materials);
                }
            }
        }
    }

    void Update()
    {
        if (isDissolving)
        {
            UpdateDissolve();
        }
    }

    public void Dissolve()
    {
        if (allSkinnedMaterials != null && allSkinnedMaterials.Count > 0)
        {
            dissolveCounter = 0;
            isDissolving = true;
            Debug.Log("HAHAHAH");
        }
    }

    private void UpdateDissolve()
    {
        // Zwi?kszamy licznik o czas, który up?yn?? od ostatniej klatki
        // Dzielenie przez dissolveSpeed pozwala kontrolowa? czas trwania (np. 2 sekundy)
        dissolveCounter += Time.deltaTime * dissolveSpeed;

        for (int i = 0; i < allSkinnedMaterials.Count; i++)
        {
            allSkinnedMaterials[i].SetFloat("_DissolveAmount", dissolveCounter);
        }

        // Zatrzymujemy proces, gdy osi?gniemy pe?ne rozpuszczenie (1.0)
        if (dissolveCounter >= 1f)
        {
            isDissolving = false;
        }
    }

    public void TurnOffCollider()
    {
        if (npcCollider != null)
            npcCollider.enabled = false;
    }

}
