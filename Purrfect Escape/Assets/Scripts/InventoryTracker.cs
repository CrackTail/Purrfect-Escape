using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool _hasFish = false;
    private bool _hasNecklace = false;
    private bool _hasAnger = false;
    private bool _hasKey = false;
    private bool _hasKeyPink = false;
    private bool _hasKeyRusty = false;
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
    public bool hasBlueKey
    {
        get => _hasBlueKey;
        set
        {
            if (!_hasBlueKey && value)
                Debug.Log("Collected Blue Key!");
            _hasBlueKey = value;
        }
    }

    public bool hasOrangeKey
    {
        get => _hasOrangeKey;
        set
        {
            if (!_hasOrangeKey && value)
                Debug.Log("Collected Orange Key!");
            _hasOrangeKey = value;
        }
    }

    public bool hasPurpleKey
    {
        get => _hasPurpleKey;
        set
        {
            if (!_hasPurpleKey && value)
                Debug.Log("Collected Purple Key!");
            _hasPurpleKey = value;
        }
    }

    public bool hasRedKey
    {
        get => _hasRedKey;
        set
        {
            if (!_hasRedKey && value)
                Debug.Log("Collected Red Key!");
            _hasRedKey = value;
        }
    }
}
