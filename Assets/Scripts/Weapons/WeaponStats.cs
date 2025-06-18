using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum weaponType
{
    bubblegun = 0,
    waterBalloon
}

[CreateAssetMenu(menuName = "Weapon/Weapon Stats")]

public class WeaponStats : ScriptableObject
{
    public weaponType weapon;

    public GameObject prefab;
    public int maxMagazine;
    public float damage;
    public float bulletSpeed;
    public float fireRate;
}