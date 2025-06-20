using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("follow", 2);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void follow()
    {
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        transform.GetComponent<Rigidbody>().velocity = dir * 10;
    }
}
