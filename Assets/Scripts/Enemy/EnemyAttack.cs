using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [Header("Projectile Settings")]
    public int numberOfProjectiles;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public GameObject actualSpawnPoint;

    [Header("Private Variables")]
    private Vector3 startPoint;
    private const float radius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startPoint = actualSpawnPoint.transform.position;
            SpawnProjectiles(numberOfProjectiles);
        }

    }

    private void SpawnProjectiles(int _numberOfProjectiles)
    {
        float angleStep = 360f / _numberOfProjectiles;
        float angle = 1f;

        for (int i = 0; i < _numberOfProjectiles; i++) 
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float projectileDirYPosition = startPoint.z + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 projectileVector = new Vector3(projectileDirXPosition, 0, projectileDirYPosition);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

            GameObject tmpObj = Instantiate(projectilePrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.y);

            tmpObj.gameObject.layer = LayerMask.NameToLayer("E_bullet");

            angle += angleStep;
        }
    }
}
