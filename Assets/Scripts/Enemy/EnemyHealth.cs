using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            float damage = other.GetComponent<Weapon>().damage;
            health -= damage;
        }
    }
}