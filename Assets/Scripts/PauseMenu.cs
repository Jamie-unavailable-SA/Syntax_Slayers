using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles pause / resume and return‑to‑menu.
/// Attach to an empty GameObject (e.g. "PauseController").
/// </summary>
public class PauseController : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("The entire Pause menu panel (disabled at runtime).")]
    [SerializeField] private GameObject pausePanel;

    [Tooltip("Full‑screen black Image used for fade‑out. Optional.")]
    [SerializeField] private Image fadeOverlay;

    [Header("Gameplay")]
    [Tooltip("WordTimer script that spawns words.")]
    [SerializeField] private WordTimer wordTimer;

    [Header("Scene names")]
    [Tooltip("Scene to load when 'Main Menu' is pressed.")]
    [SerializeField] private string mainMenuScene = "MainMenu";

    [Header("Fade settings")]
    [SerializeField] private float fadeDuration = 0.5f;

    public static bool IsPaused { get; private set; }

    /* ─────────────────────────────────────────────────────────── */
    /*  INPUT                                                     */
    /* ─────────────────────────────────────────────────────────── */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    /* ─────────────────────────────────────────────────────────── */
    /*  PUBLIC API – called by UI buttons                         */
    /* ─────────────────────────────────────────────────────────── */
    public void Resume()
    {
        // deactivate menu UI
        pausePanel.SetActive(false);

        // resume gameplay
        Time.timeScale = 1f;
        if (wordTimer) wordTimer.enabled = true;

        IsPaused = false;
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(LoadMenuRoutine());
    }

    /* ─────────────────────────────────────────────────────────── */
    /*  INTERNAL HELPERS                                           */
    /* ─────────────────────────────────────────────────────────── */
    private void Pause()
    {
        // show menu UI
        pausePanel.SetActive(true);

        // pause gameplay
        Time.timeScale = 0f;
        if (wordTimer) wordTimer.enabled = false;

        IsPaused = true;
    }

    private IEnumerator LoadMenuRoutine()
    {
        // Un‑pause time so the fade runs in realtime
        Time.timeScale = 1f;

        // simple black fade
        if (fadeOverlay)
        {
            fadeOverlay.gameObject.SetActive(true);
            Color c = fadeOverlay.color;
            c.a = 0f;
            fadeOverlay.color = c;

            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                c.a = Mathf.Clamp01(t / fadeDuration);
                fadeOverlay.color = c;
                yield return null;
            }
        }

        // let main‑menu music start again
        GameOver.gobgmusicstopper = false;

        SceneManager.LoadScene(mainMenuScene);
    }
}
