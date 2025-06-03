using TMPro;
using UnityEngine;

public class TutorialQuest : MonoBehaviour
{
    public GameObject dialogueBubble;
    public TextMeshProUGUI dialogueText;

    [SerializeField] private string[] dialogueLines;
    private int currentLineIndex = 0;

    private bool playerInRange = false;

    void Start()
    {
        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowNextDialogueLine();
        }
    }

    void ShowNextDialogueLine()
    {
        if (dialogueLines.Length == 0) return;

        dialogueBubble.SetActive(true);
        dialogueText.text = dialogueLines[currentLineIndex];

        currentLineIndex = (currentLineIndex + 1) % dialogueLines.Length; 
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
