using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaEnemySpawner : MonoBehaviour
{


    float minDistance = 5.0f;
    float maxDistance = 20.0f;

    GameObject player;
    float distance;

    Vector3 spawnPosition;



    void Start()
    {
        spawnPosition = player.transform.position;
        distance = Random.Range(minDistance, maxDistance);
        
    }

   
    void Update()
    {
        float angle = Random.Range(-Mathf.PI, Mathf.PI);
        spawnPosition += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;

        Vector3 pos = new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f).normalized * Random.Range(20, 50);

    }
}
