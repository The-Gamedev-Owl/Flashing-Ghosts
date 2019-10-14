using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public float scaredTime;
    /* Camera Zoom */
    public float minCameraZoom;
    public float maxCameraZoom;
    /* Invincible */
    public float invincibleTime;
    public float flashingInvincibleRate; // Should be lower than 'invincibleTime'
    private bool isInvincible;

    private Animator animator;
    private ChangeDirection directionManager;
    private PlayerInventory playerInventory;

    private void Start()
    {
        isInvincible = false;
        directionManager = GetComponent<ChangeDirection>();
        animator = GetComponent<Animator>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isInvincible && collision.CompareTag("Enemy"))
        {
            AnimatorSetBool("Scared");
            directionManager.SetIsScared(true); // Disable flashlight control
            playerInventory.LooseHP(1);
            Destroy(collision.gameObject);
            //// Should be immobile when scared
            isInvincible = true;
            StartCoroutine(ZoomInCameraToPlayer(maxCameraZoom)); // Zoom camera to player
            if (playerInventory.lifes > 0)
            {
                //// Touching ghost should disapear + Nearest ghosts should be backed away to allow the player to see animation and not being touched right away
                StartCoroutine(ScaredTimer()); // Exit "Scared" mode after 'scaredTime'
            }
        }
    }

    private IEnumerator ScaredTimer()
    {
        yield return new WaitForSeconds(scaredTime); // Wait for player not to be scared again
        AnimatorSetBool("Left");
        directionManager.SetIsScared(false); // Enable flashlight control
        StartCoroutine(ZoomOutCamera()); // Zoom out camera to default position and zooms
        StartCoroutine(Invincible()); // Flash Sprite when player is invincible
    }

    private IEnumerator ZoomInCameraToPlayer(float orthographicSize)
    {
        float timeGoal = Time.time + scaredTime;
        Vector3 playerPosition;

        while (Time.time < timeGoal)
        {
            playerPosition = transform.position;
            playerPosition.z = Camera.main.transform.position.z;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, playerPosition, Time.deltaTime * 2f);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, orthographicSize, Time.deltaTime * 2f);
            yield return new WaitForSeconds(0f);
        }
    }

    private IEnumerator ZoomOutCamera()
    {
        Vector3 zeroPos = Vector3.zero;
        float timeGoal = Time.time + invincibleTime;

        zeroPos.z = Camera.main.transform.position.z;
        while (Time.time < timeGoal)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, zeroPos, Time.deltaTime * 2f);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, minCameraZoom, Time.deltaTime * 2f);
            yield return new WaitForSeconds(0f);
        }
        Camera.main.transform.position = new Vector3(0f, 0f, zeroPos.z);
        Camera.main.orthographicSize = minCameraZoom;
    }

    private IEnumerator Invincible()
    {
        float timeGoal = Time.time + invincibleTime;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        while (spriteRenderer != null && Time.time < timeGoal)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashingInvincibleRate);
        }
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    private void AnimatorSetBool(string boolToSet)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.SetBool(parameter.name, false);
        }
        animator.SetBool(boolToSet, true);
    }
}
