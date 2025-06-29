using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MainMenuMusic : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource; // Optional, if you want to assign an existing AudioSource
    public AudioClip menuMusicClip;  // Drag your music file here
    [Range(0f, 1f)] public float volume = 0.6f;
    public bool loop = true;
    public bool persistAcrossScenes = true;

    [Header("Optional Fade-In")]
    public bool fadeIn = true;
    public float fadeDuration = 2f; // Seconds

    [Header("Fade-Out")]
    public float fadeOutDuration = 1.5f; // Seconds for fade out

    private AudioSource _source;
    private bool isFadingOut = false;
    private Coroutine currentFadeCoroutine;

    // Static reference to allow other scripts to trigger fade out
    public static MainMenuMusic Instance;

    void Awake()
    {
        // Set up singleton instance
        if (Instance == null)
        {
            Instance = this;
        }

        // Make sure only one instance exists
        if (persistAcrossScenes)
        {
            int musicScripts = FindObjectsOfType<MainMenuMusic>().Length;
            if (musicScripts > 1)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        // Configure or create AudioSource
        _source = GetComponent<AudioSource>();
        _source.clip = menuMusicClip;
        _source.loop = loop;
        _source.playOnAwake = false;
        _source.volume = fadeIn ? 0f : volume;
    }

    void Start()
    {
        if (menuMusicClip == null)
        {
            Debug.LogWarning("MainMenuMusic: No AudioClip assigned.");
            return;
        }

        _source.Play();

        // Optionally fade in
        if (fadeIn)
        {
            currentFadeCoroutine = StartCoroutine(FadeInCoroutine());
        }
    }

    IEnumerator FadeInCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            _source.volume = Mathf.Lerp(0f, volume, elapsed / fadeDuration);
            yield return null;
        }
        _source.volume = volume; // clamp
        currentFadeCoroutine = null;
    }

    // Method to trigger fade out (can be called by other scripts)
    public void FadeOut()
    {
        if (!isFadingOut && _source.isPlaying)
        {
            // Stop any current fade coroutine
            if (currentFadeCoroutine != null)
            {
                StopCoroutine(currentFadeCoroutine);
            }
            
            currentFadeCoroutine = StartCoroutine(FadeOutCoroutine());
        }
    }

    // Method to fade out and then stop completely
    public void FadeOutAndStop()
    {
        if (!isFadingOut && _source.isPlaying)
        {
            // Stop any current fade coroutine
            if (currentFadeCoroutine != null)
            {
                StopCoroutine(currentFadeCoroutine);
            }
            
            currentFadeCoroutine = StartCoroutine(FadeOutAndStopCoroutine());
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        isFadingOut = true;
        float startVolume = _source.volume;
        float elapsed = 0f;

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            _source.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeOutDuration);
            yield return null;
        }

        _source.volume = 0f;
        isFadingOut = false;
        currentFadeCoroutine = null;
    }

    IEnumerator FadeOutAndStopCoroutine()
    {
        isFadingOut = true;
        float startVolume = _source.volume;
        float elapsed = 0f;

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            _source.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeOutDuration);
            yield return null;
        }

        _source.volume = 0f;
        _source.Stop();
        isFadingOut = false;
        currentFadeCoroutine = null;
    }

    // Method to restart music with optional fade in
    public void RestartMusic(bool shouldFadeIn = true)
    {
        if (menuMusicClip == null) return;

        // Stop any current fade
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
            currentFadeCoroutine = null;
        }

        isFadingOut = false;
        _source.volume = shouldFadeIn ? 0f : volume;
        _source.Play();

        if (shouldFadeIn)
        {
            currentFadeCoroutine = StartCoroutine(FadeInCoroutine());
        }
    }

    // Method to stop music immediately (no fade)
    public void StopMusic()
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
            currentFadeCoroutine = null;
        }
        
        _source.Stop();
        _source.volume = volume;
        isFadingOut = false;
    }

    void OnDestroy()
    {
        // Clear static reference
        if (Instance == this)
        {
            Instance = null;
        }
    }
}