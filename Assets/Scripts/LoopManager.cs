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

    [Header("Final UI")]
    [SerializeField] private GameObject finalUIPanel;

    private static List<int> unusedAnomalyIndices = new List<int>();

    private void Start()
    {
        UpdateLoopUI();
        
        if (anomalyObjects.Count > 0 && Random.value <= 0.65f)
        {
            if (unusedAnomalyIndices == null || unusedAnomalyIndices.Count == 0)
            {
                unusedAnomalyIndices = new List<int>();
                for (int i = 0; i < anomalyObjects.Count; i++)
                    unusedAnomalyIndices.Add(i);
            }
            
            int randomListIndex = Random.Range(0, unusedAnomalyIndices.Count);
            int chosenIndex = unusedAnomalyIndices[randomListIndex];
        
            anomalyObjects[chosenIndex].SetActive(true);
            unusedAnomalyIndices.RemoveAt(randomListIndex); 
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
        
        if (loopData.loopValue >= 7)
        {
            ShowFinalUI();
        }
        else
        {
            LoadingScreenManager.Instance.LoadSceneWithTransition("Loop");
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