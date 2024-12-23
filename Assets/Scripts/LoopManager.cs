using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoopManager : MonoBehaviour
{
    [SerializeField] private List<SceneConfigsSO> sceneConfigs; 
    private int loopLevel = 0;
    private string currentSceneName;
    

    public void LoadNextScene()
    {
        SceneConfigsSO nextScene;
        do
        {
            nextScene = sceneConfigs[Random.Range(0, sceneConfigs.Count)];
        } while (nextScene.sceneName == currentSceneName);
        
        currentSceneName = nextScene.sceneName;
        SceneManager.LoadScene(currentSceneName);
    }

    public void CorrectDecision()
    {
        loopLevel++;
        Debug.Log("Correct decision! Loop level: " + loopLevel);
        LoadNextScene();
    }

    public void WrongDecision()
    {
        loopLevel = 0;
        Debug.Log("Wrong decision! Loop level reset to: " + loopLevel);
        LoadNextScene();
    }
}
