using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatergunPickup : MonoBehaviour
{
    public GameObject Watagun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            Destroy(this.gameObject);
        }
    }
}
