using UnityEngine;
using UnityEngine.UI;

public class buttonClickSound : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip buttonClickSoundClip;
    public float volume = 1f;
    
    private AudioSource audioSource;
    private Button button;
    
    void Start()
    {
        // Get the Button component
        button = GetComponent<Button>();
        
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Configure AudioSource
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
        
        // Add listener to button click
        if (button != null)
        {
            button.onClick.AddListener(PlayButtonSound);
        }
        else
        {
            Debug.LogWarning("No Button component found on " + gameObject.name);
        }
    }
    
    public void PlayButtonSound()
    {
        if (buttonClickSoundClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSoundClip, volume);
        }
        else
        {
            Debug.LogWarning("Button click sound not assigned or AudioSource missing on " + gameObject.name);
        }
    }
    
    void OnDestroy()
    {
        // Clean up listener when object is destroyed
        if (button != null)
        {
            button.onClick.RemoveListener(PlayButtonSound);
        }
    }
}