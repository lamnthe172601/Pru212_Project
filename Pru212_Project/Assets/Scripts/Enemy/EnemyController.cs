using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyController : MonoBehaviour
{
    [Space(10)]
    public Rigidbody2D rigidbody2d;
    public SpriteRenderer spriteRenderer;

    [Space(10)]
    // Enemies attributes for health, damage, and movement speed
    public float health = 5f;
    public float moveSpeed = 2f;
    public float damage = 5f;

    [Space(10)]
    // Time before the enemy can hit again
    public float hitWaitTime = 0.5f;
    // Duration of the knockback effect when hit
    public float knockBackTime = .5f;

    [Space(10)]
    // Amount of experience given to the player upon defeat
    public int experienceToGive = 1;
    // Probability of dropping a coin upon death
    public float coinDropRate = 0.5f;
    // Probability of dropping a chest upon death
    private float chestDropRate = 0.001f;

    // Timer to manage hit frequency
    private float hitCounter;
    // Timer to manage knockback duration
    private float knockBackCounter;
    // The target the enemy will pursue
    private Transform target;

    private bool isDefeated = false;
    public int xpDrop = 5;
    public GameObject ExpPrefab;
    public GameObject healthPickupPrefab;

    private EnemyHealthController enemyHealthController;

    public GameObject deathEffect;
    public float deathEffectDelay = 1f; // Thời gian hiệu ứng tồn tại trước khi bị hủy

    [SerializeField] private AudioClip audioSource;

    void Start()
    {
        // Find the player in the scene and set it as the target
        target = FindObjectOfType<PlayerMove>().transform;
        enemyHealthController = GetComponent<EnemyHealthController>();
        enemyHealthController.Hp = health;        
    }

    void Update()
    {
        // Handle knockback effect
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * 0.5f);
            }
        }

        rigidbody2d.linearVelocity = (target.position - transform.position).normalized * moveSpeed;

        FlipTowardsPlayer();

        // Reset hit counter after an attack
        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void FlipTowardsPlayer()
    {
        // Flip the sprite depending on the relative position of the target
        if (transform.position.x < target.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x > target.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }

    private Coroutine attackCoroutine; // Lưu trữ Coroutine tấn công

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Bắt đầu tấn công liên tục nếu chưa có Coroutine đang chạy
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackPlayer(collision.gameObject));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Dừng tấn công khi Enemy không còn chạm vào Player
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    private IEnumerator AttackPlayer(GameObject player)
    {
        while (player != null) 
        {
            PlayerHealthController.instance.TakeDame(damage);
            yield return new WaitForSeconds(1f); // Gây sát thương mỗi 1 giây
        }
    }


    public void TakeDamage(float damageToTake)
    {
        if (isDefeated)
            return;

        enemyHealthController.TakeDame(damageToTake);
        health -= damageToTake;
        if (health <= 0)
        {
            isDefeated = true;

            GameObject soundObject = new GameObject("DeathSound");
            AudioSource source = soundObject.AddComponent<AudioSource>();
            source.clip = audioSource;
            source.pitch = 2.5f;
            source.Play();
            Destroy(soundObject, source.clip.length);
            
            if (deathEffect != null)
            {
                GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(effect, deathEffectDelay); // Hủy hiệu ứng sau một khoảng thời gian
            }

            Destroy(gameObject);
            DropLoot(); 
       
            
        }

    }

    private void DropLoot()
    {
        float roll = Random.value; 

        if (roll < 0.1f) 
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }
        else if (roll < 0.4f) 
        {
            Instantiate(ExpPrefab, transform.position, Quaternion.identity);
        }
    }


}
