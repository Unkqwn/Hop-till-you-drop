using UnityEngine;

public abstract class MovementModule : MonoBehaviour
{
    protected Rigidbody rb;
    protected PlayerMovement controller;

    public virtual void Initialize(PlayerMovement controller, Rigidbody rigidbody)
    {
        this.controller = controller;
        this.rb = rigidbody;
    }

    public abstract void HandleMovement(Vector2 moveInput);
}