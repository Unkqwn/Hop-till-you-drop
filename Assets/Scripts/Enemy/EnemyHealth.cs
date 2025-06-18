using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private PlayerLevel EXP;

    private void Start()
    {
        EXP = FindAnyObjectByType<PlayerLevel>();
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            EXP.score++;
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