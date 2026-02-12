using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    public float BaseSpeed => baseSpeed;

    private Rigidbody rb;
    private Vector2 moveInput;

    private MovementModule[] modules;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        modules = GetComponents<MovementModule>();

        rb.freezeRotation = true;

        foreach (var module in modules)
        {
            module.Initialize(this, rb);
        }
    }

    private void FixedUpdate()
    {
        foreach (var module in modules)
            module.HandleMovement(moveInput);
    }
}
