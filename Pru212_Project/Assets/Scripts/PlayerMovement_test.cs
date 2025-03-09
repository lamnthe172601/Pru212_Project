using UnityEngine;

public class PlayerMovement_test : MonoBehaviour
{
    public Vector2 startPosition;
    Rigidbody2D rb;
    public float moveSpeed = 20f;
    SpriteRenderer rbSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = rb.GetComponent<SpriteRenderer>();
        //rb.useFullKinematicContacts = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    rb.AddForce(new Vector2(-10, 0), ForceMode2D.Force);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    rb.AddForce(new Vector2(10, 0), ForceMode2D.Force);
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    rb.AddForce(new Vector2(0, 10), ForceMode2D.Force);
        //}        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    rb.AddForce(new Vector2(0, -10), ForceMode2D.Force);
        //}
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement vector
        Vector2 movement = new Vector2(horizontal, vertical) * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rbSprite.flipY = false;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            rbSprite.flipY = true;
        }
        rb.MovePosition(rb.position + movement);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Enemy"))
    //    {
    //        //get the direction of the collision
    //        if (collision.collider.CompareTag("Player"))
    //        {
    //            //get the direction of the collision
    //            //Vector2 direction = collision.GetContact(0).normal;
    //            //if (direction.x == 1) Destroy(gameObject);
    //        }
    //    }
    //}
}
