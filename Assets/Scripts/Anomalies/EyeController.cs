using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EyeController : MonoBehaviour
{
   [Header("References")]
    [SerializeField] private Transform eyeSprite;
    [SerializeField] private Transform player;

    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 10f;

    [Header("Movement Settings")]
    [SerializeField] private float maxZOffset = 0.5f;
    [SerializeField] private float tweenDuration = 0.2f;

    private float initialZ;

    private void Start()
    {
        initialZ = eyeSprite.position.z;

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    private void Update()
    {
        if (player == null || eyeSprite == null)
            return;

        float distanceXY = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
            new Vector2(player.position.x, player.position.y));
        if (distanceXY <= detectionRange)
        {
            float dz = player.position.z - transform.position.z;
            dz = Mathf.Clamp(dz, -detectionRange, detectionRange);
            float targetZOffset = (dz / detectionRange) * maxZOffset;
            float targetZ = initialZ + targetZOffset;
            eyeSprite.DOMoveZ(targetZ, tweenDuration).SetEase(Ease.OutQuad);
        }
        else
        {
            eyeSprite.DOMoveZ(initialZ, tweenDuration).SetEase(Ease.OutQuad);
        }
    }
}