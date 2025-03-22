using UnityEngine;

public class RainEffect : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player
    public ParticleSystem rainParticleSystem; // Reference to the particle system
    public float spawnDistance = 10f; // Distance ahead of the player where the rain will generate
    public float spawnInterval = 0.2f; // Time between new rain spawn
    private float nextSpawnTime = 0f; // Tracks the time for next spawn

    void Update()
    {
        // Only spawn rain if the player moves and after a certain time interval
        if (Time.time >= nextSpawnTime)
        {
            // Calculate a new spawn position ahead of the player
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;

            // Set the rain particle system's position to the spawn position
            rainParticleSystem.transform.position = spawnPosition;

            // Play the rain particle system if it's not already playing
            if (!rainParticleSystem.isPlaying)
            {
                rainParticleSystem.Play();
            }

            // Set next spawn time
            nextSpawnTime = Time.time + spawnInterval;
        }
    }
}
