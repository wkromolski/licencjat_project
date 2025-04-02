using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingAnomaly : MonoBehaviour
{
    [Header("Source image")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite anomalySprite;

    private void OnEnable()
    {
        if (spriteRenderer != null && anomalySprite != null)
        {
            spriteRenderer.sprite = anomalySprite;
        }
    }

    private void OnDisable()
    {
        if (spriteRenderer != null && normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }
}
