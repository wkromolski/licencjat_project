using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class LoopManager : MonoBehaviour
{
    public LoopData loopData;
    [SerializeField] private TextMeshPro loopText3D;
    
    public List<GameObject> anomalyObjects;
    
    private static int lastAnomalyIndex = -1;

    void Start()
    {
        if (loopText3D != null)
            loopText3D.text = loopData.loopValue.ToString();
        
        if (anomalyObjects.Count > 0 && Random.value <= 0.7f)
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
        // Sprawdzamy, czy jakiekolwiek obiekty z tagiem "Anomalies" są aktywne
        bool anomalyActive = false;

        // Sprawdzamy wszystkie obiekty na scenie, mające tag "Anomalies"
        GameObject[] anomalyObjectsInScene = GameObject.FindGameObjectsWithTag("Anomalies");
    
        foreach (GameObject anomaly in anomalyObjectsInScene)
        {
            if (anomaly.activeSelf)
            {
                anomalyActive = true;
                break;
            }
        }

        // Sprawdzamy wybór drzwi i aktualizujemy loopa
        if (doorType == "End")
        {
            if (anomalyActive)
            {
                // Jeśli anomalia jest aktywna, zwiększamy loop o 1
                loopData.loopValue += 1;
            }
            else
            {
                // Jeśli brak anomalii, resetujemy loop
                loopData.loopValue = 0;
            }
        }
        else if (doorType == "Beginning")
        {
            if (anomalyActive)
            {
                // Jeśli anomalia jest aktywna, resetujemy loop
                loopData.loopValue = 0;
            }
            else
            {
                // Jeśli brak anomalii, zwiększamy loop o 1
                loopData.loopValue += 1;
            }
        }
        else
        {
            Debug.LogWarning("Nieznany typ drzwi: " + doorType);
        }

        // Aktualizacja wyświetlanego tekstu (opcjonalnie)
        if (loopText3D != null)
            loopText3D.text = loopData.loopValue.ToString();
    
        // Przeładowanie sceny z animacją przejścia
        LoadingScreenManager.Instance.LoadSceneWithTransition("Loop");
    }
}