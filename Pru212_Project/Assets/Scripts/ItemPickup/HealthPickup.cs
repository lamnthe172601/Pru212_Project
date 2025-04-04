using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 10; 
   

    private void Start()
    {
        Destroy(gameObject, 30);

    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController playerHealth = other.GetComponent<PlayerHealthController>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
