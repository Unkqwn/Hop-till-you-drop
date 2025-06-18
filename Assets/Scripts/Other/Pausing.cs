using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Pausing : MonoBehaviour
{
    [SerializeField] private bool isPaused = false;
    private PlayerShooting pShoot;

    public GameObject pauseFirstButton;

    private void Start()
    {
        pShoot = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>();
    }

    private void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(null);


            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
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
