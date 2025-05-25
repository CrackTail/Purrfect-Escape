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
            case "Key_Yellow":
                inventory.hasYellowKey = true;
                break;
            case "Key_Pink":
                inventory.hasPinkKey = true;
                break;
            case "Key_Rusty":
                inventory.hasRustyKey = true;
                break;
            case "Key_Blue":
                inventory.hasBlueKey = true;
                break;
            case "Key_Orange":
                inventory.hasOrangeKey = true;
                break;
            case "Key_Purple":
                inventory.hasPurpleKey = true;
                break;
            case "Key_Red":
                inventory.hasRedKey = true;
                break;
            case "Necklace":
                inventory.hasNecklace = true;
                break;
            case "Anger":
                inventory.hasAnger = true;
                break;
        }
    }
}