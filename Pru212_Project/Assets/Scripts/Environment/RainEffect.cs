using UnityEngine;

public class RainEffect : MonoBehaviour
{
    public Transform playerTransform;
    public ParticleSystem rainParticleSystem;
    public float rainHeight = 10f;
    public float xOffset = 2f;
    public float followSmoothness = 0.1f;

    private Vector3 rainOffset;

    void Start()
    {
        // Set initial offset (X to the right, Y above the player)
        rainOffset = new Vector3(xOffset, rainHeight, 0);
    }

    void LateUpdate()
    {
        if (playerTransform != null && rainParticleSystem != null)
        {
            // Calculate target position with X offset
            Vector3 targetPosition = playerTransform.position + rainOffset;

            // Smoothly move the rain effect
            rainParticleSystem.transform.position = Vector3.Lerp(rainParticleSystem.transform.position, targetPosition, followSmoothness);

            // Ensure the rain is playing
            if (!rainParticleSystem.isPlaying)
            {
                rainParticleSystem.Play();
            }
        }
    }
}
