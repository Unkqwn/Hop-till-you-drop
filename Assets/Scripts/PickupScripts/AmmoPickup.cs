using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
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
            other.gameObject.GetComponent<PlayerShooting>().ammoCount++;
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
