using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;
    
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private RectTransform image1;

    [Header("Animation Settings")]
    [SerializeField] private float fadeOutDuration = 1f;  
    [SerializeField] private float loadingDelay = 2f;   
    [SerializeField] private float fadeInDuration = 1f;  
    
    private Sequence loadingSequence;
    
    private Image image1Image;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(false);
        
        if (image1 != null)
        {
            image1.localScale = Vector3.one * 1.5f;
            image1Image = image1.GetComponent<Image>();
            if (image1Image != null)
            {
                Color col = image1Image.color;
                col.a = 0f;
                image1Image.color = col;
            }
            else
            {
                Debug.LogWarning("LoadingScreenManager: Obiekt image1 nie ma komponentu Image!");
            }
        }
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayFadeIn();
    }
    
    public void LoadSceneWithTransition(string sceneName)
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(true);

        loadingSequence = DOTween.Sequence();
        
        if (image1 != null)
        {
            loadingSequence.Append(image1.DOScale(1f, fadeOutDuration).SetEase(Ease.OutQuad));
            if (image1Image != null)
                loadingSequence.Join(image1Image.DOFade(1f, fadeOutDuration));
        }
        
        loadingSequence.AppendInterval(loadingDelay);
        
        loadingSequence.AppendCallback(() =>
        {
            SceneManager.LoadScene(sceneName);
        });

        loadingSequence.Play();
    }
    
    public void PlayFadeIn()
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(true);

        Sequence fadeInSequence = DOTween.Sequence();

        if (image1 != null)
        {
            fadeInSequence.Append(image1.DOScale(1.5f, fadeInDuration).SetEase(Ease.InQuad));
            if (image1Image != null)
                fadeInSequence.Join(image1Image.DOFade(0f, fadeInDuration));
        }

        fadeInSequence.OnComplete(() =>
        {
            if (loadingPanel != null)
                loadingPanel.SetActive(false);
        });

        fadeInSequence.Play();
    }
}