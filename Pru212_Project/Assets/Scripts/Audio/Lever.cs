using UnityEngine;

public class Lever : MonoBehaviour
{
    public AudioClip shootSound;
    public AudioClip hitSound; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.pitch = 2.0f; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaySound(shootSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            PlaySound(hitSound);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip); 
        }
    }
}
