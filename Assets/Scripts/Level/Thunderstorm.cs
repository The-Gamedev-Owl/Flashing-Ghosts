using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunderstorm : MonoBehaviour
{
    private GameObject[] lights;
    private AudioSource audioSource;

    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("ThunderLight");
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StrikeThunder());
    }

    private IEnumerator StrikeThunder()
    {
        yield return new WaitForSeconds(Mathf.Lerp(5, 10, Random.value));
        while (true)
        {
            foreach (var light in lights)
            {
                var animator = light.GetComponent<Animator>();
                if (animator)
                {
                    animator.SetTrigger("StrikeThunder");
                }
            }

            audioSource.Play();
            yield return new WaitForSeconds(Mathf.Lerp(10, 20, Random.value));
        }
    }
}
