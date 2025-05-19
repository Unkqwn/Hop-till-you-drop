using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private WeaponStats weapon;
    private int ammoCount;

    private void Start()
    {
        ammoCount = weapon.maxAmmo;
    }

    void Update()
    {
        LookAtCursor();
        Shoot();
        Reload();
    }

    private void Shoot()
    {
            if (Input.GetKeyDown(KeyCode.Mouse0) && ammoCount >= 1)
            {
                GameObject bullet = Instantiate(weapon.prefab, transform.position, transform.rotation);
                bullet.layer = LayerMask.NameToLayer("P_bullet");
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * weapon.bulletSpeed, ForceMode.Impulse);
                Destroy(bullet, 5f);
                ammoCount--;
            }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ammoCount = weapon.maxAmmo;
        }
    }

    private void LookAtCursor()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.yellow);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}