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
            // Gán tốc độ cho đạn
            currentAmmo--;
            GameObject bullet = Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>(); // Lấy script Bullet gắn trên viên đạn
            if (bulletScript != null)
            {
                bulletScript.SetBulletSpeed(bulletSpeed);
                bulletScript.SetBulletDamage(bulletDamage);
            }
        }
    }
    void ReLoad()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }
}

    

    // Script xử lý hành vi của đạn trong cùng file
    

