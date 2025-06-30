using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private Button quitButton;
    
    void Start()
    {
        // Get the Button component
        quitButton = GetComponent<Button>();
        
        if (quitButton != null)
        {
            // Add the quit function to the button's click event
            quitButton.onClick.AddListener(QuitGame);
        }
        else
        {
            Debug.LogError("No Button component found on " + gameObject.name);
        }
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        
        #if UNITY_EDITOR
            // If running in the Unity Editor, stop playing
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If running as a build, quit the application
            Application.Quit();
        #endif
    }
    
    void OnDestroy()
    {
        // Clean up listener when object is destroyed
        if (quitButton != null)
        {
            quitButton.onClick.RemoveListener(QuitGame);
        }
    }
}