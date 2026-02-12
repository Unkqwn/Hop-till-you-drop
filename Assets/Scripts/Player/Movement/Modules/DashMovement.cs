using UnityEngine;
using UnityEngine.InputSystem;

public class DashMovement : MovementModule
{
    [SerializeField] private float dashForce = 10f;
    private bool dashRequested;

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            dashRequested = true;
    }

    public override void HandleMovement(Vector2 moveInput)
    {
        if (!dashRequested)
        {
            return;
        }

        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        rb.AddForce(direction * dashForce * (Time.deltaTime * 100), ForceMode.Impulse);

        dashRequested = false;
    }
}