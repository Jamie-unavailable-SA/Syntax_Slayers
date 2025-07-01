using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInOnStart : MonoBehaviour
{
    [Header("Fade In Settings")]
    [Tooltip("Duration of fade in animation")]
    public float fadeDuration = 1f;
    
    [Tooltip("Color to fade from (should match StartButton fade color)")]
    public Color fadeColor = Color.black;
    
    [Tooltip("Delay before starting fade in")]
    public float startDelay = 0.1f;
    public GameObject player;
    
    private GameObject fadePanel;
    private Image fadeImage;
    
    void Start()
    {
        CreateFadePanel();
        StartCoroutine(FadeInSequence());
    }
    
    void CreateFadePanel()
    {
        // Find Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas found in scene! Fade effect requires a Canvas.");
            return;
        }
        
        // Create fade panel GameObject
        fadePanel = new GameObject("FadeInPanel");
        fadePanel.transform.SetParent(canvas.transform, false);
        
        // Add Image component for the fade effect
        fadeImage = fadePanel.AddComponent<Image>();
        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1f); // Start opaque
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
    }
    
    IEnumerator FadeInSequence()
    {
        // Wait for start delay
        yield return new WaitForSeconds(startDelay);
        
        // Fade in (from black to transparent)
        yield return StartCoroutine(FadeIn());
        player.SetActive(true);
        
        // Clean up fade panel when done
        if (fadePanel != null)
        {
            Destroy(fadePanel);
        }
    }
    
    IEnumerator FadeIn()
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
    }
}