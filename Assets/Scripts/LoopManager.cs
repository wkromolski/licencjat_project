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

        // 70% szansy na aktywację anomalii
        if (anomalyObjects.Count > 0 && Random.value <= 0.65f)
        {
            // Zainicjalizuj lub zresetuj listę dostępnych indeksów
            if (unusedAnomalyIndices == null || unusedAnomalyIndices.Count == 0)
            {
                unusedAnomalyIndices = new List<int>();
                for (int i = 0; i < anomalyObjects.Count; i++)
                    unusedAnomalyIndices.Add(i);
            }

            // Losuj z dostępnych indeksów
            int randomListIndex = Random.Range(0, unusedAnomalyIndices.Count);
            int chosenIndex = unusedAnomalyIndices[randomListIndex];
        
            anomalyObjects[chosenIndex].SetActive(true);
            unusedAnomalyIndices.RemoveAt(randomListIndex); // usuń z puli

            Debug.Log("Wylosowano anomalię: " + anomalyObjects[chosenIndex].name);
        }
        else
        {
            Debug.Log("Brak anomalii w tej pętli.");
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