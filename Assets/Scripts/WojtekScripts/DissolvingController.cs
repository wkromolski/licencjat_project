using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvingController : MonoBehaviour
{
    [Header("All character MeshRenderers")]
    [SerializeField] private List<SkinnedMeshRenderer> skinnedMeshRenderer;
    private List<Material> allSkinnedMaterials;
    private float dissolveRate = 0.0125f;
    private float refreshRate = 0.025f;
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
                    allSkinnedMaterials.AddRange(renderer.materials);
                }
            }
        }
    }

    public void Dissolve()
    {

        if (allSkinnedMaterials != null && allSkinnedMaterials.Count > 0)
        {
            StartCoroutine(DissolveCo());
        }
    }


    IEnumerator DissolveCo()
    {
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
    }

    public void TurnOffCollider()
    {
        npcCollider.enabled = false;
    }
}