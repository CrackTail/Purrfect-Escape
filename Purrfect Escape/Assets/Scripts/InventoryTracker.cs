using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Image[] itemSlots = new Image[2];  // Index 0 = Slot 1, Index 1 = Slot 2
    [SerializeField] private Vector2[] slotCoordinates = new Vector2[2];

    private const int SLOT_ONE_INDEX = 0;
    private const int SLOT_TWO_INDEX = 1;

    private bool _hasFish = false;
    private bool _hasNecklace = false;
    private bool _hasAnger = false;
    private bool _hasYellowKey = false;
    private bool _hasPinkKey = false;
    private bool _hasRustyKey = false;
    private bool _hasBlueKey = false;
    private bool _hasOrangeKey = false;
    private bool _hasPurpleKey = false;
    private bool _hasRedKey = false;

    private void Start()
    {
        ApplySlotCoordinates();
    }

    private void ApplySlotCoordinates()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null && i < slotCoordinates.Length)
            {
                RectTransform rt = itemSlots[i].GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.anchoredPosition = slotCoordinates[i];
                    Debug.Log($"Slot {i + 1} positioned at {slotCoordinates[i]}.");
                }
                else
                {
                    Debug.LogWarning($"itemSlot[{i}] does not have a RectTransform.");
                }
            }
        }
    }

    public bool hasFish
    {
        get => _hasFish;
        set
        {
            if (_hasFish != value)
            {
                Debug.Log($"hasFish changed from {_hasFish} to {value}");
                _hasFish = value;
                UpdateInventory("Fish", value);
            }
        }
    }

    public bool hasNecklace
    {
        get => _hasNecklace;
        set
        {
            if (_hasNecklace != value)
            {
                Debug.Log($"hasNecklace changed from {_hasNecklace} to {value}");
                _hasNecklace = value;
                UpdateInventory("Necklace", value);
            }
        }
    }

    public bool hasAnger
    {
        get => _hasAnger;
        set
        {
            if (_hasAnger != value)
            {
                Debug.Log($"hasAnger changed from {_hasAnger} to {value}");
                _hasAnger = value;
                UpdateInventory("Anger", value);
            }
        }
    }

    public bool hasYellowKey
    {
        get => _hasYellowKey;
        set
        {
            if (_hasYellowKey != value)
            {
                Debug.Log($"hasYellowKey changed from {_hasYellowKey} to {value}");
                _hasYellowKey = value;
                UpdateInventory("Key_Yellow", value);
            }
        }
    }

    public bool hasPinkKey
    {
        get => _hasPinkKey;
        set
        {
            if (_hasPinkKey != value)
            {
                Debug.Log($"hasPinkKey changed from {_hasPinkKey} to {value}");
                _hasPinkKey = value;
                UpdateInventory("Key_Pink", value);
            }
        }
    }

    public bool hasRustyKey
    {
        get => _hasRustyKey;
        set
        {
            if (_hasRustyKey != value)
            {
                Debug.Log($"hasRustyKey changed from {_hasRustyKey} to {value}");
                _hasRustyKey = value;
                UpdateInventory("Key_Rusty", value);
            }
        }
    }

    public bool hasBlueKey
    {
        get => _hasBlueKey;
        set
        {
            if (_hasBlueKey != value)
            {
                Debug.Log($"hasBlueKey changed from {_hasBlueKey} to {value}");
                _hasBlueKey = value;
                UpdateInventory("Key_Blue", value);
            }
        }
    }

    public bool hasOrangeKey
    {
        get => _hasOrangeKey;
        set
        {
            if (_hasOrangeKey != value)
            {
                Debug.Log($"hasOrangeKey changed from {_hasOrangeKey} to {value}");
                _hasOrangeKey = value;
                UpdateInventory("Key_Orange", value);
            }
        }
    }

    public bool hasPurpleKey
    {
        get => _hasPurpleKey;
        set
        {
            if (_hasPurpleKey != value)
            {
                Debug.Log($"hasPurpleKey changed from {_hasPurpleKey} to {value}");
                _hasPurpleKey = value;
                UpdateInventory("Key_Purple", value);
            }
        }
    }

    public bool hasRedKey
    {
        get => _hasRedKey;
        set
        {
            if (_hasRedKey != value)
            {
                Debug.Log($"hasRedKey changed from {_hasRedKey} to {value}");
                _hasRedKey = value;
                UpdateInventory("Key_Red", value);
            }
        }
    }

    public void UpdateInventory(string itemName, bool add)
    {
        if (add)
        {
            Debug.Log($"Adding {itemName} to inventory UI.");
            Sprite icon = Resources.Load<Sprite>($"InventoryIcons/{itemName}");
            if (icon == null)
            {
                Debug.LogWarning($"Icon '{itemName}' not found in Resources/InventoryIcons/");
                return;
            }

            int slotIndex = GetAvailableSlotIndex();
            if (slotIndex == -1)
            {
                Debug.Log("No available inventory slots to add item: " + itemName);
                return;
            }

            Image parentSlot = itemSlots[slotIndex];
            if (parentSlot == null)
            {
                Debug.LogWarning($"Parent slot {slotIndex + 1} is null.");
                return;
            }

            Transform childImageTransform = parentSlot.transform.Find($"SlotInventory{slotIndex + 1}");
            if (childImageTransform == null)
            {
                Debug.LogWarning($"Child image SlotInventory{slotIndex + 1} not found!");
                return;
            }

            Image childImage = childImageTransform.GetComponent<Image>();
            if (childImage != null)
            {
                childImage.sprite = icon;
                childImage.enabled = true;
                childImage.transform.SetAsLastSibling();
                childImage.gameObject.layer = 2;
                childImage.rectTransform.anchoredPosition = Vector2.zero;

                Debug.Log($"Item {itemName} added to slot {slotIndex + 1}.");
            }
            else
            {
                Debug.LogWarning($"Child image SlotInventory{slotIndex + 1} does not have an Image component!");
            }
        }
        else
        {
            Debug.Log($"Removing {itemName} from inventory UI.");
            RemoveItemFromSlot(itemName);
        }
    }

    private int GetAvailableSlotIndex()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Transform childImageTransform = itemSlots[i].transform.Find($"SlotInventory{i + 1}");
            if (childImageTransform == null)
            {
                Debug.Log($"SlotInventory{i + 1} not found.");
                continue;
            }

            Image childImage = childImageTransform.GetComponent<Image>();
            if (childImage == null)
            {
                Debug.Log($"SlotInventory{i + 1} has no Image component.");
                continue;
            }

            if (childImage.sprite == null)
            {
                Debug.Log($"Slot {i + 1} is free.");
                return i;
            }
            else
            {
                Debug.Log($"Slot {i + 1} is occupied by {childImage.sprite.name}.");
            }
        }

        return -1;
    }

    private void RemoveItemFromSlot(string itemName)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Transform childImageTransform = itemSlots[i].transform.Find($"SlotInventory{i + 1}");
            if (childImageTransform == null)
                continue;

            Image childImage = childImageTransform.GetComponent<Image>();
            if (childImage != null && childImage.sprite != null && childImage.sprite.name == itemName)
            {
                childImage.sprite = null;
                childImage.enabled = false;
                Debug.Log($"Item {itemName} removed from slot {i + 1}.");
                break;
            }
        }
    }
}