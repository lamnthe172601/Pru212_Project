﻿using UnityEngine;

public class BossController : MonoBehaviour
{
    [Space(10)]
    // Rigidbody component for physics operations
    public Rigidbody2D rigidbody2d;
    // SpriteRenderer component to manipulate the boss sprite
    public SpriteRenderer spriteRenderer;
    // Prefab for the death effect to instantiate upon boss death
    public GameObject deathEffect;
    // Target player to follow or aim at
    private Transform target;

    [Space(10)]
    // Boss attributes for health, damage, and movement speed
    public float health = 10000;
    public float damage = 100;
    public float moveSpeed = 2f;

    [Space(10)]
    // Timers for handling knockback and hit reactions
    public float knockBackTime = 0.5f;
    public float hitWaitTime = 0.5f;
    private float knockBackCounter;
    private float hitCounter;

    [Space(10)]
    // Animator for controlling boss animations
    private Animator animator;
    // Timers to control different phases of the boss's behavior
    public float timer;
    public float secondTimer;
    public float thirdTimer;
    // Last known position of the player, used for AI behavior
    private Vector3 playerLastPosition;
    private EnemyHealthController enemyHealthController;

    [SerializeField]
    private float time_appears;


    void Start()
    {
        // Initialize components and find the player at the start
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = FindObjectOfType<PlayerMove>().transform;
        animator = GetComponentInChildren<Animator>();
        // Set initial values for timers and record the player's position
        playerLastPosition = target.position;
        // Initialize timers and player position
        timer = 5f;
        secondTimer = 1f;
        thirdTimer = 2f;
        
        enemyHealthController = GetComponent<EnemyHealthController>();
        enemyHealthController.enabled = true;
        enemyHealthController.Hp = health;       

        gameObject.SetActive(false); // Ẩn boss ngay từ đầu
        Invoke("EnableBoss", time_appears); // Gọi hàm sau 5 phút

    }
    void EnableBoss()
    {
        gameObject.SetActive(true); // Kích hoạt boss
    }



    void Update()
    {
        // Check and handle knockback effect
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            // Reverse and increase move speed during knockback
            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            // Reset move speed once knockback is over
            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * 0.5f);
            }
        }

        // Perform main boss behavior patterns
        BehaviorBoss();
        // Ensure boss sprite faces the player
        FlipTowardsPlayer();

        // Handle timing for post-hit invulnerability or reaction delay
        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        // Reduce health
        health -= damageToTake;
        enemyHealthController.TakeDame(damageToTake);

        // If health is depleted
        if (health <= 0)
        {
            // Spawn death effect
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 3f); // Ensure effect is destroyed after 3 seconds

            CameraShake.instance.ShakeIt(1f, 0.2f);

            // Delay boss destruction slightly to ensure effect spawns properly
            Destroy(gameObject, 0.1f);
        }
    }


    // Overloaded method to take damage with optional knockback
    //public void TakeDamage(float damageToTake, bool shouldKnockBack)
    //{
    //    // Invoke damage taking and apply knockback if specified
    //    TakeDamage(damageToTake);

    //    if (shouldKnockBack == true)
    //    {
    //        knockBackCounter = knockBackTime;
    //        hitCounter = hitWaitTime;
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deal damage to the player upon collision
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.TakeDame(damage);

            hitCounter = hitWaitTime;
        }
    }

    private void FlipTowardsPlayer()
    {
        // Flip the boss sprite to face the player based on their relative positions
        if (transform.position.x < target.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x > target.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void StopBoss()
    {
        // Stop the boss's movement by setting its velocity to zero
        rigidbody2d.linearVelocity = Vector2.zero;
    }

    private void BehaviorBoss()
    {
        // Control the boss's behavior with a sequence of timed actions
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            // Sequence of actions triggered by timers
            StopBoss();
            animator.SetBool("isRunning", false);
            secondTimer -= Time.deltaTime;
            if (secondTimer <= 0)
            {
                animator.SetBool("isRunning", true);
                Vector3 playerPosition = playerLastPosition;
                moveSpeed = 8f;
                Vector3 direction = playerPosition - transform.position;
                direction.Normalize();
                transform.position += direction * moveSpeed * Time.deltaTime;
                thirdTimer -= Time.deltaTime;
                if (thirdTimer <= 0)
                {
                    moveSpeed = 2f;
                    timer = 5f;
                    secondTimer = 1f;
                    thirdTimer = 2f;
                }
            }
        }
        else
        {
            if (playerLastPosition != target.position)
            {
                playerLastPosition = target.position;
            }
            rigidbody2d.linearVelocity = (target.position - transform.position).normalized * moveSpeed;
        }
    }
}
