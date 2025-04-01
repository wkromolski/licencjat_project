using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private RectTransform image1;

    [Header("Animation Settings")] [SerializeField]
    private float fadeOutDuration = 1f;

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

    public void LoadSceneWithTransition(string sceneName)
    {
        StartCoroutine(LoadSceneWithImmediateActivation(sceneName));
    }

    private IEnumerator LoadSceneWithImmediateActivation(string sceneName)
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(true);

        if (image1 != null)
        {
            image1.localScale = Vector3.one * 1.5f;
            if (image1Image != null)
            {
                Color col = image1Image.color;
                col.a = 0f;
                image1Image.color = col;
            }
        }
        
        Sequence fadeOut = DOTween.Sequence();
        fadeOut.Append(image1.DOScale(1f, fadeOutDuration).SetEase(Ease.OutQuad));
        fadeOut.Join(image1Image.DOFade(1f, fadeOutDuration));
        yield return fadeOut.WaitForCompletion();
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = true;
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        yield return new WaitForSeconds(loadingDelay);
        
        Sequence fadeIn = DOTween.Sequence();
        fadeIn.Append(image1.DOScale(1.5f, fadeInDuration).SetEase(Ease.InQuad));
        fadeIn.Join(image1Image.DOFade(0f, fadeInDuration));
        yield return fadeIn.WaitForCompletion();

        if (loadingPanel != null)
            loadingPanel.SetActive(false);
    }
}