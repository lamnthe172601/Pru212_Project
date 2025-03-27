using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    public int xpAmount = 5;   
    

    private void Start()
    {
      Destroy(gameObject,30);
    }

    private void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerExperience playerXP = other.GetComponent<PlayerExperience>();
            if (playerXP != null)
            {
                playerXP.GainXP(xpAmount);
                Destroy(gameObject);
            }
        }
    }
}
