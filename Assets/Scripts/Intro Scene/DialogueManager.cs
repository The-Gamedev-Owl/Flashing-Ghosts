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

    #region Sentences
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
            StartCoroutine(TypeText(displayedSentence));
        }
    }
    #endregion Sentences

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

    #region Miscellaneous
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
    #endregion Miscellaneous

    #region TextDisplay
    private IEnumerator TypeText(string textToDisplay)
    {
        dialogueBoxText.text = "";

        // If LeftClick if pressed, displays the text entirely
        StartCoroutine(DisplayAllText(textToDisplay));
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
        FinishTyping();
    }

    private IEnumerator DisplayAllText(string textToDisplay)
    {
        // Prevent from skiping text when releasing click after clicked on Continue
        bool hasButtonDown = false;

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
                hasButtonDown = true;
            else if (hasButtonDown && Input.GetMouseButtonUp(0))
            {
                dialogueBoxText.text = textToDisplay;
                FinishTyping();
                break;
            }
            yield return null;
        }
    }

    private void FinishTyping()
    {
        StopAllCoroutines();
        ShowContinueButton();
    }
    #endregion TextDisplay

    private void ShowContinueButton()
    {
        continueButton.StartFade();
    }

    public void ChangeSceneToGameplay()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
}
