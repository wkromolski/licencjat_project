using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvingController : MonoBehaviour
{
    [Header("All character MeshRenderers")]
    public List<SkinnedMeshRenderer> skinnedMeshRenderer; // To jest lista!
    private List<Material> allSkinnedMaterials; // Zmieniamy to na listê materia³ów
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;


    // Start is called before the first frame update
    void Start()
    {
        allSkinnedMaterials = new List<Material>(); // Inicjalizujemy listê materia³ów

        if (skinnedMeshRenderer != null && skinnedMeshRenderer.Count > 0)
        {
            foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderer)
            {
                if (renderer != null)
                {
                    allSkinnedMaterials.AddRange(renderer.materials); // Dodajemy materia³y z ka¿dego renderera
                }
                else
                {
                    Debug.LogWarning("One of the SkinnedMeshRenderer entries in the list is null for " + gameObject.name);
                }
            }
        }
        else
        {
            Debug.LogError("SkinnedMeshRenderer list is not assigned or is empty in the Inspector for " + gameObject.name);
        }
    }

    public void Dissolve()
    {
        // Sprawdzamy, czy lista materia³ów nie jest pusta
        if (allSkinnedMaterials != null && allSkinnedMaterials.Count > 0)
        {
            StartCoroutine(DissolveCo());
        }
        else
        {
            Debug.LogWarning("Cannot start dissolve effect: No materials found. Ensure SkinnedMeshRenderers are assigned and have materials.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Sprawdzamy, czy lista materia³ów nie jest pusta
            if (allSkinnedMaterials != null && allSkinnedMaterials.Count > 0)
            {
                StartCoroutine(DissolveCo());
            }
            else
            {
                Debug.LogWarning("Cannot start dissolve effect: No materials found. Ensure SkinnedMeshRenderers are assigned and have materials.");
            }
        }
    }

    IEnumerator DissolveCo()
    {
        // U¿ywamy nowej listy materia³ów
        if (allSkinnedMaterials != null && allSkinnedMaterials.Count > 0)
        {
            float counter = 0;
            while (allSkinnedMaterials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for (int i = 0; i < allSkinnedMaterials.Count; i++)
                {
                    allSkinnedMaterials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
        else
        {
            Debug.LogWarning("DissolveCo called but allSkinnedMaterials is null or empty. This should not happen if previous checks passed.");
        }
    }
}