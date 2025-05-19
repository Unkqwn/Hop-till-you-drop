using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float speed;
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        // Changes the speed of the player when they aren't going straight
        if (zInput != 0 && xInput != 0)
        {
            speed = playerSpeed * 0.66f;
        }
        else
        {
            speed = playerSpeed;
        }
        
        // Makes the movement rely on the camera rotation instead of player rotation
        Vector3 forward = Camera.main.transform.up;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardReletiveVerticalInput = zInput * forward * Time.deltaTime * speed;
        Vector3 rightReletiveVerticalInput = xInput * right * Time.deltaTime * speed;

        Vector3 cameraRelativeMovement = forwardReletiveVerticalInput + rightReletiveVerticalInput;
        this.transform.Translate(cameraRelativeMovement, Space.World);
    }
}
