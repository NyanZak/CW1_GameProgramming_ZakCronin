using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public static int score, highScore;
    public TMP_Text scoreText, highScoreText;

    private void Awake()
    {
       instance = this;

        if (PlayerPrefs.HasKey("HighScore"))
      {
           highScore = PlayerPrefs.GetInt("HighScore");
           highScoreText.text = highScore.ToString();
       }
    }

    public void AddScore()
    {
        score++;
        UpdateHighScore();
        scoreText.text = score.ToString();
    }

    public void MinusScore()
    {
        score --;
        UpdateHighScore();
        scoreText.text = score.ToString();
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString();
           PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void ClearHighScore()
    {
       PlayerPrefs.DeleteKey("HighScore");

        highScore = 0;
        highScoreText.text = highScore.ToString();
    }
}
