using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoopManager : MonoBehaviour
{
    public LoopData loopData;
    [SerializeField] private TextMeshPro loopText3D;

    [Header("Anomaly System")]
    public List<GameObject> anomalyObjects;
    private static int lastAnomalyIndex = -1;
    
    private static List<int> recentAnomalies = new List<int>();
    private const int maxRecent = 4;

    [Header("Radio System")]
    [SerializeField] private GameObject radioTrigger;

    private static int lastRadioIndex = -1;
    
    [Header("Final UI")]
    [SerializeField] private GameObject finalUIPanel;
    

    private void Start()
    {
        UpdateLoopUI();
        
        if (anomalyObjects.Count > 0 && Random.value <= 0.65f)
        {
            List<int> possible = new List<int>();

            for (int i = 0; i < anomalyObjects.Count; i++)
            {
                if (!recentAnomalies.Contains(i))
                    possible.Add(i);
            }

            if (possible.Count > 0)
            {
                int chosenIndex = possible[Random.Range(0, possible.Count)];

                anomalyObjects[chosenIndex].SetActive(true);

                recentAnomalies.Add(chosenIndex);

                if (recentAnomalies.Count > maxRecent)
                    recentAnomalies.RemoveAt(0);
            }
        }
        
        if (radioTrigger != null)
        {
            if (Random.value <= 0.15f)
                radioTrigger.SetActive(true);
            else
                radioTrigger.SetActive(false);
        }
    }

    public void ProcessDoorChoice(string doorType)
    {
        bool anomalyActive = false;
        foreach (GameObject anomaly in anomalyObjects)
        {
            if (anomaly != null && anomaly.activeSelf)
            {
                anomalyActive = true;
                break;
            }
        }
        
        if (doorType == "End")
        {
            loopData.loopValue = anomalyActive ? loopData.loopValue + 1 : 0;
        }
        else if (doorType == "Beginning")
        {
            loopData.loopValue = anomalyActive ? 0 : loopData.loopValue + 1;
        }
        
        UpdateLoopUI();
        
        if (loopData.loopValue >= 8)
        {
            ShowFinalUI();
        }
        else
        {
            //kod Sigmy
            //LoadingScreenManager.Instance.LoadSceneWithTransition("Loop");

            //Wojtkowy kod
            SceneManager.LoadScene("Loop");
        }
    }
    
    private IEnumerator FreezeAfterFadeIn()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 0f;
    }


    private void UpdateLoopUI()
    {
        if (loopText3D != null)
            loopText3D.text = loopData.loopValue.ToString();
    }

    private void ShowFinalUI()
    {
        if (finalUIPanel != null)
            finalUIPanel.SetActive(true);

        StartCoroutine(FreezeAfterFadeIn());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (LoadingScreenManager.Instance != null)
            LoadingScreenManager.Instance.LoadSceneWithTransition("Menu");
        else
            SceneManager.LoadScene("Menu");
    }
}