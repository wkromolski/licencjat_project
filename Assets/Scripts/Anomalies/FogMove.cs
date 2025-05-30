using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FogMove : MonoBehaviour
{
    [SerializeField] private float targetZ = 10f;
    [SerializeField] private float duration = 2f;
    [SerializeField] private bool useLocal = false;
    [SerializeField] private Ease easeType = Ease.Linear;

    private void Start()
    {
        if (useLocal)
        {
            transform.DOLocalMoveZ(targetZ, duration)
                .SetEase(easeType)
                .SetUpdate(true);
        }
        else
        {
            transform.DOMoveZ(targetZ, duration)
                .SetEase(easeType)
                .SetUpdate(true);
        }
    }
}