using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().health++;
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 10)
        {
            this.transform.LookAt(player.transform);
            this.GetComponent<Rigidbody>().AddForce(this.transform.forward * 10);

        }

    }
}
