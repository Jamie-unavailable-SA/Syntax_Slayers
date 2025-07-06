using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameoverpanel;
    public GameObject player;
    public GameObject score;
    public AudioSource audiosource;
    public AudioClip gameoversound;
    public WordTimer wordtimer;
    public static bool gobgmusicstopper = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("word"))
        {
            // Disable game objects
            player.SetActive(false);
            score.SetActive(false);

            // Stop background music
            gobgmusicstopper = true;

            // Play game over sound
            if (audiosource != null && gameoversound != null)
            {
                audiosource.playOnAwake = false;
                audiosource.loop = false;
                audiosource.clip = gameoversound;
                audiosource.Play(); // ✅ THIS LINE PLAYS THE SOUND
            }
            else
            {
                Debug.LogWarning("AudioSource or GameOverSound is missing!", this);
            }

            // Show game over panel
            gameoverpanel.SetActive(true);

            // Stop spawning new words
            if (wordtimer) wordtimer.enabled = false;

            // Destroy all words currently in scene
            foreach (GameObject w in GameObject.FindGameObjectsWithTag("word"))
            {
                Destroy(w);
            }
        }
        else
        {
            Debug.Log("Tag not hit/collider error", this);
        }
    }
}
