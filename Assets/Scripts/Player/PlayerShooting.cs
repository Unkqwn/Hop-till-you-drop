using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private WeaponStats weapon;
    private int ammoCount;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool isPC;
    private Vector2 mouseLook, joystickLook;
    private Vector3 rotationTarget;

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
        ammoCount = weapon.maxAmmo;
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
        if (context.started)
        {
            Debug.Log("Action has started");
        }
        else if (context.performed)
        {
            if (isPC)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    GameObject projectile = Instantiate(weapon.prefab, transform.position, transform.rotation);
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();
                    Bullet bullet = projectile.GetComponent<Bullet>();
                    projectile.layer = LayerMask.NameToLayer("P_bullet");
                    bullet.bulletDamage = weapon.damage;
                    rb.AddForce(transform.forward * weapon.bulletSpeed, ForceMode.Impulse);
                    Destroy(projectile, 5f);
                    ammoCount--;
                }
            }
            else
            {
                if (Gamepad.current.rightTrigger.wasPressedThisFrame)
                {
                    GameObject projectile = Instantiate(weapon.prefab, transform.position, transform.rotation);
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();
                    Bullet bullet = projectile.GetComponent<Bullet>();
                    projectile.layer = LayerMask.NameToLayer("P_bullet");
                    bullet.bulletDamage = weapon.damage;
                    rb.AddForce(transform.forward * weapon.bulletSpeed, ForceMode.Impulse);
                    Destroy(projectile, 5f);
                    ammoCount--;
                }
            }
        }
        else if (context.canceled)
        {
            Debug.Log("Action was cancelled");
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ammoCount = weapon.maxAmmo;
        }
    }
}