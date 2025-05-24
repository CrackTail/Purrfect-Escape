using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool _hasFish = false;
    private bool _hasNecklace = false;
    private bool _hasAnger = false;
    private bool _hasKey = false;
    private bool _hasKeyPink = false;
    private bool _hasKeyRusty = false;

    public bool hasFish
    {
        get => _hasFish;
        set
        {
            if (_hasFish != value)
                Debug.Log($"Inventory updated: Fish = {value}");
            _hasFish = value;
        }
    }

    public bool hasNecklace
    {
        get => _hasNecklace;
        set
        {
            if (_hasNecklace != value)
                Debug.Log($"Inventory updated: Necklace = {value}");
            _hasNecklace = value;
        }
    }

    public bool hasAnger
    {
        get => _hasAnger;
        set
        {
            if (_hasAnger != value)
                Debug.Log($"Inventory updated: Anger = {value}");
            _hasAnger = value;
        }
    }

    public bool hasKey
    {
        get => _hasKey;
        set
        {
            if (_hasKey != value)
                Debug.Log($"Inventory updated: Gold Key = {value}");
            _hasKey = value;
        }
    }

    public bool hasKeyPink
    {
        get => _hasKeyPink;
        set
        {
            if (_hasKeyPink != value)
                Debug.Log($"Inventory updated: Pink Key = {value}");
            _hasKeyPink = value;
        }
    }

    public bool hasRustyKey
    {
        get => _hasKeyRusty;
        set
        {
            if (_hasKeyRusty != value)
                Debug.Log($"Inventory updated: Rusty Key = {value}");
            _hasKeyRusty = value;
        }
    }
}
