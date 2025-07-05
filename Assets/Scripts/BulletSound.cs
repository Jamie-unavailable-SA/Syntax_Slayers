using UnityEngine;
using UnityEngine.UI;

public class BulletSound : MonoBehaviour
{
    [Header("Sound")]
    public AudioClip Bulletsound;
    public AudioSource source;
    public float volume = 1f;

    public void Start()
    {
        source = GetComponent<AudioSource>();
        if (Bulletsound != null)
        source.playOnAwake = true;
        source.volume = volume;
    }
}
