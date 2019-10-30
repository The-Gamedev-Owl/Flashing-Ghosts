using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    private Text scoreText;
    private Animator animator;

    private void Start()
    {
        scoreText = GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public void AddScore(int actualScore)
    {
        animator.SetTrigger("AddedScore");
        scoreText.text = actualScore.ToString();
    }
}
