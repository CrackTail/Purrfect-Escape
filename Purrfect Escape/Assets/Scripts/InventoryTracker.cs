using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Image[] itemSlots = new Image[2];
    [SerializeField] private Vector2[] slotCoordinates = new Vector2[2];

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

    public bool hasFish
    {
        get => _hasFish;
        set
        {
            if (_hasFish != value)
            {
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
                _hasAnger = value;
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
                _hasRedKey = value;
                UpdateInventory("Key_Red", value);
            }
        }
    }

    private void UpdateInventory(string itemName, bool add)
    {
        if (add)
        {
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
            }
            else
            {
                Debug.LogWarning($"Child image SlotInventory{slotIndex + 1} does not have an Image component!");
            }
        }
        else
        {
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
            if (childImageTransform == null) continue;

            Image childImage = childImageTransform.GetComponent<Image>();
            if (childImage != null && childImage.sprite != null && childImage.sprite.name == itemName)
            {
                childImage.sprite = null;
                childImage.enabled = false;
                break;
            }
        }
    }
}