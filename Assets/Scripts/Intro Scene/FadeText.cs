using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    public Text text;
    public Button button;

    void Start()
    {
        button = GetComponent<Button>();
        HideButton();
    }

    public void HideButton()
    {
        Color transparent = text.color;

        button.interactable = false;
        StopAllCoroutines();
        transparent.a = 0f;
        text.color = transparent;
    }

    public void StartFade()
    {
        button.interactable = true;
        StartCoroutine(FadeInText());
    }

    private IEnumerator FadeInText()
    {
        Color actualColor = text.color;
        float fadeDuration = 2f;

        while (text.color.a < 1f)
        {
            actualColor.a += Mathf.Lerp(0, 1, Time.deltaTime / fadeDuration);
            actualColor.a = Mathf.Clamp(actualColor.a, 0, 1); // Cap maximum alpha to 1 (and minimum to 0)
            text.color = actualColor;
            yield return null;
        }
        yield return StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        Color actualColor = text.color;
        float fadeDuration = 2f;

        while (text.color.a > 0f)
        {
            actualColor.a -= Mathf.Lerp(0, 1, Time.deltaTime / fadeDuration);
            actualColor.a = Mathf.Clamp(actualColor.a, 0, 1); // Cap maximum alpha to 1 (and minimum to 0)
            text.color = actualColor;
            yield return null;
        }
        yield return StartCoroutine(FadeInText());
    }
}
