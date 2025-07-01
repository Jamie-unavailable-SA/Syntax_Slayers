using UnityEngine;

public class CollideRestart : MonoBehaviour
{
    public GameObject player;
    public GameObject GameOverPanel;
    public GameObject score;
    public AudioClip gameOverSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("word"))
        {
            // Disable player and show Game Over panel
            player.SetActive(false);
            GameOverPanel.SetActive(true);
            score.SetActive(false);

            //play game over sound
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && gameOverSound != null)
            {
                audioSource.PlayOneShot(gameOverSound);
            }
            else
            {
                Debug.LogWarning("AudioSource or gameOverSound is not set.");
            }

            // Optionally, you can reset the game state or perform other actions here
            Debug.Log("Game Over! You collided with a word.");
        }
    }
}
