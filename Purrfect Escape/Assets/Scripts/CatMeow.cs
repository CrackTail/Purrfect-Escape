using UnityEngine;

public class CatMeow : MonoBehaviour
{
    [SerializeField] private AudioClip[] meowClips;
    [SerializeField] private KeyCode meowKey = KeyCode.E;

    [SerializeField] private float minPitch = 0.95f;
    [SerializeField] private float maxPitch = 1.05f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(meowKey) && meowClips.Length > 0)
        {
            PlayRandomMeow();
        }
    }

    void PlayRandomMeow()
    {
        AudioClip clip = meowClips[Random.Range(0, meowClips.Length)];
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(clip);
    }
}