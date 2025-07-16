using UnityEngine;
using System.Collections;

public class MusicLooper : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] loopClips;
    public float switchInterval = 60f;
    public float fadeDuration = 1.5f;

    private int currentIndex = 0;
    private float timer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        PlayClip(currentIndex);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % loopClips.Length;
            StartCoroutine(FadeToNextClip(currentIndex));
        }
    }

    void PlayClip(int index)
    {
        if (loopClips.Length == 0 || audioSource == null) return;

        audioSource.clip = loopClips[index];
        audioSource.loop = true;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    IEnumerator FadeToNextClip(int nextIndex)
    {
        // Fade Out
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();

        // Switch clip
        audioSource.clip = loopClips[nextIndex];
        audioSource.Play();

        // Fade In
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 1f;
    }
}