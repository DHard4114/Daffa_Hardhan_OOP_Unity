using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private float speed = 2f;
    private float leftBoundary, rightBoundary;
    private float moveDirection;

    void Start()
    {
        leftBoundary = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f;
        rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f;
        float bottomBoundary = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1f;
        float topBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1f;
        float spawnY = Random.Range(bottomBoundary, topBoundary);
        transform.position = new Vector3(
            Random.value < 0.5f ? leftBoundary : rightBoundary,
            spawnY,
            transform.position.z
        );
        moveDirection = transform.position.x < 0 ? 1f : -1f;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * moveDirection * Time.deltaTime);
        if (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary)
        {
            moveDirection *= -1;
        }
    }
}
