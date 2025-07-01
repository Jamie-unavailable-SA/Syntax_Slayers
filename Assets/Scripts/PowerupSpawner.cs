using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject StopperPrefab;
    public GameObject SlowdownPrefab;

    public float spawnInterval = 10f; // Time in seconds between spawns

    private float timer;

    public float speed = 3f;
    private void Start()
    {
        timer = spawnInterval; // Initialize the timer
    }
    private void Update()
    {
        timer -= Time.deltaTime; // Decrease the timer by the time since last frame

        if (timer <= 0f)
        {
            SpawnPowerup();
            timer = spawnInterval; // Reset the timer
        }
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }

    private void SpawnPowerup()
    {
        // Randomly choose a powerup to spawn
        GameObject powerupPrefab = Random.Range(0, 2) == 0 ? StopperPrefab : SlowdownPrefab;

        // Calculate a random position within the screen bounds
        Vector2 spawnPosition = new Vector2(12, Random.Range(-3.5f, 3.5f));

        // Instantiate the powerup at the calculated position
        Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);


    }
}
