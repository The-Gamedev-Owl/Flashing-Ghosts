using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceMusic : MonoBehaviour
{
    public float maxMusicVolume;
    public float fadeInSpeed;

    private float startTime;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        startTime = Time.time;
        StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic()
    {
        float beginVolume = audioSource.volume;

        audioSource.Play();
        while (audioSource.volume < maxMusicVolume)
        {
            audioSource.volume = Mathf.SmoothStep(beginVolume, maxMusicVolume, (Time.time - startTime) / fadeInSpeed);
            yield return null;
        }
    }
}
