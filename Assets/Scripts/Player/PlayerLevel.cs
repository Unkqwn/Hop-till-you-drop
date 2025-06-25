using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public int score;
    private bool bossIsSpawned;
    public GameObject boss;
    public GameObject bossSpawnArea;
    public GameObject gameManager;

    void Update()
    {
        scoreText.text = score.ToString();
        Debug.Log(score);

        if (score >= 10)
        {
            SceneManager.LoadScene("Win screen");
        }

        if (score >= 20)
        {
            gameManager.gameObject.GetComponent<EnemySpawner>().StopSpawning();
            if (bossIsSpawned == false)
            {
                GameObject bossEnemy = Instantiate(boss, bossSpawnArea.transform.position, Quaternion.identity);
                bossIsSpawned = true;
            }
        }
    }
}
