// BackgroundAnimator.cs
//
// Put this script on your empty “Menu Controller” object.
// In the Inspector you’ll be able to:
//   • Drag the UI Image that should display the animation
//   • Drag‑drop all sliced sprites (frames) at once
//

using UnityEngine;
using UnityEngine.UI;   // <-- UI Image lives here

[DisallowMultipleComponent]
public class BackgroundAnimator : MonoBehaviour
{
    [Header("Target to animate")]
    [Tooltip("UI Image that will show the animated background")]
    [SerializeField] private Image targetImage;      // drag your Image here

    [Header("Animation frames")]
    [Tooltip("All frames from your sliced sprite sheet, in order")]
    [SerializeField] private Sprite[] frames;        // bulk‑drag frames here

    [Header("Playback settings")]
    [Tooltip("Frames per second")]
    [SerializeField, Range(1, 60)] private float frameRate = 10f;

    private int currentFrame;
    private float timer;

    // If you forgot to drag an Image, try to grab one on the same GameObject
    void Awake()
    {
        if (targetImage == null)
            targetImage = GetComponent<Image>();  // optional auto‑fallback
    }

    void Start()
    {
        if (!ValidateSetup()) return;

        targetImage.sprite = frames[0];            // show first frame immediately
    }

    void Update()
    {
        if (!ValidateSetup()) return;

        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            timer -= 1f / frameRate;
            currentFrame = (currentFrame + 1) % frames.Length;
            targetImage.sprite = frames[currentFrame];
        }
    }

    // Simple guard so the script fails loudly but safely
    private bool ValidateSetup()
    {
        if (targetImage == null)
        {
            Debug.LogWarning($"{name}: BackgroundAnimator needs a target Image.", this);
            enabled = false;
            return false;
        }

        if (frames == null || frames.Length == 0)
        {
            Debug.LogWarning($"{name}: BackgroundAnimator has no frames to play.", this);
            enabled = false;
            return false;
        }

        return true;
    }
}
