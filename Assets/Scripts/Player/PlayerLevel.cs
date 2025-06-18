using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public int score;

    [SerializeField] private Scene winScene;

    void Update()
    {
        scoreText.text = score.ToString();

        if (score >= 10)
        {
            SceneManager.SetActiveScene(winScene);
        }
    }
}
