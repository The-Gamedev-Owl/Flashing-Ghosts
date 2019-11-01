using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public DisplayScore scoreText;

    private int score;

    private void Start()
    {
        score = 0;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.AddScore(score);
    }
}
