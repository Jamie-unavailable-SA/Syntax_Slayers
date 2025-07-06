using UnityEngine;

/// <summary>
/// Simple homing bullet: flies toward the closest object tagged "Word",
/// destroys that object on contact, then destroys itself.
/// Attach to the Bullet prefab (must have a Collider2D set as Trigger
/// and optionally a Rigidbody2D set to Kinematic).
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class MoveToWord : MonoBehaviour
{
    [SerializeField] private float speed        = 12f;   // units per second
    [SerializeField] private float seekInterval = 0.25f; // how often to reacquire target
    [SerializeField] private float lifetime     = 5f;    // auto‑destruct failsafe

    private Transform target;
    private float seekTimer;

    private void Start()
    {
        AcquireTarget();
        Destroy(gameObject, lifetime);          // bullet won’t linger forever
    }

    private void Update()
    {
        //------------------------------------------------------------------
        // 1. Periodically reacquire in case the word was destroyed mid‑flight
        //------------------------------------------------------------------
        seekTimer += Time.deltaTime;
        if (seekTimer >= seekInterval)
        {
            seekTimer = 0f;
            if (target == null) AcquireTarget();
        }

        //------------------------------------------------------------------
        // 2. Move toward the current target (if any)
        //------------------------------------------------------------------
        if (target != null)
        {
            Vector3 destination = target.position;   // you can offset if desired
            transform.position = Vector3.MoveTowards(
                transform.position,
                destination,
                speed * Time.deltaTime
            );
        }
    }

    //----------------------------------------------------------------------
    // 3. Collision – destroy both objects and play impact sfx if you want
    //----------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Word")) return;

        Destroy(other.gameObject); // or SetActive(false) if you’re pooling
        Destroy(gameObject);       // <-- bullet disappears immediately
    }

    //----------------------------------------------------------------------
    // Helper – grab any active word in the scene
    //----------------------------------------------------------------------
    private void AcquireTarget()
    {
        GameObject wordObj = GameObject.FindGameObjectWithTag("word");
        target = wordObj ? wordObj.transform : null;
    }
}
