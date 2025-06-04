using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayOnEnableSound : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource musicSourceToStop;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (musicSourceToStop != null && musicSourceToStop.isPlaying)
        {
            musicSourceToStop.Stop();
        }

        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}