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

    private void Start()
    {
        UpdateLoopUI();

        // Losuj anomalię z 65% szansą (nie powtarzaj tej samej co ostatnio)
        if (anomalyObjects.Count > 0 && Random.value <= 0.65f)
        {
            List<int> possibleIndices = new List<int>();
            for (int i = 0; i < anomalyObjects.Count; i++)
            {
                if (i != lastAnomalyIndex)
                    possibleIndices.Add(i);
            }

            int randomIndex = possibleIndices[Random.Range(0, possibleIndices.Count)];
            anomalyObjects[randomIndex].SetActive(true);
            lastAnomalyIndex = randomIndex;
        }
    }

    public void ProcessDoorChoice(string doorType)
    {
        // Krok 1: sprawdź, czy jakakolwiek anomalia jest aktywna
        bool anomalyActive = false;
        foreach (GameObject anomaly in anomalyObjects)
        {
            if (anomaly != null && anomaly.activeSelf)
            {
                anomalyActive = true;
                break;
            }
        }

        // Krok 2: zaktualizuj wynik na podstawie wyboru drzwi i anomalii
        if (doorType == "End")
        {
            loopData.loopValue = anomalyActive ? loopData.loopValue + 1 : 0;
        }
        else if (doorType == "Beginning")
        {
            loopData.loopValue = anomalyActive ? 0 : loopData.loopValue + 1;
        }

        // Krok 3: zaktualizuj UI
        UpdateLoopUI();

        // Krok 4: wygrana albo kolejna pętla
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