using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartButton : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Name of the scene to load (must be added to Build Settings)")]
    public string sceneToLoad = "GameScene";
    
    [Header("Fade Settings")]
    [Tooltip("Duration of fade out and fade in animations")]
    public float fadeDuration = 1f;
    
    [Tooltip("Color to fade to (usually black)")]
    public Color fadeColor = Color.black;
    
    private Button startButton;
    private GameObject fadePanel;
    private Image fadeImage;
    private bool isTransitioning = false;
    
    void Start()
    {
        // Get the Button component
        startButton = GetComponent<Button>();
        
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
        else
        {
            Debug.LogError("No Button component found on " + gameObject.name);
        }
        
        // Create fade panel
        CreateFadePanel();
    }
    
    void CreateFadePanel()
    {
        // Find or create Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas found in scene! Fade effect requires a Canvas.");
            return;
        }
        
        // Create fade panel GameObject
        fadePanel = new GameObject("FadePanel");
        fadePanel.transform.SetParent(canvas.transform, false);
        
        // Add Image component for the fade effect
        fadeImage = fadePanel.AddComponent<Image>();
        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f); // Start transparent
        fadeImage.raycastTarget = false;
        
        // Make it cover the entire screen
        RectTransform rectTransform = fadePanel.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        
        // Set high sorting order to appear on top
        fadePanel.transform.SetAsLastSibling();
        
        // Initially disable the panel
        fadePanel.SetActive(false);
    }
    
    public void StartGame()
    {
        if (isTransitioning) return; // Prevent multiple clicks
        
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("Scene to load is not specified!");
            return;
        }
        
        // Check if scene exists in build settings
        if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            StartCoroutine(TransitionToScene());
        }
        else
        {
            Debug.LogError($"Scene '{sceneToLoad}' not found! Make sure it's added to Build Settings.");
        }
    }
    
    IEnumerator TransitionToScene()
    {
        isTransitioning = true;
        
        // Disable button to prevent multiple clicks
        if (startButton != null)
            startButton.interactable = false;
        
        // Enable fade panel
        if (fadePanel != null)
            fadePanel.SetActive(true);
        
        // Fade out (to black)
        yield return StartCoroutine(FadeOut());
        
        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        
        // Wait until the scene is loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        // Note: Fade in will be handled by a FadeInOnStart script in the new scene
        // or you can add it to this script if you want it to persist
    }
    
    IEnumerator FadeOut()
    {
        if (fadeImage == null) yield break;
        
        float elapsedTime = 0f;
        Color startColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);
        Color endColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1f);
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            fadeImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        
        fadeImage.color = endColor;
    }
    
    // Public method to fade in (can be called from other scripts)
    public IEnumerator FadeIn()
    {
        if (fadeImage == null) yield break;
        
        float elapsedTime = 0f;
        Color startColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1f);
        Color endColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            fadeImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        
        fadeImage.color = endColor;
        
        // Disable fade panel when done
        if (fadePanel != null)
            fadePanel.SetActive(false);
    }
    
    void OnDestroy()
    {
        // Clean up listener when object is destroyed
        if (startButton != null)
        {
            startButton.onClick.RemoveListener(StartGame);
        }
    }
}