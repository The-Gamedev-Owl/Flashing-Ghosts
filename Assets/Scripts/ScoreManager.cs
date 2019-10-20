using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private int score;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        score = 0;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Your score: " + score;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
