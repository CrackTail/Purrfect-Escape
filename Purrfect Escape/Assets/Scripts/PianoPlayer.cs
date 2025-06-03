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

    public float fadeOutDuration = 0.5f;

    private bool isPlayerOnPlatform;
    private Transform playerTransform;
    private float lastXPosition;
    private AudioClip currentClip;

    private bool isIdleLooping;
    private Coroutine fadeOutCoroutine;
    private Coroutine idleLoopCoroutine;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerTransform = collision.collider.transform;
            lastXPosition = playerTransform.position.x;
            isPlayerOnPlatform = true;
            PlayIdleSound();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            playerTransform = null;
            FadeOutAndStop(fadeOutDuration);
            currentClip = null;
            isIdleLooping = false;
            if (idleLoopCoroutine != null)
            {
                StopCoroutine(idleLoopCoroutine);
                idleLoopCoroutine = null;
            }
        }
    }

    private void Update()
    {
        if (!isPlayerOnPlatform || playerTransform == null) return;

        float currentX = playerTransform.position.x;
        bool isMoving = Mathf.Abs(currentX - lastXPosition) > 0.001f;

        if (isMoving)
        {
            PlayMovementSound();
        }
        else
        {
            PlayIdleSound();
        }

        lastXPosition = currentX;
    }

    private void PlayIdleSound()
    {
        if (idleSounds.Length == 0) return;
        if (!isActiveAndEnabled) return;

        AudioClip clip = idleSounds[Random.Range(0, idleSounds.Length)];
        if (clip != currentClip)
        {
            currentClip = clip;
            audioSource.clip = currentClip;
            audioSource.loop = false;
            audioSource.time = Mathf.Clamp(idleLoopStart, 0, clip.length);
            audioSource.volume = 1f;
            audioSource.Play();
            isIdleLooping = true;
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
                fadeOutCoroutine = null;
            }
            if (idleLoopCoroutine != null)
            {
                StopCoroutine(idleLoopCoroutine);
            }
            idleLoopCoroutine = StartCoroutine(IdleLoopRoutine());
        }
    }

    private void PlayMovementSound()
    {
        if (movementSounds.Length == 0) return;

        AudioClip clip = movementSounds[Random.Range(0, movementSounds.Length)];
        if (clip != currentClip)
        {
            currentClip = clip;
            audioSource.clip = currentClip;
            audioSource.loop = true;
            audioSource.time = Random.Range(0f, clip.length);
            audioSource.volume = 1f;
            audioSource.Play();
            isIdleLooping = false;
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
                fadeOutCoroutine = null;
            }
            if (idleLoopCoroutine != null)
            {
                StopCoroutine(idleLoopCoroutine);
                idleLoopCoroutine = null;
            }
        }
    }

    private void FadeOutAndStop(float duration)
    {
        if (!isActiveAndEnabled) return;

        if (fadeOutCoroutine != null)
            StopCoroutine(fadeOutCoroutine);
        fadeOutCoroutine = StartCoroutine(FadeOutRoutine(duration));
        if (idleLoopCoroutine != null)
        {
            StopCoroutine(idleLoopCoroutine);
            idleLoopCoroutine = null;
        }
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

    private System.Collections.IEnumerator IdleLoopRoutine()
    {
        while (isIdleLooping && audioSource.isPlaying)
        {
            if (audioSource.time >= idleLoopEnd)
            {
                audioSource.time = idleLoopStart;
            }
            yield return null;
        }
    }
}