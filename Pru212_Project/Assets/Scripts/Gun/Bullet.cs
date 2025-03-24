using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private float damage;

    // Phương thức để gán tốc độ và sát thương cho viên đạn
    public void SetBulletSpeed(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    public void SetBulletDamage(float bulletDamage)
    {
        damage = bulletDamage;
    }

    void Update()
    {
        // Di chuyển viên đạn
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Xử lý va chạm và damage ở đây, ví dụ: giảm máu kẻ thù
        if (collision.gameObject.CompareTag("Enemy") )
        {
          
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);

        }
        // Xử lý va chạm và damage ở đây, ví dụ: giảm máu kẻ thù
        if (collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Hit Boss");
            collision.gameObject.GetComponent<BossController>().TakeDamage(damage);
            Destroy(gameObject);

        }
    }
}
