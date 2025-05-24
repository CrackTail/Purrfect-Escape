using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float interactionRange = 4f;

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
                // Check if player has the inventory component
                PlayerInventory inventory = player.GetComponent<PlayerInventory>();
                if (inventory != null && inventory.hasKey)
                {
                    TriggerWin();
                }
                else
                {
                    Debug.Log("You need the key to unlock the exit!");
                }
            }
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
