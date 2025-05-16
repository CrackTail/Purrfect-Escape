using UnityEngine;
using TMPro;

public class CatInteraction : MonoBehaviour
{
    public GameObject dialogueBubble;
    public TextMeshProUGUI dialogueText;

    private bool playerInRange = false;
    private bool hasReceivedFish = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowDialogue();
        }
    }

    void ShowDialogue()
    {
        dialogueBubble.SetActive(true);

        dialogueText.text = hasReceivedFish
            ? "You've already given me fish."
            : "Find me some food and I will give you this shinny key";
    }

    public void ReceiveFish()
    {
        hasReceivedFish = true;
        // Trigger giving the player the key here
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
        }
    }
}