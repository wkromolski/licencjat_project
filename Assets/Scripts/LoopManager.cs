using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoopManager : MonoBehaviour
{
    [SerializeField] private List<SceneConfigsSO> sceneConfigs;
    [SerializeField] private LoopData loopData;  
    private string currentSceneName;

    private void Start()
    {
        if (loopData == null)
        {
            loopData = Resources.Load<LoopData>("LoopData"); 
        }
    }

    public void LoadNextScene()
    {
        SceneConfigsSO nextScene;
        do
        {
            nextScene = sceneConfigs[Random.Range(1, sceneConfigs.Count)];
        } while (nextScene.sceneName == currentSceneName);

        currentSceneName = nextScene.sceneName;
        SceneManager.LoadScene(currentSceneName);
    }

    public void CorrectDecision()
    {
        loopData.loopLevel++;  
        Debug.Log("Correct decision! Loop level: " + loopData.loopLevel);
        LoadNextScene();
    }

    public void WrongDecision()
    {
        loopData.loopLevel = 0; 
        Debug.Log("Wrong decision! Loop level reset to: " + loopData.loopLevel);
        LoadNextScene();
    }
}
