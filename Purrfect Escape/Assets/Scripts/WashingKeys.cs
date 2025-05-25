using UnityEngine;

public class WashingKeys : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float interactionRange = 4f;

    [SerializeField] private GameObject interactionIconPrefab;

    private GameObject player;
    private GameObject interactionIconInstance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        bool isInRange = distance <= interactionRange;
        if (isInRange)
        {
            if (interactionIconInstance == null && interactionIconPrefab != null)
            {
                interactionIconInstance = Instantiate(interactionIconPrefab, player.transform);
                interactionIconInstance.transform.localPosition = new Vector3(0, 1.5f, 0);
            }
            if (distance <= interactionRange)
            {
                if (Input.GetKeyDown(interactionKey))
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
            else
            {
                if (interactionIconInstance != null)
                {
                    Destroy(interactionIconInstance);
                }
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
            Debug.Log("Your Rusty Key turned into an Orange Key!");
        }
        else if (roll < 0.5f)
        {
            inventory.hasBlueKey = true;
            Debug.Log("Your Rusty Key turned into a Blue Key!");
        }
        else if (roll < 0.75f)
        {
            inventory.hasPurpleKey = true;
            Debug.Log("Your Rusty Key turned into a Purple Key!");
        }
        else
        {
            inventory.hasRedKey = true;
            Debug.Log("Your Rusty Key turned into a Red Key!");
        }
        if (interactionIconInstance != null)
        {
            Destroy(interactionIconInstance);
        }
    }
}
