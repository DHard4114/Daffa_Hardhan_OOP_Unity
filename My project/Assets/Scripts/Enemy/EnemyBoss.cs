using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : Enemy
{
    public float horizontalSpeed = 3f; // Kecepatan gerakan horizontal
    public Bullet bulletPrefab;       // Prefab peluru
    public Transform bulletSpawnPoint; // Titik spawn peluru

    private IObjectPool<Bullet> bulletPool; // Pool untuk peluru
    private float minX, maxX; // Batas gerakan horizontal
    private float direction = 1f; // Arah gerakan horizontal
    private float shootTimer; // Timer untuk interval tembakan

    public void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetFromPool,
            OnReleaseToPool,
            DestroyBullet
        );
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.SetObjectPool(bulletPool);
        return bullet;
    }

    private void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true); // Aktifkan peluru
    }

    private void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); // Nonaktifkan peluru
    }

    private void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject); // Hancurkan peluru jika pool dihancurkan
    }

    private void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f;
        maxX = Camera.main.ViewportToWorldPoint(Vector3.right).x - 1f;
        direction = Random.value < 0.5f ? 1f : -1f;
    }

    private void Update()
    {
        // Gerakan horizontal
        transform.Translate(Vector3.right * horizontalSpeed * direction * Time.deltaTime);

        // Balik arah saat mencapai batas
        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            direction *= -1;
        }

        // Penembakan peluru dengan interval
        if (Time.time >= shootTimer)
        {
            Shoot();
            shootTimer = Time.time + 1f; // Interval tembakan 1 detik
        }
    }

    private void Shoot()
    {
        Bullet bullet = bulletPool.Get();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, 180); // Arahkan ke bawah
            bullet.OnEnable(); // Gerakkan peluru
        }
    }
}
