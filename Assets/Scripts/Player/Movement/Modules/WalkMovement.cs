using UnityEngine;

public class WalkMovement : MovementModule
{
    [SerializeField] private float acceleration = 15f;

    public override void HandleMovement(Vector2 moveInput)
    {
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        Vector3 targetVelocity = direction * controller.BaseSpeed;

        Vector3 currentVelocity = rb.velocity;
        Vector3 velocityChange = Vector3.Lerp(new Vector3(currentVelocity.x, 0f, currentVelocity.z), targetVelocity,
            acceleration * Time.deltaTime);

        rb.velocity = new Vector3(velocityChange.x, rb.velocity.y, velocityChange.z);
    }
}