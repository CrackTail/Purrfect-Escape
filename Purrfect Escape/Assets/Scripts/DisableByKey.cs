using UnityEngine;

public class DisableOnKeyPickup : MonoBehaviour
{
    [Header("Assign the Key GameObject (e.g., Key_Yellow)")]
    [SerializeField] private GameObject keyItem;

    [Header("Door that will appear when this one disables")]
    [SerializeField] private GameObject doorOpen; // Enable this when the door unlocks

    private PlayerInventory playerInventory;
    private string keyTag;
    private bool hasBeenDisabled = false;
    private bool playerIsInRange = false;

    void Start()
    {
        playerInventory = Object.FindFirstObjectByType<PlayerInventory>();

        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory not found in the scene!");
            return;
        }

        if (keyItem == null)
        {
            Debug.LogError("No key item assigned to DisableOnKeyPickup on " + gameObject.name);
            return;
        }

        keyTag = keyItem.tag;

        if (string.IsNullOrEmpty(keyTag))
        {
            Debug.LogError("Assigned key item does not have a tag!");
        }

        if (doorOpen != null)
        {
            doorOpen.SetActive(false); // Hide open door by default
        }
    }

    void Update()
    {
        if (hasBeenDisabled || !playerIsInRange || playerInventory == null || string.IsNullOrEmpty(keyTag))
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            bool hasKey = CheckIfPlayerHasKey(keyTag);

            if (hasKey)
            {
                if (doorOpen != null)
                    doorOpen.SetActive(true);

                if (transform.parent != null)
                {
                    transform.parent.gameObject.SetActive(false);
                    hasBeenDisabled = true;
                }
                else
                {
                    Debug.LogError("No parent object found to disable.");
                }

                playerInventory.UpdateInventory(keyTag, false);
                SetPlayerKeyFlag(keyTag, false);
            }
            else
            {
                Debug.Log("Missing required key: " + keyTag);
            }
        }
    }

    private bool CheckIfPlayerHasKey(string tag)
    {
        return tag switch
        {
            "Key_Yellow" => playerInventory.hasYellowKey,
            "Key_Pink" => playerInventory.hasPinkKey,
            "Key_Rusty" => playerInventory.hasRustyKey,
            "Key_Blue" => playerInventory.hasBlueKey,
            "Key_Orange" => playerInventory.hasOrangeKey,
            "Key_Purple" => playerInventory.hasPurpleKey,
            "Key_Red" => playerInventory.hasRedKey,
            _ => false
        };
    }

    private void SetPlayerKeyFlag(string tag, bool value)
    {
        switch (tag)
        {
            case "Key_Yellow": playerInventory.hasYellowKey = value; break;
            case "Key_Pink": playerInventory.hasPinkKey = value; break;
            case "Key_Rusty": playerInventory.hasRustyKey = value; break;
            case "Key_Blue": playerInventory.hasBlueKey = value; break;
            case "Key_Orange": playerInventory.hasOrangeKey = value; break;
            case "Key_Purple": playerInventory.hasPurpleKey = value; break;
            case "Key_Red": playerInventory.hasRedKey = value; break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInRange = false;
        }
    }
}