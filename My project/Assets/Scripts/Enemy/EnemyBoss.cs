using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : Enemy
{
    public float horizontalSpeed = 3f;
    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;

    private IObjectPool<Bullet> bulletPool;
    private float minX, maxX;
    private float direction = 1f;
    private float shootTimer;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            bullet => bullet.gameObject.SetActive(true),
            bullet => bullet.gameObject.SetActive(false),
            Destroy
        );
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.SetPool(bulletPool);
        return bullet;
    }

    private void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f;
        maxX = Camera.main.ViewportToWorldPoint(Vector3.right).x - 1f;
        direction = Random.value < 0.5f ? 1f : -1f;
        transform.position = new Vector3(direction > 0 ? minX : maxX, bulletSpawnPoint.position.y, transform.position.z);
    }

    private void Update()
    {
        // Gerakkan musuh secara horizontal
        transform.Translate(Vector3.right * horizontalSpeed * direction * Time.deltaTime);

        // Balik arah saat mencapai batas kiri/kanan layar
        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            direction *= -1;
        }

        // Penembakan peluru dengan interval
        if (Time.time >= shootTimer)
        {
            Shoot();
            shootTimer = Time.time + 1f; // Interval 1 detik
        }
    }

    private void Shoot()
    {
        Bullet bullet = bulletPool.Get();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.Fire(Vector2.down); // Tembak ke bawah
    }
}
