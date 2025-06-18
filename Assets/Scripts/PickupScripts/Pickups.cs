using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public static int currentPointsAmount;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Debug.Log("coin amount: " + currentPointsAmount);
    }
}
