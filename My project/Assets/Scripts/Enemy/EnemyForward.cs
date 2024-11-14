using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    private float speed = 2f;
    private float topBoundary, bottomBoundary;
    private float moveDirection;

    void Start()
    {
        topBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1f;
        bottomBoundary = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1f;
        
        float leftBoundary = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f;
        float rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f;
        float spawnX = Random.Range(leftBoundary, rightBoundary);

        transform.position = new Vector3(
            spawnX,
            Random.value < 0.5f ? topBoundary : bottomBoundary,
            transform.position.z
        );

        moveDirection = transform.position.y > 0 ? -1f : 1f;
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * moveDirection * Time.deltaTime);
        
        if (transform.position.y <= bottomBoundary || transform.position.y >= topBoundary)
        {
            moveDirection *= -1;
        }
    }
}
