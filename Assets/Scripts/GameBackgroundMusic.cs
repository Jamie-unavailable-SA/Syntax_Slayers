using System.Collections;
using UnityEngine;

/// <summary>
/// Plays one looping music track until GameOver.gobgmusicstopper == true.
/// Attach to a GameObject that also has an AudioSource.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private bool playOnStart = true;

    [Header("Fade‑out when stopping")]
    [SerializeField] private float fadeOutDuration = 0.5f;   // seconds

    private AudioSource audioSource;
    private bool isFading = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip        = musicClip;
        audioSource.loop        = true;
        audioSource.playOnAwake = false;      // we’ll start it manually
    }

    private void Start()
    {
        if (playOnStart && musicClip != null)
            audioSource.Play();
    }

    private void Update()
    {
        // GameOver sets this flag to true → fade out music once
        if (GameOver.gobgmusicstopper == true && audioSource.isPlaying && !isFading)
            StartCoroutine(FadeOutAndStop());
    }

    private IEnumerator FadeOutAndStop()
    {
        isFading = true;

        float startVol = audioSource.volume;
        float t = 0f;

        while (t < fadeOutDuration)
        {
            t += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(startVol, 0f, t / fadeOutDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVol;    // reset so it’s ready if you replay
    }
}
