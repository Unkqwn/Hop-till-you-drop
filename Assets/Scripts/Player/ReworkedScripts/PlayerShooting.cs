using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public WeaponStats weapon;

    [SerializeField] private GameObject projectile;

    private int ammoCount;
    private bool canShoot = true;
    private float firerate;

    private void Start()
    {
        projectile = weapon.prefab;

        ammoCount = weapon.maxMagazine;
        firerate = weapon.fireRate;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.isGamePaused && canShoot)
        {
            if (ammoCount > 0)
            {
                canShoot = false;
                ammoCount--;
                SpawnProjectile();
                Invoke(nameof(ResetShoot), 1f/firerate);
            }
        }
    }

    private void SpawnProjectile()
    {
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        PlayerBullet pBullet = bullet.GetComponent<PlayerBullet>();
        if (pBullet == null)
        {
            pBullet = projectile.AddComponent<PlayerBullet>();
        }
        pBullet.damage = weapon.damage;
        pBullet.speed = weapon.bulletSpeed;
        Destroy(bullet, 5f);
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
}