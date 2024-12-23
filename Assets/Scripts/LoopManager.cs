using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoopManager : MonoBehaviour
{
    [SerializeField] private List<SceneConfigsSO> sceneConfigs; 
    private string currentSceneName;
    

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
        GameManager.Instance.loopLevel++;
        Debug.Log("Correct decision! Loop level: " + GameManager.Instance.loopLevel);
        LoadNextScene();
    }

    public void WrongDecision()
    {
        GameManager.Instance.loopLevel = 0;
        Debug.Log("Wrong decision! Loop level reset to: " + GameManager.Instance.loopLevel);
        LoadNextScene();
    }
}
