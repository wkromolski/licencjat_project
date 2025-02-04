using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;

    [Header("UI Elements (Loading Screen)")]
    [SerializeField] private GameObject loadingPanel;
    [Tooltip("Obraz nr 1 – animacja skalowania (RectTransform)")]
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
        // Na starcie wyłączamy panel loading screena
        if (loadingPanel != null)
            loadingPanel.SetActive(false);

        // Ustawiamy początkowy stan obrazu
        if (image1 != null)
        {
            image1.localScale = Vector3.one * 1.5f;
            // Pobieramy komponent Image, aby animować alfę
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
    
    /// <summary>
    /// Wywołuje animację przejścia (FadeOut, loading delay, a potem ładuje scenę)
    /// </summary>
    /// <param name="sceneName">Nazwa sceny do załadowania</param>
    public void LoadSceneWithTransition(string sceneName)
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(true);

        loadingSequence = DOTween.Sequence();
        
        if (image1 != null)
        {
            // FadeOut: skalowanie z 1.5 -> 1
            loadingSequence.Append(image1.DOScale(1f, fadeOutDuration).SetEase(Ease.OutQuad));
            // FadeOut: alfa obrazu z 0 -> 1 (pod warunkiem, że image1Image jest dostępny)
            if (image1Image != null)
                loadingSequence.Join(image1Image.DOFade(1f, fadeOutDuration));
        }
        
        // Loading delay
        loadingSequence.AppendInterval(loadingDelay);

        // Po upływie delay'u, ładujemy scenę
        loadingSequence.AppendCallback(() =>
        {
            SceneManager.LoadScene(sceneName);
        });

        loadingSequence.Play();
    }

    /// <summary>
    /// Animacja FadeIn – obraz zmienia skalę z 1 -> 1.5 oraz alfę z 1 -> 0.
    /// Po zakończeniu animacji panel loading screena jest wyłączany.
    /// </summary>
    public void PlayFadeIn()
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(true);

        Sequence fadeInSequence = DOTween.Sequence();

        if (image1 != null)
        {
            // FadeIn: skalowanie z 1 -> 1.5
            fadeInSequence.Append(image1.DOScale(1.5f, fadeInDuration).SetEase(Ease.InQuad));
            // FadeIn: alfa obrazu z 1 -> 0 (pod warunkiem, że image1Image jest dostępny)
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