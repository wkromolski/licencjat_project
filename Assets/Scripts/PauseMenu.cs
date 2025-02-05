using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; 
    
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true); 
        isPaused = true;

        Time.timeScale = 0f;
        
        AudioListener.pause = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        
    }
    
    public void Resume()
    {
        pausePanel.SetActive(false); 
        isPaused = false;

        Time.timeScale = 1f;
        
        AudioListener.pause = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
