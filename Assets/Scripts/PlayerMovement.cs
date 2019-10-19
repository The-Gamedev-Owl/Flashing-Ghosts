using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 3.0f;

    private Rigidbody2D rb2d;
    private Animator animator;
    private GameObject lightsGO;
    private bool isIdle;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator>();
        lightsGO = transform.GetChild(0).gameObject;
        isIdle = false;
    }

    private void Update()
    {
        RotatePlayer();
        if (!isIdle && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            isIdle = true;
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0.3f);
        }
        else
            isIdle = false;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        rb2d.velocity = movement * speed;
    }

    private void RotatePlayer()
    {
        float lightsRotation = lightsGO.transform.eulerAngles.z;

        if (lightsRotation <= 45 || lightsRotation > 315)
            SetDirectionInAnimator("Left");
        else if (lightsRotation > 45 && lightsRotation <= 135)
            SetDirectionInAnimator("Down");
        else if (lightsRotation > 135 && lightsRotation <= 225)
            SetDirectionInAnimator("Right");
        else if (lightsRotation > 225 && lightsRotation <= 315)
            SetDirectionInAnimator("Up");
    }

    private void SetDirectionInAnimator(string direction)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.SetBool(parameter.name, false);
        }
        animator.SetBool(direction, true);
    }
}
