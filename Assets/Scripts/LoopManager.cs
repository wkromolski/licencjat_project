using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public LoopData loopData;
    [SerializeField] private TextMeshPro loopText3D;
    
    public List<GameObject> anomalyObjects;
    
    private static int lastAnomalyIndex = -1;
    
    [SerializeField] private GameObject finalUIPanel;

    void Start()
    {
        if (loopText3D != null)
            loopText3D.text = loopData.loopValue.ToString();
        
        if (anomalyObjects.Count > 0 && Random.value <= 0.6f)
        {
            List<int> possibleIndices = new List<int>();
            for (int i = 0; i < anomalyObjects.Count; i++)
            {
                if (i == lastAnomalyIndex)
                    continue;
                possibleIndices.Add(i);
            }
           
            int randomIndex = possibleIndices[Random.Range(0, possibleIndices.Count)];
            anomalyObjects[randomIndex].SetActive(true);
            lastAnomalyIndex = randomIndex;
        }
    }

    /// <summary>
    /// Metoda wywoływana przez skrypty drzwi, która aktualizuje wartość loopa w zależności od:
    /// - wybranych drzwi (parametr doorType – "Beginning" lub "End")
    /// - aktywności anomalii
    /// Po zmianie, metoda przeładowuje scenę przy użyciu LoadSceneWithTransition.
    /// </summary>
    /// <param name="doorType">Typ drzwi: "Beginning" lub "End"</param>
    public void ProcessDoorChoice(string doorType)
    {
        bool anomalyActive = false;

        GameObject[] anomalyObjectsInScene = GameObject.FindGameObjectsWithTag("Anomalies");
    
        foreach (GameObject anomaly in anomalyObjectsInScene)
        {
            if (anomaly.activeSelf)
            {
                anomalyActive = true;
                break;
            }
        }

        if (doorType == "End")
        {
            if (anomalyActive)
            {
                loopData.loopValue += 1;
            }
            else
            {
                loopData.loopValue = 0;
            }
        }
        else if (doorType == "Beginning")
        {
            if (anomalyActive)
            {
                loopData.loopValue = 0;
            }
            else
            {
                loopData.loopValue += 1;
            }
        }
        
        if (loopText3D != null)
            loopText3D.text = loopData.loopValue.ToString();
        
        if (loopData.loopValue >= 6)
        {
            ShowFinalUI();
        }
        else
        {
            // Przeładowanie sceny, jeśli jeszcze nie osiągnięto wygranej
            LoadingScreenManager.Instance.LoadSceneWithTransition("Loop");
        }
    }

    /// <summary>
    /// Metoda aktywująca finalne UI (np. ekran wygranej) i pauzująca rozgrywkę.
    /// </summary>
    private void ShowFinalUI()
    {
        if (finalUIPanel != null)
        {
            finalUIPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("FinalUIPanel nie został przypisany w Inspectorze!");
        }
        
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        if (LoadingScreenManager.Instance != null)
        {
            LoadingScreenManager.Instance.LoadSceneWithTransition("Menu");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
    
}