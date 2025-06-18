using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stayOnGround : MonoBehaviour
{

    private float startHeight = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, startHeight, transform.position.z);
    }
}
