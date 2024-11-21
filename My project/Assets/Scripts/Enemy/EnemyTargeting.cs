using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    // Deklarasi variabel untuk menyimpan referensi ke pemain, kecepatan musuh, dan batas layar
    private Transform player;
    private float speed = 2f;
    private float minX, maxX, minY, maxY;

    // Fungsi Start dipanggil sekali saat permainan dimulai
    void Start()
    {
        // Mencari objek dengan tag "Player" dan mendapatkan komponen Transform-nya
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Mengatur batas minimum dan maksimum untuk koordinat X dan Y berdasarkan tampilan layar
        minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f;
        minY = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1f;
        maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1f;

        // Menentukan posisi spawn musuh secara acak pada batas layar (minX atau maxX untuk X, acak di antara minY dan maxY untuk Y)
        float spawnX = Random.value < 0.5f ? minX : maxX;
        float spawnY = Random.Range(minY, maxY);
        transform.position = new Vector3(spawnX, spawnY, transform.position.z);
    }

    // Fungsi Update dipanggil setiap frame
    void Update()
    {
        // Jika pemain ditemukan, musuh akan bergerak menuju pemain
        if (player != null)
        {
            // Menghitung arah dari posisi musuh ke posisi pemain dan menormalkannya
            Vector3 direction = (player.position - transform.position).normalized;

            // Menggerakkan musuh menuju pemain dengan kecepatan tertentu
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    // Fungsi ini dipanggil ketika musuh bertabrakan dengan objek lain
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jika musuh bertabrakan dengan objek yang memiliki tag "Player", hancurkan objek musuh
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
