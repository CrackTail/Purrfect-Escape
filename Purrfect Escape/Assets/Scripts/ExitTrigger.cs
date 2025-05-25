//using UnityEngine;

//public class ExitDoor : MonoBehaviour
//{
//    [SerializeField] private GameObject gameWinPanel;
//    [SerializeField] private KeyCode interactionKey = KeyCode.E;
//    [SerializeField] private float interactionRange = 4f;

//    private GameObject player;

//    private void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player");
//        if (gameWinPanel != null)
//            gameWinPanel.SetActive(false);
//    }

//    private void Update()
//    {
//        if (player == null) return;

//        float distance = Vector2.Distance(transform.position, player.transform.position);
//        if (distance <= interactionRange)
//        {
//            if (Input.GetKeyDown(interactionKey))
//            {
//                // Check if player has the inventory component
//                PlayerInventory inventory = player.GetComponent<PlayerInventory>();
//                if (inventory != null && inventory.hasKey)
//                {
//                    TriggerWin();
//                }
//                else
//                {
//                    Debug.Log("You need the key to unlock the exit!");
//                }
//            }
//        }
//    }

//    private void TriggerWin()
//    {
//        Time.timeScale = 0f;
//        if (gameWinPanel != null)
//            gameWinPanel.SetActive(true);
//        Debug.Log("You Win!");
//    }
//}
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float interactionRange = 4f;

    [SerializeField] private KeyType requiredKey = KeyType.GoldKey; // Choose which key unlocks the door!

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (gameWinPanel != null)
            gameWinPanel.SetActive(false);
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= interactionRange)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                PlayerInventory inventory = player.GetComponent<PlayerInventory>();
                if (inventory != null && HasRequiredKey(inventory))
                {
                    TriggerWin();
                }
                else
                {
                    Debug.Log($"You need the {requiredKey} to unlock the exit!");
                }
            }
        }
    }

    private bool HasRequiredKey(PlayerInventory inventory)
    {
        switch (requiredKey)
        {
            case KeyType.GoldKey: return inventory.hasKey;
            case KeyType.PinkKey: return inventory.hasKeyPink;
            case KeyType.RustyKey: return inventory.hasRustyKey;
            case KeyType.BlueKey: return inventory.hasBlueKey;
            case KeyType.OrangeKey: return inventory.hasOrangeKey;
            case KeyType.PurpleKey: return inventory.hasPurpleKey;
            case KeyType.RedKey: return inventory.hasRedKey;
            default: return false;
        }
    }

    private void TriggerWin()
    {
        Time.timeScale = 0f;
        if (gameWinPanel != null)
            gameWinPanel.SetActive(true);
        Debug.Log("You Win!");
    }
}

public enum KeyType
{
    GoldKey,
    PinkKey,
    RustyKey,
    BlueKey,
    OrangeKey,
    PurpleKey,
    RedKey
}