using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvingControllerNonCoroutine : MonoBehaviour
{
    [Header("All character MeshRenderers")]
    [SerializeField] private List<SkinnedMeshRenderer> skinnedMeshRenderer;
    private List<Material> allSkinnedMaterials;

    [SerializeField] private float dissolveSpeed = 0.5f;
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
        }
    }

    private void UpdateDissolve()
    {

        dissolveCounter += Time.deltaTime * dissolveSpeed;

        for (int i = 0; i < allSkinnedMaterials.Count; i++)
        {
            allSkinnedMaterials[i].SetFloat("_DissolveAmount", dissolveCounter);
        }

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
