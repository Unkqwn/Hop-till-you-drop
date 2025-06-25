using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 move;
    private Vector3 movement;

    private bool canDash = true;
    private float dashCooldown;
    [SerializeField] private float dashSpeed;
    [SerializeField] private RawImage dashIcon;
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    void Update()
    {
        movePlayer();
        
        if (canDash == false)
        {
            dashCooldown += Time.deltaTime;
            if (dashCooldown >= 5)
            {
                canDash = true;
                dashCooldown = 0;

            }
            dashIcon.color = new Color32(80, 80, 80, 255);
        }
        else
        {
            dashIcon.color = new Color32(255, 255, 255, 255);
        }
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
            canDash = false;
        }
    }
}