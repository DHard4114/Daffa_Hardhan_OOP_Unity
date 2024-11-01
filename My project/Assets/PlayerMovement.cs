using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;

    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = new Vector2((2 * maxSpeed.x / timeToFullSpeed.x) * moveDirection.x, (2 * maxSpeed.y / timeToFullSpeed.y) * moveDirection.y);
        moveFriction = new Vector2((-2 * maxSpeed.x) / Mathf.Pow(timeToFullSpeed.x, 2), (-2 * maxSpeed.y) / Mathf.Pow(timeToFullSpeed.y, 2));
        stopFriction = new Vector2((-2 * maxSpeed.x) / Mathf.Pow(timeToStop.x, 2), (-2 * maxSpeed.y) / Mathf.Pow(timeToStop.y, 2));
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDirection = new Vector2(x, y);

        if (timeToFullSpeed.x > 0 && timeToFullSpeed.y > 0)
        {
            moveVelocity = new Vector2(moveDirection.x * maxSpeed.x / timeToFullSpeed.x, moveDirection.y * maxSpeed.y / timeToFullSpeed.y);
        }
        else
        {
            moveVelocity = Vector2.zero;
        }

        if (moveDirection.magnitude == 0)
        {
            moveVelocity = Vector2.MoveTowards(moveVelocity, Vector2.zero, stopFriction.magnitude * Time.deltaTime);
        }

        if (moveVelocity.magnitude < stopClamp.magnitude)
        {
            moveVelocity = Vector2.zero;
        }

        if (!float.IsNaN(moveVelocity.x) && !float.IsNaN(moveVelocity.y))
        {
            rb.velocity = moveVelocity;
        }
    }

    public Vector2 GetFriction()
    {
        return (moveDirection.magnitude > 0) ? moveFriction : stopFriction;
    }

    public void MoveBound() { }

    public bool IsMoving()
    {
        return moveVelocity.magnitude > 0;
    }
}
