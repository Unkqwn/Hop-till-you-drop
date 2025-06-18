using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSplit : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public int numberOfProjectiles;
    public int splitTime;

    private float radius = 5f;
    public static Vector3 startPoint;
    private float startHeight = 1;

    void Start()
    {
        
        Invoke("Wait", splitTime);
        
    }

    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, startHeight, transform.position.z);
    }

    private void Wait()
    {
        SpawnProjectiles(numberOfProjectiles);
    }

    private void SpawnProjectiles(int _numberOfProjectiles)
    {
        float angleStep = 360f / _numberOfProjectiles;
        float angle = 0f;

      

        for (int i = 0; i < _numberOfProjectiles; i++)
        {
           
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.z + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, 0, projectileDirYPosition);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

            GameObject tmpObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.z);
            


            tmpObj.gameObject.layer = LayerMask.NameToLayer("E_bullet");

            angle += angleStep;
            Destroy(tmpObj,10);
            

        }
        Destroy(gameObject);
    }


}
