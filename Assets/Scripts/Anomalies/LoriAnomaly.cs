using UnityEngine;
using DG.Tweening;
using System.Collections;

public class LoriAnomaly : MonoBehaviour
{
    [Header("OIIA LORI")]
    [SerializeField] private Transform animatedObject;

    [Header("Float")]
    [SerializeField] private float floatDistance = 0.1f;
    [SerializeField] private float floatDuration = 1.5f;

    [Header("Spin")]
    [SerializeField] private float minSpinSpeed = 20f;
    [SerializeField] private float maxSpinSpeed = 90f;
    [SerializeField] private float spinChangeInterval = 3f;

    private Tween floatTween;
    private Tween spinTween;
    private Coroutine spinRoutine;

    private void OnEnable()
    {
        if (animatedObject == null) return;

        StartFloating();
        spinRoutine = StartCoroutine(SpinRoutine());
    }

    private void OnDisable()
    {
        floatTween?.Kill();
        spinTween?.Kill();
        if (spinRoutine != null) StopCoroutine(spinRoutine);
    }

    private void StartFloating()
    {
        Vector3 startPos = animatedObject.localPosition;
        Vector3 upPos = startPos + new Vector3(0f, floatDistance, 0f);

        floatTween = animatedObject.DOLocalMoveY(upPos.y, floatDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private IEnumerator SpinRoutine()
    {
        while (true)
        {
            float randomSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);
            int direction = Random.value < 0.5f ? 1 : -1;

            float rotationPerSecond = randomSpeed * direction;
            float duration = spinChangeInterval;

            spinTween = animatedObject.DOLocalRotate(
                animatedObject.localEulerAngles + new Vector3(0f, rotationPerSecond * duration, 0f),
                duration,
                RotateMode.FastBeyond360
            ).SetEase(Ease.Linear);

            yield return new WaitForSeconds(spinChangeInterval);
        }
    }
}