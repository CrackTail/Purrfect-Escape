using UnityEngine;
using TMPro;

public class CatQuest : MonoBehaviour
{
    public GameObject dialogueBubble;
    public TextMeshProUGUI dialogueText;
    public GameObject NPCGives;
    public GameObject NPCTakes;

    [SerializeField] private string dialogueBefore = "Default request message";
    [SerializeField] private string dialogueAfter = "Default reward message";
    [SerializeField] private float fontSizeBefore = 36f;
    [SerializeField] private float fontSizeAfter = 36f;

    public enum RequiredItem { Fish, Necklace, Anger }
    [SerializeField] private RequiredItem requiredItem;

    public enum RewardItem { Key, KeyPink, KeyRusty }
    [SerializeField] private RewardItem rewardItem;

    private bool playerInRange = false;
    private bool hasReceivedItem = false;

    private PlayerInventory inventory;

    void Start()
    {
        inventory = FindAnyObjectByType<PlayerInventory>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!hasReceivedItem && HasRequiredItem())
            {
                ReceiveItem();
            }

            ShowDialogue();
        }
    }

    void ShowDialogue()
    {
        dialogueBubble.SetActive(true);

        if (hasReceivedItem)
        {
            dialogueText.text = dialogueAfter;
            dialogueText.fontSize = fontSizeAfter;
        }
        else
        {
            dialogueText.text = dialogueBefore;
            dialogueText.fontSize = fontSizeBefore;
        }
    }

    bool HasRequiredItem()
    {
        switch (requiredItem)
        {
            case RequiredItem.Fish:
                return inventory.hasFish;
            case RequiredItem.Necklace:
                return inventory.hasNecklace;
            case RequiredItem.Anger:
                return inventory.hasAnger;
            default:
                return false;
        }
    }

    void ReceiveItem()
    {
        hasReceivedItem = true;

        switch (requiredItem)
        {
            case RequiredItem.Fish:
                inventory.hasFish = false;
                Debug.Log("Item taken: Fish");
                break;
            case RequiredItem.Necklace:
                inventory.hasNecklace = false;
                Debug.Log("Item taken: Necklace");
                break;
            case RequiredItem.Anger:
                inventory.hasAnger = false;
                Debug.Log("Item taken: Anger");
                break;
        }

        if (NPCTakes != null)
            Destroy(NPCTakes);

        if (NPCGives != null)
            NPCGives.SetActive(true); // NPCGives should be the item to spawn in the scene
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