using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum GameState
{
    
}

public class Pausing : MonoBehaviour
{
    private bool isPaused = false;

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
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPaused = !isPaused;
        }
    }
}
