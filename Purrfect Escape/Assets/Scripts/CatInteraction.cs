using UnityEngine;
using UnityEngine.UI;
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
        if (!hasReceivedFish)
        {
            dialogueText.text = "Grant me fish and I'll grant you key.";
        }
        else
        {
            dialogueText.text = "You've already given me fish.";
        }
    }

    public void ReceiveFish()
    {
        hasReceivedFish = true;
        // Here, you'd trigger giving the player the key (see next script)
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueBubble.SetActive(false);
        }
    }
}

