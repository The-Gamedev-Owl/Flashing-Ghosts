using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Restart()
    {
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Destroy();
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
}
