using UnityEngine;

public class CollideRestart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("word"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
        }
    }
}
