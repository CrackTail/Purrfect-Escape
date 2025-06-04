using UnityEngine;
using System.Collections.Generic;

public class PickUp : MonoBehaviour
{
    private GameObject itemToPickUp;
    private PlayerInventory inventory;
    private AudioSource audioSource;

    [System.Serializable]
    public class ItemData
    {
        public string tag;
        public GameObject iconImage;
        public AudioClip pickupSound;
    }

    [System.Serializable]
    public class KeyFalseSound
    {
        public string keyTag;
        public AudioClip soundOnFalse;
    }

    [SerializeField] private ItemData[] items;
    [SerializeField] private KeyFalseSound[] keyFalseSounds;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    private Dictionary<string, bool> keyState = new Dictionary<string, bool>();

    void Start()
    {
        inventory = FindAnyObjectByType<PlayerInventory>();
        if (inventory == null)
        {
            Debug.LogWarning("PlayerInventory component not found!");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        keyState["Key_Yellow"] = inventory.hasYellowKey;
        keyState["Key_Pink"] = inventory.hasPinkKey;
        keyState["Key_Red"] = inventory.hasRedKey;
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

                    if (item.pickupSound != null)
                        audioSource.PlayOneShot(item.pickupSound, volume);

                    Destroy(itemToPickUp);
                    itemToPickUp = null;
                    break;
                }
            }
        }

        CheckKeyFalseState("Key_Yellow", inventory.hasYellowKey);
        CheckKeyFalseState("Key_Pink", inventory.hasPinkKey);
        CheckKeyFalseState("Key_Red", inventory.hasRedKey);
    }

    void CheckKeyFalseState(string tag, bool currentState)
    {
        if (!keyState.ContainsKey(tag))
        {
            keyState[tag] = currentState;
            return;
        }

        if (keyState[tag] && !currentState)
        {
            PlayKeyFalseSound(tag);
            keyState[tag] = currentState;
        }
        else
        {
            keyState[tag] = currentState;
        }
    }

    void PlayKeyFalseSound(string tag)
    {
        foreach (var keySound in keyFalseSounds)
        {
            if (keySound.keyTag == tag && keySound.soundOnFalse != null)
            {
                audioSource.PlayOneShot(keySound.soundOnFalse, volume);
                break;
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
            case "Fish": inventory.hasFish = true; break;
            case "Key_Yellow": inventory.hasYellowKey = true; break;
            case "Key_Pink": inventory.hasPinkKey = true; break;
            case "Key_Rusty": inventory.hasRustyKey = true; break;
            case "Key_Blue": inventory.hasBlueKey = true; break;
            case "Key_Orange": inventory.hasOrangeKey = true; break;
            case "Key_Purple": inventory.hasPurpleKey = true; break;
            case "Key_Red": inventory.hasRedKey = true; break;
            case "Necklace": inventory.hasNecklace = true; break;
            case "Anger": inventory.hasAnger = true; break;
        }
    }
}