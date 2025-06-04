using UnityEngine;

public class WashingKeys : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float interactionRange = 4f;
    [SerializeField] private AudioClip washSound;
    [SerializeField, Min(0f)] private float washSoundVolume = 1f;

    private GameObject player;
    private AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= interactionRange && Input.GetKeyDown(interactionKey))
        {
            PlayerInventory inventory = player.GetComponent<PlayerInventory>();
            if (inventory != null && inventory.hasRustyKey)
            {
                TransformRustyKey(inventory);
            }
            else
            {
                Debug.Log("You need the Rusty Key to use this.");
            }
        }
    }

    private void TransformRustyKey(PlayerInventory inventory)
    {
        inventory.hasRustyKey = false;

        float roll = Random.value;

        if (roll < 0.25f)
        {
            inventory.hasOrangeKey = true;
            Debug.Log("Rusty Key got turned into Orange Key");
        }
        else if (roll < 0.5f)
        {
            inventory.hasBlueKey = true;
            Debug.Log("Rusty Key got turned into Blue Key");
        }
        else if (roll < 0.75f)
        {
            inventory.hasPurpleKey = true;
            Debug.Log("Rusty Key got turned into Purple Key");
        }
        else
        {
            inventory.hasRedKey = true;
            Debug.Log("Rusty Key got turned into Red Key");
        }

        if (washSound != null)
        {
            audioSource.PlayOneShot(washSound, washSoundVolume);
        }
    }
}