using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public float magnetChance = 0f; // Xác suất hút Exp & Máu

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = playerInput.normalized * moveSpeed;

        if (playerInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (playerInput != Vector2.zero)
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }
    }

    // Tăng tốc độ chạy
    public void IncreaseMoveSpeed(float speedUp)
    {
        moveSpeed += speedUp;
       
    }

    // Kích hoạt hút Exp & Máu (xác suất 10%)
    public void ActivateMagnetChance()
    {
        if (magnetChance >= 1) 
        {
         
            GameObject[] expAndHealthItems = GameObject.FindGameObjectsWithTag("Collectible");
            foreach (GameObject item in expAndHealthItems)
            {
                item.transform.position = transform.position; // Hút về vị trí người chơi
            }
            magnetChance = 0;
        }
    }
}
