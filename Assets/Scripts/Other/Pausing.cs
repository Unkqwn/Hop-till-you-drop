using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Pausing : MonoBehaviour
{
    private bool isPaused = false;
    private PlayerShooting pShoot;

    private void Start()
    {
        pShoot = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>();
    }

    private void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        pShoot.isPaused = isPaused;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPaused = !isPaused;
        }
    }
}
