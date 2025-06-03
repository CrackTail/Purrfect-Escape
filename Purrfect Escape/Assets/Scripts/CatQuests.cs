using UnityEngine;
using TMPro;
using System.Collections;

public class CatQuest : MonoBehaviour
{
    public GameObject dialogueBubble;
    public TextMeshProUGUI dialogueText;
    public GameObject NPCGives;
    public GameObject NPCTakes;

    [TextArea]
    [SerializeField] private string[] dialogueBeforeLines = { "Default request message 1", "Default request message 2" };
    [TextArea]
    [SerializeField] private string dialogueAfter = "Default reward message";

    [SerializeField] private float fontSizeBefore = 36f;
    [SerializeField] private float fontSizeAfter = 36f;

    public enum RequiredItem { Fish, Necklace, Anger }
    [SerializeField] private RequiredItem requiredItem;

    public enum RewardItem { KeyYellow, KeyPink, KeyRusty }
    [SerializeField] private RewardItem rewardItem;

    private bool playerInRange = false;
    private bool hasReceivedItem = false;
    private int dialogueIndex = 0;

    private PlayerInventory inventory;

    [Header("Meow Settings")]
    [SerializeField] private AudioClip[] meowClips;
    [SerializeField] private float meowDelay = 0.3f;

    private AudioSource audioSource;

    void Start()
    {
        inventory = FindAnyObjectByType<PlayerInventory>();
        if (inventory == null)
        {
            Debug.LogError("PlayerInventory not found by CatQuest!");
        }
        else
        {
            Debug.Log($"CatQuest found PlayerInventory instance: {inventory.name}");
        }

        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogWarning("Missing AudioSource on CatQuest GameObject.");
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!hasReceivedItem && HasRequiredItem())
            {
                ReceiveItem();
                ShowDialogue();
            }
            else
            {
                ShowDialogue();
            }

            if (audioSource != null && meowClips.Length > 0)
                StartCoroutine(PlayMeowWithDelay());
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
            if (dialogueBeforeLines.Length > 0)
            {
                dialogueText.text = dialogueBeforeLines[dialogueIndex];
                dialogueText.fontSize = fontSizeBefore;

                dialogueIndex++;
                if (dialogueIndex >= dialogueBeforeLines.Length)
                {
                    dialogueIndex = 0;
                }
            }
        }
    }

    bool HasRequiredItem()
    {
        switch (requiredItem)
        {
            case RequiredItem.Fish: return inventory.hasFish;
            case RequiredItem.Necklace: return inventory.hasNecklace;
            case RequiredItem.Anger: return inventory.hasAnger;
            default: return false;
        }
    }

    void ReceiveItem()
    {
        hasReceivedItem = true;

        switch (requiredItem)
        {
            case RequiredItem.Fish: inventory.hasFish = false; break;
            case RequiredItem.Necklace: inventory.hasNecklace = false; break;
            case RequiredItem.Anger: inventory.hasAnger = false; break;
        }

        inventory.UpdateInventory(requiredItem.ToString(), false);

        if (NPCTakes != null)
            Destroy(NPCTakes);

        if (NPCGives != null)
            NPCGives.SetActive(true);
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
            dialogueIndex = 0;
        }
    }
}