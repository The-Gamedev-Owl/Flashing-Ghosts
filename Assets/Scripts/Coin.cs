using UnityEngine;

public class Coin : MonoBehaviour
{
    private ScoreManager manager;

    private void Start()
    {
        Destroy(gameObject, 10f); // Coin disappear after 10 seconds
        manager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            manager.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
