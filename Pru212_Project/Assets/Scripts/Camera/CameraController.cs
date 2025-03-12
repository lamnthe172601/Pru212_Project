using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the target transform (player)
    private Transform target;

    private void Awake()
    {
        // Find the GameObject with the "Player" tag and get its transform
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure the player GameObject has the 'Player' tag.");
        }
    }

    private void FixedUpdate()
    {
        // Follow the target if it's found
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
