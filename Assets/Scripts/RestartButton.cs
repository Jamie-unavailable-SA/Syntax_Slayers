using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverPanel;

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
