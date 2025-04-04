using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private float bulletDamage = 10f; // Sát thương
    [SerializeField] private int bulletCount = 1; // Số tia đạn
    [SerializeField] private Text numBullet;

    private AudioClip shootSound;//âm thanh
    public AudioClip ShootSound
    {
        get => shootSound;
        set => shootSound = value;
    }
    private AudioSource audioSource;
    void Start()
    {
        currentAmmo = maxAmmo;
        numBullet.text = currentAmmo.ToString();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        RoteGun();
        Shoot();
        ReLoad();
        numBullet.text = currentAmmo.ToString();
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
            currentAmmo--;

            // Tạo đạn theo số lượng nâng cấp
            float spreadAngle = 10f; // Góc lệch giữa các viên đạn
            for (int i = 0; i < bulletCount; i++)
            {
                float offsetAngle = (i - (bulletCount - 1) / 2f) * spreadAngle;
                Quaternion bulletRotation = firePos.rotation * Quaternion.Euler(0, 0, offsetAngle);
                GameObject bullet = Instantiate(bulletPrefabs, firePos.position, bulletRotation);
                Bullet bulletScript = bullet.GetComponent<Bullet>();

                if (bulletScript != null)
                {
                    bulletScript.SetBulletSpeed(bulletSpeed);
                    bulletScript.SetBulletDamage(bulletDamage);
                }
            }
            PlaySound(ShootSound);
        }
    }

    void ReLoad()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }

    // Tăng sát thương
    public void IncreaseDamage(float dameUp)
    {
        bulletDamage += dameUp;
    }

    // Tăng số tia đạn
    public void IncreaseBulletCount(int bulletCountUp)
    {
        bulletCount += bulletCountUp;
    }
    
    //gọi âm thanh
    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, 0.3f);
        }
    }
}
