using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    // Kecepatan gerakan musuh
    private float speed = 2f;
    
    // Batas kiri dan kanan pada layar
    private float leftBoundary, rightBoundary;
    
    // Arah gerakan musuh (1 untuk ke kanan, -1 untuk ke kiri)
    private float moveDirection;

    // Fungsi Start dipanggil sekali saat permainan dimulai
    void Start()
    {
        // Mengatur batas kiri pada layar menggunakan ViewportToWorldPoint (ditambah 1 untuk margin)
        leftBoundary = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f;
        
        // Mengatur batas kanan pada layar menggunakan ViewportToWorldPoint (dikurangi 1 untuk margin)
        rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f;
        
        // Mengatur batas bawah layar menggunakan ViewportToWorldPoint (ditambah 1 untuk margin)
        float bottomBoundary = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1f;
        
        // Mengatur batas atas layar menggunakan ViewportToWorldPoint (dikurangi 1 untuk margin)
        float topBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1f;
        
        // Menentukan posisi spawn Y secara acak antara batas bawah dan atas layar
        float spawnY = Random.Range(bottomBoundary, topBoundary);
        
        // Menentukan posisi spawn X di batas kiri atau kanan layar, lalu menetapkan posisi musuh
        transform.position = new Vector3(
            Random.value < 0.5f ? leftBoundary : rightBoundary, // Posisi X di kiri atau kanan
            spawnY, // Posisi Y di antara batas atas dan bawah
            transform.position.z // Menjaga posisi Z tetap sama
        );
        
        // Menentukan arah gerakan berdasarkan posisi spawn; jika di kiri, gerak ke kanan, dan sebaliknya
        moveDirection = transform.position.x < 0 ? 1f : -1f;
    }

    // Fungsi Update dipanggil setiap frame
    void Update()
    {
        // Menggerakkan musuh ke kanan atau kiri berdasarkan kecepatan dan arah gerakan
        transform.Translate(Vector3.right * speed * moveDirection * Time.deltaTime);
        
        // Jika musuh mencapai batas kiri atau kanan, balik arah gerakan
        if (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary)
        {
            moveDirection *= -1; // Mengganti arah gerakan
        }
    }
}
