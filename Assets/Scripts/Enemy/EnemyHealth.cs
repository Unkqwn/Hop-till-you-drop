using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private PlayerLevel EXP;

    const float dropChance = 1f / 3f;
    public GameObject heartPickup;
    private Vector3 heartOffset;

    private void Start()
    {
        EXP = FindAnyObjectByType<PlayerLevel>();
        heartOffset = new Vector3(transform.position.x, 2, transform.position.z);
    }

    void Update()
    {
        if (health <= 0)
        {
            if (Random.Range(0f,1f) <= dropChance)
            {
                GameObject hp = Instantiate(heartPickup, heartOffset, Quaternion.identity);

            }

            if(gameObject.tag == "Boss")
            {
                finalBlow();
            }


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

    private void finalBlow()
    {
        GetComponent<BossEnemyScript>().SpawnProjectiles(25);
    }

    
}