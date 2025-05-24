using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool _hasFish = false;
    private bool _hasPendant = false;
    private bool _hasAnger = false;
    private bool _hasKey = false;
    private bool _hasPinkKey = false;
    private bool _hasRustyKey = false;

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

    public bool hasPendant
    {
        get => _hasPendant;
        set
        {
            if (!_hasPendant && value)
                Debug.Log("Collected Pendant!");
            _hasPendant = value;
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

    public bool hasPinkKey
    {
        get => _hasPinkKey;
        set
        {
            if (!_hasPinkKey && value)
                Debug.Log("Collected Pink Key!");
            _hasPinkKey = value;
        }
    }

    public bool hasRustyKey
    {
        get => _hasRustyKey;
        set
        {
            if (!_hasRustyKey && value)
                Debug.Log("Collected Rusty Key!");
            _hasRustyKey = value;
        }
    }
}