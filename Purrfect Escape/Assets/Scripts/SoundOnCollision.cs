using UnityEngine;

public class SoundTopTrigger : MonoBehaviour
{
    public AudioClip[] soundOrigin;
    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Vector2 direction = other.transform.position - transform.position;
        if (direction.y > 0.1f)
        {
            PlaySound();
        }
    }
    private void PlaySound()
    {
        if (soundOrigin.Length == 0) return;

        AudioClip clip = soundOrigin[Random.Range(0, soundOrigin.Length)];
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.volume = 1f;
        audioSource.Play();
    }
}