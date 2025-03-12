using UnityEngine;

public class MoveGun : MonoBehaviour
{
    private float roteOffset = 180f;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;

    [SerializeField] private int maxAmmo = 24;
    public int currentAmmo;

    [SerializeField] private float bulletSpeed = 10f; // Tốc độ đạn
    [SerializeField] private float bulletRange = 5f; // Tầm bắn
    [SerializeField] private float bulletDamage = 10f;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        RoteGun();
        Shoot();
        ReLoad();
    }

    void RoteGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width ||
            Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }

        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + roteOffset);

        Vector3 currentScale = transform.localScale;
        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), Mathf.Abs(currentScale.y), currentScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), -Mathf.Abs(currentScale.y), currentScale.z);
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;
            GameObject bullet = Instantiate(bulletPrefabs, firePos.position, firePos.rotation);

            // Gán tốc độ cho đạn
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = firePos.right * bulletSpeed;
            }

            // Thiết lập hủy đạn sau khi đạt tầm bắn
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetRange(bulletRange, bullet.transform.position);
            }

            currentAmmo--;
        }
    }

    void ReLoad()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }

    // Script xử lý hành vi của đạn trong cùng file
    public class Bullet : MonoBehaviour
    {
        private Vector2 startPosition;
        private float maxDistance;

        public void SetRange(float range, Vector3 startPos)
        {
            startPosition = startPos;
            maxDistance = range;
        }

        void Update()
        {
            if (Vector2.Distance(startPosition, transform.position) >= maxDistance)
            {
                Destroy(gameObject); // Hủy đạn khi đạt tầm bắn
            }
        }
    }
}
