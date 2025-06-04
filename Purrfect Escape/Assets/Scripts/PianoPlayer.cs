using UnityEngine;

public class PianoTrigger : MonoBehaviour
{
    public AudioClip[] idleSounds;
    public AudioClip[] movementSounds;
    public AudioSource audioSource;

    [Range(0, 10)]
    public float idleLoopStart = 0f;

    [Range(0, 10)]
    public float idleLoopEnd = 5f;

    public float fadeInDuration = 0.2f;
    public float fadeOutDuration = 0.5f;

    [Header("Compressor-like behavior")]
    [Range(0f, 1f)]
    public float maxVolumeThreshold = 0.8f; // <-- volume cap

    private bool isPlayerOnPlatform;
    private Rigidbody2D playerRb;
    private bool wasMoving;
    private AudioClip currentClip;

    private Coroutine fadeOutCoroutine;
    private Coroutine fadeInCoroutine;
    private Coroutine idleLoopCoroutine;

    private float previousPlayerX;
    private float previousPlayerY;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerRb = collision.collider.GetComponent<Rigidbody2D>();
            isPlayerOnPlatform = true;
            previousPlayerX = playerRb.position.x;
            previousPlayerY = playerRb.position.y;
            PlayIdleSound();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isPlayerOnPlatform || playerRb == null) return;

        bool isMoving = Mathf.Abs(playerRb.position.x - previousPlayerX) > 0.001f || Mathf.Abs(playerRb.position.y - previousPlayerY) > 0.001f;
        previousPlayerX = playerRb.position.x;
        previousPlayerY = playerRb.position.y;

        if (isMoving && !wasMoving)
        {
            PlayMovementSound();
        }
        else if (!isMoving && wasMoving)
        {
            PlayIdleSound();
        }

        wasMoving = isMoving;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            playerRb = null;
            wasMoving = false;
            FadeOutAndStop(fadeOutDuration);
            currentClip = null;
            if (idleLoopCoroutine != null)
            {
                StopCoroutine(idleLoopCoroutine);
                idleLoopCoroutine = null;
            }
        }
    }

    private void PlayIdleSound()
    {
        if (idleSounds.Length == 0 || !isActiveAndEnabled) return;

        AudioClip clip = idleSounds[Random.Range(0, idleSounds.Length)];
        if (clip != currentClip)
        {
            currentClip = clip;
            audioSource.clip = currentClip;
            audioSource.loop = false;
            audioSource.time = Mathf.Clamp(idleLoopStart, 0f, clip.length);
            audioSource.volume = 0f;
            audioSource.Play();
            StopAllFadeCoroutines();
            fadeInCoroutine = StartCoroutine(FadeInRoutine(fadeInDuration));
        }
    }

    private void PlayMovementSound()
    {
        if (movementSounds.Length == 0 || !isActiveAndEnabled) return;

        AudioClip clip = movementSounds[Random.Range(0, movementSounds.Length)];
        if (clip != currentClip)
        {
            currentClip = clip;
            audioSource.clip = currentClip;
            audioSource.loop = true;
            audioSource.time = Random.Range(0f, clip.length);
            audioSource.volume = 0f;
            audioSource.Play();
            StopAllFadeCoroutines();
            fadeInCoroutine = StartCoroutine(FadeInRoutine(fadeInDuration));
        }
    }

    private void FadeOutAndStop(float duration)
    {
        if (!isActiveAndEnabled) return;

        StopAllFadeCoroutines();
        fadeOutCoroutine = StartCoroutine(FadeOutRoutine(duration));
    }

    private void StopAllFadeCoroutines()
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            fadeOutCoroutine = null;
        }
        if (fadeInCoroutine != null)
        {
            StopCoroutine(fadeInCoroutine);
            fadeInCoroutine = null;
        }
        if (idleLoopCoroutine != null)
        {
            StopCoroutine(idleLoopCoroutine);
            idleLoopCoroutine = null;
        }
    }

    private System.Collections.IEnumerator FadeInRoutine(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Min(Mathf.Lerp(0f, 1f, time / duration), maxVolumeThreshold);
            yield return null;
        }
        audioSource.volume = maxVolumeThreshold;
        fadeInCoroutine = null;
    }

    private System.Collections.IEnumerator FadeOutRoutine(float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        fadeOutCoroutine = null;
    }
}