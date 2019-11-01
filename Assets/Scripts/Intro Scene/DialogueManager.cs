using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueBoxText;
    public Image dialogueBoxImage;
    public FadeText continueButton;
    public GameObject[] allCharactersImagesToHide;
    public Dialogue[] dialogues;

    private int dialogueIndex;
    private Queue<string> actualSentences;
    private AudioSource audioSource;

    private void Start()
    {
        dialogueIndex = -1;
        actualSentences = new Queue<string>();
        audioSource = GetComponent<AudioSource>();
        DisplayNextDialogue();
    }

    private void EnqueueSentences()
    {
        foreach (string sentence in dialogues[dialogueIndex].sentences)
            actualSentences.Enqueue(sentence);
    }

    public void DisplayNextSentence()
    {
        string displayedSentence;

        continueButton.HideButton();
        if (actualSentences.Count == 0)
            DisplayNextDialogue();
        else
        {
            displayedSentence = actualSentences.Dequeue();
            StopAllCoroutines();
            Coroutine typeText = StartCoroutine(TypeText(displayedSentence));
        }
    }

    private void DisplayNextDialogue()
    {
        actualSentences.Clear();
        if (dialogueIndex >= dialogues.Length - 1)
            ChangeSceneToGameplay();
        else
        {
            dialogueIndex++;
            dialogueBoxImage.sprite = dialogues[dialogueIndex].dialogueBoxImage;
            ShowCorrectCharacters();
            PlaySound();
            EnqueueSentences();
            DisplayNextSentence();
        }
    }

    private void PlaySound()
    {
        if (dialogues[dialogueIndex].soundToPlay != null)
        {
            audioSource.clip = dialogues[dialogueIndex].soundToPlay;
            audioSource.Play();
        }
    }

    private void ShowCorrectCharacters()
    {
        if (dialogues[dialogueIndex].gameobjectToEnable.Length > 0)
        {
            foreach (GameObject characterToHide in allCharactersImagesToHide)
                characterToHide.SetActive(false);
            foreach (GameObject characterToShow in dialogues[dialogueIndex].gameobjectToEnable)
                characterToShow.SetActive(true);
        }
    }

    private IEnumerator TypeText(string textToDisplay)
    {
        dialogueBoxText.text = "";

        foreach (char character in textToDisplay.ToCharArray())
        {
            dialogueBoxText.text += character;
            if (character == '.' || character == '?' || character == '!')
                yield return new WaitForSeconds(0.8f);
            else if (character == ',' || character == ':')
                yield return new WaitForSeconds(0.5f);
            else
                yield return new WaitForSeconds(Time.deltaTime);
        }
        ShowContinueButton();
    }

    private void ShowContinueButton()
    {
        continueButton.StartFade();
    }

    public void ChangeSceneToGameplay()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
}
