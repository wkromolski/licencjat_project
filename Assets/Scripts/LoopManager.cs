using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LoopManager : MonoBehaviour
{
    [SerializeField] private List<SceneConfigsSO> sceneConfigs;
    [SerializeField] private LoopData loopData;  
    private string currentSceneName;
    [SerializeField] private TextMeshPro loopText3D;

    private void Start()
    {
        if (loopData == null)
        {
            loopData = Resources.Load<LoopData>("LoopData"); 
        }
        UpdateLoopUI(); 
    }

    public void LoadNextScene()
    {
        SceneConfigsSO nextScene;
        do
        {
            nextScene = sceneConfigs[Random.Range(0, sceneConfigs.Count)];
        } while (nextScene.sceneName == SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(nextScene.sceneName);
    }

    public void CorrectDecision()
    {
        loopData.loopLevel++;  
        Debug.Log("Correct decision! Loop level: " + loopData.loopLevel);
        UpdateLoopUI();
        LoadNextScene();
    }

    public void WrongDecision()
    {
        loopData.loopLevel = 0; 
        Debug.Log("Wrong decision! Loop level reset to: " + loopData.loopLevel);
        UpdateLoopUI();
        SceneManager.LoadScene(0);
    }
    
    private void UpdateLoopUI()
    {
        if (loopText3D != null)
        {
            loopText3D.text = loopData.loopLevel.ToString();
        }
    }
}
