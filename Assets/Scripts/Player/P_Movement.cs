using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement : MonoBehaviour
{
    private float playerSpeed = 10f;
    private float speed;
    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float playerVerticalInput = Input.GetAxis("Vertical");
        float playerHorizontalInput = Input.GetAxis("Horizontal");

        if (playerHorizontalInput >= 0.5 && playerVerticalInput >= 0.5 || playerHorizontalInput <= -0.5 && playerVerticalInput <= -0.5 ||
            playerHorizontalInput >= 0.5 && playerVerticalInput <= -0.5 || playerHorizontalInput <= -0.5 && playerVerticalInput >= 0.5)
        {
            speed = playerSpeed * 0.66f;
        }
        else
        {
            speed = playerSpeed;
        }

        Vector3 up = Camera.main.transform.up;
        Vector3 right = Camera.main.transform.right;
        up.y = 0;
        right.y = 0;
        up = up.normalized;
        right = right.normalized;

        Vector3 forwardReletiveVerticalInput = playerVerticalInput * up * Time.deltaTime * speed;
        Vector3 rightReletiveVerticalInput = playerHorizontalInput * right * Time.deltaTime * speed;

        Vector3 cameraRelativeMovement = forwardReletiveVerticalInput + rightReletiveVerticalInput;
        this.transform.Translate(cameraRelativeMovement, Space.World);
    }
}
