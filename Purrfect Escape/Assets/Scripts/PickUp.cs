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
    }

    void Update()
    {
        if (itemToPickUp && Input.GetKeyDown(KeyCode.E))
        {
            foreach (var item in items)
            {
                if (itemToPickUp.CompareTag(item.tag))
                {
                    RegisterItem(item.tag);

                    if (item.iconImage != null)
                        item.iconImage.SetActive(true);

                    Debug.Log($"{item.tag} registered");
                    Destroy(itemToPickUp);
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
                break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == itemToPickUp)
        {
            itemToPickUp = null;
        }
    }

    void RegisterItem(string tag)
    {
        switch (tag)
        {
            case "Fish":
                inventory.hasFish = true;
                break;
            case "Key":
                inventory.hasKey = true;
                break;
            case "KeyPink":
                inventory.hasKeyPink = true;
                break;
            case "KeyRusty":  // <-- Fixed tag here to match your object tag exactly
                inventory.hasRustyKey = true;
                break;
            case "Necklace":
                inventory.hasNecklace = true;
                break;
            case "Anger":
                inventory.hasAnger = true;
                break;
            default:
                Debug.LogWarning($"Unknown item tag '{tag}' passed to RegisterItem");
                break;
        }
    }
}