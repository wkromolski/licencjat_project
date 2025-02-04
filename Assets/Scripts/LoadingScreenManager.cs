using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;

    [Header("UI Elements (Loading Screen)")]
    [SerializeField] private RectTransform image1;
    [SerializeField] private Image image2;

    [Header("Animation Settings")]
    [SerializeField] private float fadeOutDuration = 1f;  
    [SerializeField] private float loadingDelay = 2f;
    [SerializeField] private float fadeInDuration = 1f; 

    private Sequence loadingSequence;

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
        if (image1 != null)
            image1.localScale = Vector3.one * 1.5f;
        if (image2 != null)
        {
            Color col = image2.color;
            col.a = 0f;
            image2.color = col;
        }
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayFadeIn();
    }


    public void LoadSceneWithTransition(string sceneName)
    {
        loadingSequence = DOTween.Sequence();
        
        if (image1 != null)
            loadingSequence.Append(image1.DOScale(1f, fadeOutDuration).SetEase(Ease.OutQuad));
        if (image2 != null)
            loadingSequence.Join(image2.DOFade(1f, fadeOutDuration));
        
        loadingSequence.AppendInterval(loadingDelay);
        
        loadingSequence.AppendCallback(() =>
        {
            SceneManager.LoadScene(sceneName);
        });

        loadingSequence.Play();
    }

  
    public void LoadSceneWithTransition(int sceneIndex)
    {
        loadingSequence = DOTween.Sequence();

        if (image1 != null)
            loadingSequence.Append(image1.DOScale(1f, fadeOutDuration).SetEase(Ease.OutQuad));
        if (image2 != null)
            loadingSequence.Join(image2.DOFade(1f, fadeOutDuration));

        loadingSequence.AppendInterval(loadingDelay);

        loadingSequence.AppendCallback(() =>
        {
            SceneManager.LoadScene(sceneIndex);
        });

        loadingSequence.Play();
    }
    
    public void PlayFadeIn()
    {
        Sequence fadeInSequence = DOTween.Sequence();

        if (image1 != null)
            fadeInSequence.Append(image1.DOScale(1.5f, fadeInDuration).SetEase(Ease.InQuad));
        if (image2 != null)
            fadeInSequence.Join(image2.DOFade(0f, fadeInDuration));

        fadeInSequence.Play();
    }
}
