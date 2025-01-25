using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeRemaining = 120f; 
    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private float timeIntervalOn = 1f;
    [SerializeField] private float timeIntervalOff = 0.5f;
    [SerializeField] private float timeSubtractOn = 1f;
    [SerializeField] private float timeSubtractOff = 1f;

    private void Start()
    {
        StartCoroutine(TimeLeft());
    }

    IEnumerator TimeLeft()
    {
        while (timeRemaining > 0)
        {
            DisplayTime();
            
            bool lampOn = LanternController.IsLanternOn;
            
            float waitTime = lampOn ? timeIntervalOn : timeIntervalOff;
            yield return new WaitForSeconds(waitTime);
            
            float subValue = lampOn ? timeSubtractOn : timeSubtractOff;
            timeRemaining -= subValue;
        }
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        TimeText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
