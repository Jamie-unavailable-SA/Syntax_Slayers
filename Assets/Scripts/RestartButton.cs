using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Attach this to your Restart button.  Drag the full‑screen black
/// overlay Image into the inspector, set a fade time, and you're done.
/// </summary>
[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    [Header("Fade settings")]
    [Tooltip("Full‑screen UI Image whose colour is solid black with alpha 0.")]
    [SerializeField] private Image fadeOverlay;
    [SerializeField] private float fadeDuration = 0.5f;   // seconds

    private Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnRestartClicked);

        // Make sure the overlay starts invisible
        if (fadeOverlay != null)
        {
            fadeOverlay.gameObject.SetActive(false);
            fadeOverlay.color = new Color(0, 0, 0, 0);    // transparent black
        }
    }

    void OnDestroy()
    {
        btn.onClick.RemoveListener(OnRestartClicked);
    }

    private void OnRestartClicked()
    {
        btn.interactable = false;             // guard against double‑clicks
        StartCoroutine(RestartRoutine());
    }

    private IEnumerator RestartRoutine()
    {
        // 1) Fade to black
        if (fadeOverlay != null)
        {
            fadeOverlay.gameObject.SetActive(true);

            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                float a = Mathf.Clamp01(t / fadeDuration);
                fadeOverlay.color = new Color(0, 0, 0, a);
                yield return null;
            }
        }

        // 2) Reload the current scene
        GameOver.gobgmusicstopper = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
