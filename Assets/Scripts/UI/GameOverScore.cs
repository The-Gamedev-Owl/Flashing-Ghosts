using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    private int score;
    private int highscore;

    public void Start()
    {
        var manager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        score = manager.score;
        highscore = PlayerPrefs.GetInt("Highscore");
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
        }
        //ResetScore(); // SHOULD BE COMMENTED
        DisplayScores();
    }

    //Should be called ONLY one time before build and commented when built
    private void ResetScore()
    {
        PlayerPrefs.SetInt("Highscore", 0);
    }

    private void DisplayScores()
    {
        var scoreText = transform.GetChild(0).GetComponent<Text>();
        var highscoreText = transform.GetChild(1).GetComponent<Text>();
        scoreText.text = score.ToString();
        highscoreText.text = highscore.ToString();
    }
}
