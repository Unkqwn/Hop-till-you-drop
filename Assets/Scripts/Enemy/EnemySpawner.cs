using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject BossObject;

    public bool gameIsDone = true;

    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    public  GameObject newEnemy;


    private GameObject BossEnemy;
    private SpriteRenderer rend;
    private int randomSpawnZone;
    private float randomXposition, randomYposition;
    public GameObject playerToFollow;
    public float SpawnDelay;
    public float SpawnRate;
    

    public float MinRange;
    public float MaxRange;
    public float BossSpawnRate = 5f;
    // Start is called before the first frame update
    void Start()
    {
        playerToFollow = GameObject.FindGameObjectWithTag("Player");
        //InvokeRepeating("SpawnNewEnemy", SpawnDelay, SpawnRate);
        StartCoroutine(spawning(5));
    }

    // Update is called once per frame
    void Update()
    {
        if (playerToFollow != null)
        {
            transform.position = playerToFollow.transform.position;
        }
    }

    private IEnumerator spawning(float waitTime)
    {
        if (gameIsDone == true)
        {
            SpawnNewEnemy();
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(spawning(5));
        }
        
    }

    private void SpawnNewEnemy()
    {
        //randomSpawnZone = Random.Range(0, 4);

        //switch (randomSpawnZone)
        //{
        //    case 0:
        //        randomXposition = Random.Range(transform.position.x + -11f, transform.position.x + -10f);
        //        randomYposition = Random.Range(transform.position.z + -8f, transform.position.z + -8f);
        //        break;
        //    case 1:
        //        randomXposition = Random.Range(transform.position.x + -10f, transform.position.x + 10f);
        //        randomYposition = Random.Range(transform.position.z + -7f, transform.position.z + -8f);
        //        break;
        //    case 2:
        //        randomXposition = Random.Range(transform.position.x + 10f, transform.position.x + 11f);
        //        randomYposition = Random.Range(transform.position.z + -8f, transform.position.z + 8f);
        //        break;
        //    case 3:
        //        randomXposition = Random.Range(transform.position.x + -10f, transform.position.x + 10f);
        //        randomYposition = Random.Range(transform.position.z + 7f, transform.position.z + 8f);
        //        break;
        //}

        Vector3 offset = Random.insideUnitSphere * Random.Range(MinRange, MaxRange);
        int randomEnemy = Random.Range(0, 9);
        Vector3 pizza = playerToFollow.transform.position + (Random.insideUnitSphere * Random.Range(MinRange, MaxRange));
        pizza.y = 0;


        newEnemy = Instantiate(enemies[randomEnemy], pizza, Quaternion.identity);

    }

    public void StopSpawning()
    {
        gameIsDone = false;
    }
  
}
