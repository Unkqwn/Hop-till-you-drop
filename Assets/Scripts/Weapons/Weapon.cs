using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour
{
    public float damage;

    private float yStart;

    private void Start()
    {
        yStart = transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, yStart, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
        }
    }
}
