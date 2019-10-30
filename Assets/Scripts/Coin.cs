using UnityEngine;

public class Coin : MonoBehaviour
{
    private ScoreManager manager;
    private Animator animator;
    private AudioSource audioSource;
    private BoxCollider2D collider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider2D>();
        Destroy(gameObject, 10f); // Coin disappear after 10 seconds
        manager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collider.enabled = false;
            audioSource.Play();
            animator.SetTrigger("Pickup");
            manager.IncreaseScore();
        }
    }

    private void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
