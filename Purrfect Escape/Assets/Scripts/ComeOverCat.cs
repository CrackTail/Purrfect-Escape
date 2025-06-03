using UnityEngine;
using TMPro;

public class ComeOverCat : MonoBehaviour
{
    public GameObject dialogueBubble;
    public TextMeshProUGUI dialogueText;

    [TextArea]
    [SerializeField] private string dialogueLine = "This is the only message.";
    [SerializeField] private float fontSize = 36f;

    private bool playerInRange = false;
    private bool dialogueVisible = true;

    void Start()
    {
        if (dialogueBubble != null)
        {
            dialogueBubble.SetActive(true);
            dialogueText.text = dialogueLine;
            dialogueText.fontSize = fontSize;
            dialogueVisible = true;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && dialogueVisible)
        {
            dialogueBubble.SetActive(false);
            dialogueVisible = false;
        }
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