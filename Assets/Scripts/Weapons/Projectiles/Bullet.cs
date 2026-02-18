using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }
}
