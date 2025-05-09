using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 move = forward * Time.deltaTime * speed;
        this.transform.Translate(move, Space.World);
    }
}
