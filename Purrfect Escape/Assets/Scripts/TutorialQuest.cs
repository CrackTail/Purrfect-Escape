using TMPro;
using UnityEngine;
using System.Collections;

public class TutorialQuest : MonoBehaviour
{
    public GameObject dialogueBubble;
    public TextMeshProUGUI dialogueText;

    [SerializeField] private string[] dialogueLines;
    private int currentLineIndex = 0;

    private bool playerInRange = false;

    [SerializeField] private AudioClip[] meowClips; 
    [SerializeField] private float meowDelay = 0.3f;

    private AudioSource audioSource;

    void Start()
    {
        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowNextDialogueLine();

            if (audioSource != null && meowClips.Length > 0)
            {
                StartCoroutine(PlayMeowWithDelay());
            }
        }
    }

    void ShowNextDialogueLine()
    {
        if (dialogueLines.Length == 0) return;

        dialogueBubble.SetActive(true);
        dialogueText.text = dialogueLines[currentLineIndex];

        currentLineIndex = (currentLineIndex + 1) % dialogueLines.Length;
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
        {
            playerInRange = false;
            dialogueBubble.SetActive(false);
            currentLineIndex = 0;
        }
    }
}