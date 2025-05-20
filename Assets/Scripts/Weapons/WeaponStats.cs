using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    public GameObject prefab;
    public int maxAmmo;
    public float damage;
    public float bulletSpeed;
    public float fireRate;
}