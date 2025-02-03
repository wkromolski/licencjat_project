using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ClockTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeRemaining = 120f;
    
    [Header("Clock Hands")]
    [SerializeField] private Transform secondHand;
    [SerializeField] private Transform minuteHand; 

    [Header("Second Tick Settings")]
    [SerializeField] private float tickDuration = 0.2f;  
    [SerializeField] private float brightTickInterval = 1f; 
    [SerializeField] private float darkTickInterval = 0.5f;
    
    private const float secondTickAngle = -6f; 
    private const float minuteTickAngle = -6f;

    private void Start()
    {
        StartCoroutine(TickTimer());
    }

    private void Update()
    {
        Debug.Log("time remaining: " + timeRemaining + " seconds");
    }

    private IEnumerator TickTimer()
    {
        int secondsElapsed = 0;
        while (timeRemaining > 0)
        {
            Vector3 newSecondRotation = secondHand.eulerAngles + new Vector3(0f, 0f, -secondTickAngle);
            secondHand.DORotate(newSecondRotation, tickDuration).SetEase(Ease.OutQuad);

            bool lampOn = LanternController.IsLanternOn;
            float tickInterval = lampOn ? brightTickInterval : darkTickInterval;

            yield return new WaitForSeconds(tickInterval);

            timeRemaining -= 1f;
            secondsElapsed++;

            if (secondsElapsed % 60 == 0)
            {
                Vector3 newMinuteRotation = minuteHand.eulerAngles + new Vector3(0f, 0f, -minuteTickAngle);
                minuteHand.DORotate(newMinuteRotation, tickDuration).SetEase(Ease.OutQuad);
            }
        }
        
        Debug.Log("Clock Timer finished");
    }
}
