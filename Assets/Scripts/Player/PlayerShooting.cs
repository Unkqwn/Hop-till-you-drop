using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool isPC;
    private Vector2 mouseLook, joystickLook;
    private Vector3 rotationTarget;

    public WeaponStats weapon;

    public int ammoCount;
    private bool canShoot = true;
    private int ammoMagCount;
    private float firerate;

    public bool isPaused;

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }
    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        joystickLook = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        ammoMagCount = weapon.maxMagazine;
        firerate = weapon.fireRate;
    }

    private void Update()
    {
        if (Gamepad.all.Count > 0)
        {
            isPC = false;
        }
        else
        {
            isPC = true;
        }

        if (!isPaused)
        {
            if (isPC)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(mouseLook);

                if (Physics.Raycast(ray, out hit))
                {
                    rotationTarget = hit.point;
                }

                playerAim();
            }
            else
            {
                playerAim();
            }
        }
    }

    public void playerAim()
    {
        if (isPC)
        {
            var lookPos = rotationTarget - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(joystickLook.x, 0f, joystickLook.y);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), rotateSpeed);
            }
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && !isPaused && canShoot)
        {
            if (ammoMagCount > 0)
            {
                canShoot = false;
                ammoMagCount--;
                if (isPC)
                {
                    GameObject projectile = Instantiate(weapon.prefab, transform.position, transform.rotation * Quaternion.Euler(0,90,0));
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();
                    Weapon bullet = projectile.GetComponent<Weapon>();
                    projectile.layer = LayerMask.NameToLayer("P_bullet");
                    bullet.damage = weapon.damage;
                        rb.AddForce(transform.forward * weapon.bulletSpeed, ForceMode.Impulse);
                        Destroy(projectile, 5f);
                }
                else
                {
                    GameObject projectile = Instantiate(weapon.prefab, transform.position, transform.rotation * Quaternion.Euler(0, 90, 0));
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();
                    Weapon bullet = projectile.GetComponent<Weapon>();
                    projectile.layer = LayerMask.NameToLayer("P_bullet");
                    bullet.damage = weapon.damage;
                    rb.AddForce(transform.forward * weapon.bulletSpeed, ForceMode.Impulse);
                    Destroy(projectile, 5f);
                }
                Invoke(nameof(ResetShoot), firerate);
            }
            else
            {
                Reload(context);
            }
        }
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