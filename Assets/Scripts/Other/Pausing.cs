using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;

public class Pausing : MonoBehaviour
{
    [SerializeField] private bool isPaused = false;
    [SerializeField] private GameObject pauseMenu;
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
        pauseMenu.SetActive(isPaused);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPaused = !isPaused;
        }
    }

    public void ResumeButton()
    {
        isPaused = false;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
