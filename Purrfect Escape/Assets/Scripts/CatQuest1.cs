using UnityEngine;
using TMPro;

public class CatQuest1 : MonoBehaviour
{
    public GameObject dialogueBubble;
    public TextMeshProUGUI dialogueText;
    public GameObject NPCGives;
    public GameObject NPCTakes;

    private bool playerInRange = false;
    private bool hasReceivedFish = false;

    private PlayerInventory inventory;

    void Start()
    {
        inventory = FindAnyObjectByType<PlayerInventory>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!hasReceivedFish && inventory.hasFish)
            {
                ReceiveFish();
            }

            ShowDialogue();
        }
    }

    void ShowDialogue()
    {
        dialogueBubble.SetActive(true);

        dialogueText.text = hasReceivedFish
            ? "Now leave me be. I have more important matters to attend to."
            : "Find me some food and I will give you this shiny key";
    }

    public void ReceiveFish()
    {
        hasReceivedFish = true;
        inventory.hasFish = false;

        if (NPCTakes != null)
            Destroy(NPCTakes);

        if (NPCGives != null)
            NPCGives.SetActive(true);
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