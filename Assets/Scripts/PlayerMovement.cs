using UnityEngine;

/// <summary>
/// Keeps the player locked to the mouse's vertical position.
/// Attach this to the player.  Works in 2D or 3D.
/// </summary>
public class PalyerMovement : MonoBehaviour
{
    [Header("Basic settings")]
    [Tooltip("Camera that watches the scene (leave empty to use Camera.main)")]
    [SerializeField] private Camera cam;

    [Tooltip("How fast the player chases the mouse (units / second)")]
    [SerializeField] private float followSpeed = 10f;

    [Header("Soft limits (world units)")]
    [Tooltip("Lowest Y value the player can reach")]
    [SerializeField] private float minY = -4f;

    [Tooltip("Highest Y value the player can reach")]
    [SerializeField] private float maxY =  4f;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;   // fallback
    }

    private void Update()
    {
        // 1. Convert mouse position to world space
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);

        // 2. Pick current position but swap in the mouse's Y
        Vector3 target = transform.position;
        target.y = Mathf.Clamp(mouseWorld.y, minY, maxY);

        // 3. Smoothly move toward that Y
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            followSpeed * Time.deltaTime);
    }
}
