using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoriAnomaly : MonoBehaviour
{
    [SerializeField] private Transform lori;
    [SerializeField] private Vector3 targetLocalOffset = new Vector3(0f, 0f, 10f);
    [SerializeField] private float moveDuration = 5f;
    [SerializeField] private Ease moveEase = Ease.Linear;

    private Tween loriTween;
    private bool activated = false;
    private Vector3 initialLocalPosition;

    private void Start()
    {
        initialLocalPosition = lori.localPosition;
        lori.gameObject.SetActive(false);
    }

    public void ActivateLori()
    {
        if (lori == null) return;
        if (!activated)
        {
            activated = true;
            lori.gameObject.SetActive(true);
            loriTween = lori.DOLocalMove(initialLocalPosition + targetLocalOffset, moveDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(moveEase);
            
            if(AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayLoriSound();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateLori();
        }
    }
}