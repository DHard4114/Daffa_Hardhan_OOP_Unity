using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    // Kecepatan gerakan musuh
    private float speed = 2f;
    
    // Batas atas dan bawah layar
    private float topBoundary, bottomBoundary;
    
    // Arah gerakan musuh (1 untuk ke bawah, -1 untuk ke atas)
    private float moveDirection;

    // Fungsi Start dipanggil sekali saat permainan dimulai
    void Start()
    {
        // Mengatur batas atas layar menggunakan ViewportToWorldPoint (dikurangi 1 untuk margin)
        topBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1f;
        
        // Mengatur batas bawah layar menggunakan ViewportToWorldPoint (ditambah 1 untuk margin)
        bottomBoundary = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1f;
        
        // Mengatur batas kiri layar dengan margin
        float leftBoundary = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f;
        
        // Mengatur batas kanan layar dengan margin
        float rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f;
        
        // Menentukan posisi spawn X secara acak antara batas kiri dan kanan layar
        float spawnX = Random.Range(leftBoundary, rightBoundary);

        // Menentukan posisi spawn musuh di batas atas atau bawah layar secara acak
        transform.position = new Vector3(
            spawnX, // Posisi X di antara batas kiri dan kanan
            Random.value < 0.5f ? topBoundary : bottomBoundary, // Posisi Y di batas atas atau bawah
            transform.position.z // Menjaga posisi Z tetap sama
        );

        // Menentukan arah gerakan berdasarkan posisi spawn Y; jika di atas, bergerak ke bawah, dan sebaliknya
        moveDirection = transform.position.y > 0 ? -1f : 1f;
    }

    // Fungsi Update dipanggil setiap frame
    void Update()
    {
        // Menggerakkan musuh ke atas atau ke bawah berdasarkan kecepatan dan arah gerakan
        transform.Translate(Vector3.up * speed * moveDirection * Time.deltaTime);
        
        // Jika musuh mencapai batas atas atau bawah, balik arah gerakan
        if (transform.position.y <= bottomBoundary || transform.position.y >= topBoundary)
        {
            moveDirection *= -1; // Mengganti arah gerakan
        }
    }
}
