using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Boo : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Boo's speed")]
    [Range(0.01f, 0.1f)]
    private readonly float speed = .02f;

    private Transform player;
    private SpriteRenderer sr;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed);
        sr.flipX = transform.position.y < player.position.y;
    }
}
