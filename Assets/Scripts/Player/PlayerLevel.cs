using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public int score;

    [SerializeField] private GameObject WinScreen;

    void Start()
    {

        WinScreen.SetActive(false);
    }

    void Update()
    {
        scoreText.text = score.ToString();

        if (score >= 10)
        {
            WinScreen.SetActive(true);
        }
    }
}
