using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    [SerializeField] private GameObject creditsUI;
    public void LoadLevel()
    {
        LoadingScreenManager.Instance.LoadSceneWithTransition("Level");
    }
    public void OpenCredits()
    {
        creditsUI.SetActive(true);
    }
    public void CloseUI()
    {
        creditsUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
