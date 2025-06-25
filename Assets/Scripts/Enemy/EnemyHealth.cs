using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private PlayerLevel EXP;

    const float dropChance = 1f / 3f;
    const float ammoDropChance = 1f / 2f;
    public GameObject heartPickup;
    public GameObject ammoPickup;
    private Vector3 ammoOffset;
    private Vector3 heartOffset;
    private GameObject player;
    public LayerMask layerMask;
    public GameObject bloodstain;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EXP = FindAnyObjectByType<PlayerLevel>();
        
        
    
}

    void Update()
    {
        if (health <= 0)
        {
            if (Random.Range(0f,1f) <= dropChance)
            {
                Vector3 heartOffset = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
                GameObject hp = Instantiate(heartPickup, heartOffset, Quaternion.identity);

            }

            if (Random.Range(0f, 1f) <= ammoDropChance)
            {
                Vector3 ammoOffset = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
                GameObject ap = Instantiate(ammoPickup, ammoOffset, Quaternion.identity);

            }

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))

            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }

            Vector3 bloodPos = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            GameObject blood = Instantiate(bloodstain, bloodPos, Quaternion.identity);

            if (gameObject.tag == "Boss")
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