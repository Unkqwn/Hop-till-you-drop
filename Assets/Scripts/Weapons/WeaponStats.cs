using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    public float damage;
    public float bulletSpeed;
    public GameObject prefab;
    public int maxAmmo;
}
