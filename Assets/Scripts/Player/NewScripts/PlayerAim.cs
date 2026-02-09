using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool isPC;
    private Vector2 mouseLook, joystickLook;
    private Vector3 rotationTarget;

    public bool isPaused;

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }
    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        joystickLook = context.ReadValue<Vector2>();
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
}
