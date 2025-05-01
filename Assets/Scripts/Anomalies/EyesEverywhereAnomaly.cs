using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesEverywhereAnomaly : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spriteRenderers;
    [SerializeField] private Sprite anomalySprite;

    private List<Sprite> originalSprites;

    private void OnEnable()
    {
        if (spriteRenderers == null || anomalySprite == null) return;

        originalSprites = new List<Sprite>(spriteRenderers.Count);
        foreach (var sr in spriteRenderers)
        {
            if (sr == null)
            {
                originalSprites.Add(null);
                continue;
            }
            originalSprites.Add(sr.sprite);
            sr.sprite = anomalySprite;
        }
    }

    private void OnDisable()
    {
        if (originalSprites == null) return;

        int count = Mathf.Min(spriteRenderers.Count, originalSprites.Count);
        for (int i = 0; i < count; i++)
        {
            var sr = spriteRenderers[i];
            if (sr != null)
                sr.sprite = originalSprites[i];
        }

        originalSprites = null;
    }
}
