using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeController : MonoBehaviour
{
    [Header("Time Settings")]
    public float startTimeInSeconds = 11 * 3600 + 58 * 60; //11:58:00
    public float endTimeInSeconds = 12 * 3600; //12:00
    private float currentTime;
    
    [Header("Tick Settings")]
    public float brightTickInterval = 1f;
    public float darkTickInterval = 0.5f;
    public float tickDuration = 0.2f;
    private const float secondTickAngle = -6f;
    private const float minuteTickAngle = -6f;

    [Header("Clock Hands (Physical Clock)")]
    [SerializeField] private Transform secondHand; 
    [SerializeField] private Transform minuteHand; 

    [Header("UI Clock")]
    [SerializeField] private GameObject clockUIPanel;
    [SerializeField] private TextMeshProUGUI clockText; 

    private void Start()
    {
        currentTime = startTimeInSeconds;
        if (clockUIPanel != null)
            clockUIPanel.SetActive(false);
        
        StartCoroutine(TickTimer());
    }

    private IEnumerator TickTimer()
    {
        int ticksElapsed = 0;

        while (currentTime < endTimeInSeconds)
        {
            Vector3 newSecondRotation = secondHand.eulerAngles + new Vector3(0f, 0f, -secondTickAngle);
            secondHand.DORotate(newSecondRotation, tickDuration).SetEase(Ease.OutQuad);
            
            if (ticksElapsed > 0 && ticksElapsed % 60 == 0)
            {
                Vector3 newMinuteRotation = minuteHand.eulerAngles + new Vector3(0f, 0f, -minuteTickAngle);
                minuteHand.DORotate(newMinuteRotation, tickDuration).SetEase(Ease.OutQuad);
            }

            bool lampOn = LanternController.IsLanternOn;
            float tickInterval = lampOn ? brightTickInterval : darkTickInterval;

            yield return new WaitForSeconds(tickInterval);
            
            currentTime += 1f;
            ticksElapsed++;
            
            UpdateClockUIText();
        }
        LoadingScreenManager.Instance.LoadSceneWithTransition("Level");
    }

    private void UpdateClockUIText()
    {
        int totalSeconds = Mathf.FloorToInt(currentTime);
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;
        if (clockText != null)
        {
            clockText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (clockUIPanel != null)
                clockUIPanel.SetActive(true);
            Debug.Log("UI Clock activated. Current time: " + FormatTime(currentTime));
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (clockUIPanel != null)
                clockUIPanel.SetActive(false);
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int totalSeconds = Mathf.FloorToInt(timeInSeconds);
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
