using UnityEngine;

public class PlayerBullet : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            IDamageable enemyHealth = other.GetComponent<IDamageable>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
