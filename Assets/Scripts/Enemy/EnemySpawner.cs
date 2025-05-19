using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject BossObject;

    public  GameObject newEnemy;
    private GameObject BossEnemy;
    private SpriteRenderer rend;
    private int randomSpawnZone;
    private float randomXposition, randomYposition;
    public GameObject playerToFollow;
    public float SpawnDelay;
    public float SpawnRate;
    

    public float MinRange = 0f;
    public float MaxRange = 10f;
    public float BossSpawnRate = 5f;
    // Start is called before the first frame update
    void Start()
    {
        playerToFollow = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnNewEnemy", SpawnDelay, SpawnRate);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerToFollow.transform.position;
        
    }

    private void SpawnNewEnemy()
    {
        randomSpawnZone = Random.Range(0, 4);

        switch (randomSpawnZone)
        {
            case 0:
                randomXposition = Random.Range(transform.position.x + -11f, transform.position.x + -10f);
                randomYposition = Random.Range(transform.position.z + -8f, transform.position.z + -8f);
                break;
            case 1:
                randomXposition = Random.Range(transform.position.x + -10f, transform.position.x + 10f);
                randomYposition = Random.Range(transform.position.z + -7f, transform.position.z + -8f);
                break;
            case 2:
                randomXposition = Random.Range(transform.position.x + 10f, transform.position.x + 11f);
                randomYposition = Random.Range(transform.position.z + -8f, transform.position.z + 8f);
                break;
            case 3:
                randomXposition = Random.Range(transform.position.x + -10f, transform.position.x + 10f);
                randomYposition = Random.Range(transform.position.z + 7f, transform.position.z + 8f);
                break;
        }


        Vector3 pizza = playerToFollow.transform.position + Random.insideUnitSphere.normalized * 20;




        newEnemy = Instantiate(BossObject, pizza, Quaternion.identity);

    }

  
}
