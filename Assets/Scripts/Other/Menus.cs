using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void restartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void quitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void mainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
