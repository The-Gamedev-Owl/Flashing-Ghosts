using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    public GameObject goalWindow;
    public GameObject flashlightWindow;
    public GameObject controlWindow;

    private Animator animator;
    private AudioSource clickButtonSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        clickButtonSound = GetComponent<AudioSource>();
    }

    #region Control
    public void HideControl()
    {
        clickButtonSound.Play();
        animator.SetTrigger("HideControl");
    }

    public void ShowControl()
    {
        clickButtonSound.Play();
        animator.SetTrigger("ShowControl");
    }

    public void ActivateControl()
    {
        controlWindow.SetActive(true);
    }

    public void DeactivateControl()
    {
        controlWindow.SetActive(false);
    }
    #endregion Control

    #region Flashlight
    public void HideFlashlight()
    {
        clickButtonSound.Play();
        animator.SetTrigger("HideFlashlight");
    }

    public void ShowFlashlight()
    {
        clickButtonSound.Play();
        animator.SetTrigger("ShowFlashlight");
    }

    public void ActivateFlashlight()
    {
        flashlightWindow.SetActive(true);
    }

    public void DeactivateFlashlight()
    {
        flashlightWindow.SetActive(false);
    }
    #endregion Flashlight

    #region Goal
    public void HideGoal()
    {
        clickButtonSound.Play();
        animator.SetTrigger("HideGoal");
    }

    public void DeactivateGoal()
    {
        goalWindow.SetActive(false);
    }

    public void ChangeSceneToLevel()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
    #endregion Goal

}
