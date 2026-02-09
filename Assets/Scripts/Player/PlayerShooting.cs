using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public WeaponStats weapon;

    public int ammoCount;
    private bool canShoot = true;
    private int ammoMagCount;
    private float firerate;

    public bool isPaused;

    private void Start()
    {
        ammoMagCount = weapon.maxMagazine;
        firerate = weapon.fireRate;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && !isPaused && canShoot)
        {
            if (ammoMagCount > 0)
            {
                canShoot = false;
                ammoMagCount--;
                SpawnProjectile();
                Invoke(nameof(ResetShoot), firerate);
            }
            else
            {
                Reload(context);
            }
        }
    }

    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(weapon.prefab, transform.position, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Weapon bullet = projectile.GetComponent<Weapon>();
        bullet.damage = weapon.damage;
        rb.AddForce(transform.forward * weapon.bulletSpeed, ForceMode.Impulse);
        Destroy(projectile, 5f);
    }

    private void ResetShoot()
    {
        canShoot = true;
    }

    public void Reload(InputAction.CallbackContext context)
    {
        ammoMagCount = weapon.maxMagazine;
    }
}