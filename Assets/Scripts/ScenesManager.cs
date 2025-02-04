using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void LoadLevel()
    {
        LoadingScreenManager.Instance.LoadSceneWithTransition("Level");
    }
}
