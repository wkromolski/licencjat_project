using UnityEngine;
using DG.Tweening;

public class CabinetDoorAnomaly : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private Transform doorTransform;

    [Header("Movement")]
    [SerializeField] private float angleOffset = 15f;
    [SerializeField] private float openAngleY = 30f;
    [SerializeField] private float minDuration = 1f;
    [SerializeField] private float maxDuration = 3f;
    [SerializeField] private float randomizeInterval = 4f;

    private float baseY;
    private Sequence doorSequence;

    private void OnEnable()
    {
        baseY = doorTransform.localEulerAngles.y;
        
        if (baseY > 180f)
            baseY -= 360f;

        StartAnimation();
        InvokeRepeating(nameof(RandomizeSpeed), randomizeInterval, randomizeInterval);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(RandomizeSpeed));
        if (doorSequence != null && doorSequence.IsActive())
            doorSequence.Kill();
    }

    private void StartAnimation()
    {
        float duration = Random.Range(minDuration, maxDuration);

        float openY = baseY + angleOffset;
        float closeY = baseY - angleOffset;

        doorSequence = DOTween.Sequence();
        doorSequence.Append(doorTransform.DOLocalRotate(
                new Vector3(0f, openY, 0f), duration)
            .SetEase(Ease.InOutSine));
        doorSequence.Append(doorTransform.DOLocalRotate(
                new Vector3(0f, closeY, 0f), duration)
            .SetEase(Ease.InOutSine));
        doorSequence.SetLoops(-1);
    }

    private void RandomizeSpeed()
    {
        if (doorSequence != null && doorSequence.IsActive())
        {
            doorSequence.Kill();
            StartAnimation();
        }
    }
}