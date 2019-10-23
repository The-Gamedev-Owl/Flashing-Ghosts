using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [Tooltip("Boo's speed")]
    [Range(0.01f, 0.1f)]
    public readonly float speed = .02f;

    [Tooltip("Coin to drop on death")]
    public GameObject coin;

    /* Sounds */
    public AudioClip attackSound;
    public AudioClip deathSound;

    private Transform player;
    private SpriteRenderer sr;
    private Animator animator;
    private BoxCollider2D booCollider;
    private AudioSource audioSource;

    private float angleTowardPlayer;
    private bool isAttacking;

    #region MonoBehaviour
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        booCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        isAttacking = false;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            angleTowardPlayer = GetAngleTowardPlayer();
            PlayMovementAnimation();
        }
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed);
    }
    #endregion MonoBehaviour

    #region Animations
    private float GetAngleTowardPlayer()
    {
        Vector3 boo_pos;
        Vector3 player_pos;

        player_pos = Camera.main.WorldToScreenPoint(player.position);
        boo_pos = Camera.main.WorldToScreenPoint(transform.position);
        boo_pos.x -= player_pos.x;
        boo_pos.y -= player_pos.y;
        return Mathf.Atan2(boo_pos.y, boo_pos.x) * Mathf.Rad2Deg;
    }

    private void PlayMovementAnimation()
    {
        if (angleTowardPlayer <= 45 && angleTowardPlayer >= -45)
            SetBoolInAnimator("Left");
        else if (angleTowardPlayer > 45 && angleTowardPlayer <= 135)
            SetBoolInAnimator("Down");
        else if (angleTowardPlayer < -45 && angleTowardPlayer >= -135)
            SetBoolInAnimator("Up");
        else if ((angleTowardPlayer > 135 && angleTowardPlayer <= 180) || (angleTowardPlayer >= -180 && angleTowardPlayer <= -135))
            SetBoolInAnimator("Right");
    }

    private void SetBoolInAnimator(string direction)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.SetBool(parameter.name, false);
        }
        animator.SetBool(direction, true);
    }

    public void AttackPlayer()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
        if ((angleTowardPlayer > 90 && angleTowardPlayer <= 180) || (angleTowardPlayer >= -180 && angleTowardPlayer <= -90))
            SetBoolInAnimator("AttackRight");
        else
            SetBoolInAnimator("AttackLeft");
        isAttacking = true;
    }

    public void KillBoo(bool shouldPlayDeathSound)
    {
        if (shouldPlayDeathSound)
        {
            audioSource.clip = deathSound;
            audioSource.Play();
        }
        Instantiate(coin, transform.position, coin.transform.rotation);
        if ((angleTowardPlayer > 90 && angleTowardPlayer <= 180) || (angleTowardPlayer >= -180 && angleTowardPlayer <= -90))
            SetBoolInAnimator("HitRight");
        else
            SetBoolInAnimator("HitLeft");
        booCollider.enabled = false;
        isAttacking = true;
    }
    #endregion Animations

    public void DestroyBoo()
    {
        Destroy(gameObject);
    }
}
