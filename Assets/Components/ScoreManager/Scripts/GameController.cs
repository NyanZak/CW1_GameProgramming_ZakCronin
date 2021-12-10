using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public static int playerScore;

    private void Awake()
    {
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        playerScore = ScoreManager.score;
        if (ScoreManager.score == -1)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("ScoreManager");
        //gameOverScreen.SetActive(false);
        ScoreManager.instance.ResetScore();
    }

    public void ClearHighScore()
    {
        GetComponent<ScoreManager>().ClearHighScore();
    }
}
