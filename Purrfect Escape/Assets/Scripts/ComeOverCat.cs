using UnityEngine;
using TMPro;
using System.Collections;

public class ComeOverCat : MonoBehaviour
{
    public GameObject dialogueBubble;
    public TextMeshProUGUI dialogueText;

    [TextArea]
    [SerializeField] private string dialogueLine = "This is the only message.";
    [SerializeField] private float fontSize = 36f;

    [SerializeField] private AudioClip[] meowClips;
    [SerializeField] private float meowDelay = 0.3f;

    private bool playerInRange = false;
    private bool dialogueVisible = true;

    private AudioSource audioSource;

    void Start()
    {
        if (dialogueBubble != null)
        {
            dialogueBubble.SetActive(true);
            dialogueText.text = dialogueLine;
            dialogueText.fontSize = fontSize;
            dialogueVisible = true;
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && dialogueVisible)
        {
            dialogueBubble.SetActive(false);
            dialogueVisible = false;
            if (audioSource != null && meowClips.Length > 0)
            {
                StartCoroutine(PlayMeowWithDelay());
            }
        }
    }

    private IEnumerator PlayMeowWithDelay()
    {
        yield return new WaitForSeconds(meowDelay);

        AudioClip clip = meowClips[Random.Range(0, meowClips.Length)];
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clip);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}