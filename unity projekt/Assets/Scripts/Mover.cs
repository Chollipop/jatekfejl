using UnityEngine;

public abstract class Mover : Fighter
{
    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input, float moveSpeed)
    {
        moveDelta = new Vector3(input.x * moveSpeed, input.y * moveSpeed, 0);

        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        transform.Translate(moveDelta * Time.deltaTime);
    }
}


