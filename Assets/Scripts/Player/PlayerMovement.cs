using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 move;
    private Vector3 movement;

    private bool canDash = true;
    [SerializeField] private float dashSpeed;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    void Update()
    {
        movePlayer();
    }

    public void movePlayer()
    {
        movement = new Vector3(move.x, 0, move.y);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            transform.Translate(Vector3.forward * dashSpeed);
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
    }
}
