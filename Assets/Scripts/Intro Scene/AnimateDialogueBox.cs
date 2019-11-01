using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateDialogueBox : MonoBehaviour
{
    public Sprite luigiBoxSprite;
    public Sprite luigiScaredBoxSprite;
    public Sprite profBoxSprite;

    private Image boxImage;
    private Sprite actualSprite;
    private Animator animator;

    void Start()
    {
        boxImage = transform.GetChild(0).GetComponent<Image>();
        actualSprite = boxImage.sprite;
        animator = GetComponent<Animator>();
        AnimateBox(); // Allows to animate even on first dialogue
    }

    private void OnGUI()
    {
        if (boxImage.sprite != actualSprite)
        {
            actualSprite = boxImage.sprite;
            AnimateBox();
        }
    }

    private void AnimateBox()
    {
        if (actualSprite == luigiBoxSprite)
            animator.SetTrigger("LuigiDialogue");
        else if (actualSprite == luigiScaredBoxSprite)
            animator.SetTrigger("LuigiScaredDialogue");
        else if (actualSprite == profBoxSprite)
            animator.SetTrigger("ProfDialogue");
    }
}
