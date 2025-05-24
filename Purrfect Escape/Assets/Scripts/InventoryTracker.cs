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
            if (!_hasFish && value)
                Debug.Log("Collected Fish!");
            _hasFish = value;
        }
    }

    public bool hasNecklace
    {
        get => _hasNecklace;
        set
        {
            if (!_hasNecklace && value)
                Debug.Log("Collected Necklace!");
            _hasNecklace = value;
        }
    }

    public bool hasAnger
    {
        get => _hasAnger;
        set
        {
            if (!_hasAnger && value)
                Debug.Log("Collected Anger!");
            _hasAnger = value;
        }
    }

    public bool hasKey
    {
        get => _hasKey;
        set
        {
            if (!_hasKey && value)
                Debug.Log("Collected Gold Key!");
            _hasKey = value;
        }
    }

    public bool hasKeyPink
    {
        get => _hasKeyPink;
        set
        {
            if (!_hasKeyPink && value)
                Debug.Log("Collected Pink Key!");
            _hasKeyPink = value;
        }
    }

    public bool hasRustyKey
    {
        get => _hasKeyRusty;
        set
        {
            if (!_hasKeyRusty && value)
                Debug.Log("Collected Rusty Key!");
            _hasKeyRusty = value;
        }
    }
}