using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameObject itemToPickUp;
    private PlayerInventory inventory;

    [System.Serializable]
    public class ItemData
    {
        public string tag;
        public GameObject iconImage;
    }

    [SerializeField] private ItemData[] items;

    void Start()
    {
        inventory = FindAnyObjectByType<PlayerInventory>();
        if (inventory == null)
        {
            Debug.LogWarning("PlayerInventory component not found!");
        }
    }

    void Update()
    {
        if (itemToPickUp && Input.GetKeyDown(KeyCode.E))
        {
            foreach (var item in items)
            {
                if (itemToPickUp.CompareTag(item.tag))
                {
                    Debug.Log($"Picking up item with tag: {item.tag}");

                    RegisterItem(item.tag);

                    if (item.iconImage != null)
                    {
                        item.iconImage.SetActive(true);
                        Debug.Log($"Icon for {item.tag} activated.");
                    }

                    Destroy(itemToPickUp);
                    itemToPickUp = null;
                    break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var item in items)
        {
            if (other.CompareTag(item.tag))
            {
                itemToPickUp = other.gameObject;
                Debug.Log($"Item to pick up detected: {other.gameObject.name} with tag {item.tag}");
                break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == itemToPickUp)
        {
            Debug.Log($"Item to pick up exited trigger: {other.gameObject.name}");
            itemToPickUp = null;
        }
    }

    void RegisterItem(string tag)
    {
        Debug.Log($"RegisterItem called with tag: {tag}");

        switch (tag)
        {
            case "Fish":
                Debug.Log("Registering Fish.");
                inventory.hasFish = true;
                break;
            case "Key_Yellow":
                Debug.Log("Registering Key_Yellow.");
                inventory.hasYellowKey = true;
                break;
            case "Key_Pink":
                Debug.Log("Registering Key_Pink.");
                inventory.hasPinkKey = true;
                break;
            case "Key_Rusty":
                Debug.Log("Registering Key_Rusty.");
                inventory.hasRustyKey = true;
                break;
            case "Key_Blue":
                Debug.Log("Registering Key_Blue.");
                inventory.hasBlueKey = true;
                break;
            case "Key_Orange":
                Debug.Log("Registering Key_Orange.");
                inventory.hasOrangeKey = true;
                break;
            case "Key_Purple":
                Debug.Log("Registering Key_Purple.");
                inventory.hasPurpleKey = true;
                break;
            case "Key_Red":
                Debug.Log("Registering Key_Red.");
                inventory.hasRedKey = true;
                break;
            case "Necklace":
                Debug.Log("Registering Necklace.");
                inventory.hasNecklace = true;
                break;
            case "Anger":
                Debug.Log("Registering Anger.");
                inventory.hasAnger = true;
                break;
            default:
                Debug.LogWarning($"RegisterItem received unhandled tag: {tag}");
                break;
        }
    }
}