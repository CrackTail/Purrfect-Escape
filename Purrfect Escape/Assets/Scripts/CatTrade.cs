using UnityEngine;

public class CatTrade : MonoBehaviour
{
    public CatInteraction catInteractionScript;
    public GameObject keyPrefab;
    public Transform keySpawnPoint;
    public InventoryManager inventoryManager;

    public void GiveFish()
    {
        if (inventoryManager.HasItem("Fish"))
        {
            inventoryManager.RemoveItem("Fish");
            inventoryManager.AddItem("Key");
            catInteractionScript.ReceiveFish();

            // Optionally instantiate a physical key object
            Instantiate(keyPrefab, keySpawnPoint.position, Quaternion.identity);
        }
    }
}
