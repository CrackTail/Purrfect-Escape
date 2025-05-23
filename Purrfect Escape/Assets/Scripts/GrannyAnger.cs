using UnityEngine;
using UnityEngine.UI;

public class GrannyAnger : MonoBehaviour
{
    public float anger = 0f;
    public int angerLevel = 0;
    public float maxAnger = 100f;

    public float baseSpeed = 2f;
    public float speedPerLevel = 0.5f;
    public float grannySpeed;

    public AngerBarUI angerBarUI;

    public Sprite[] angerSprites;             // 4 sprites, assigned in Inspector
    public Image grannyImageUI;               // Reference to the UI Image

    private int destructionCount = 0; // how many objects have been destroyed
    public int objectsPerAngerLevel = 3; // adjustable in Inspector

    private PlayerInventory inventory; // reference to player's inventory

    void Start()
    {
        // Find the player's inventory in the scene (assuming one exists)
        inventory = FindAnyObjectByType<PlayerInventory>();

        UpdateGrannySpeed();
        UpdateGrannySprite(); // Optional: show correct face at start
    }

    public void RegisterObjectDestroyed()
    {
        destructionCount++;

        if (destructionCount >= objectsPerAngerLevel && angerLevel < 4)
        {
            destructionCount = 0;
            angerLevel++;
            anger = (angerLevel / 4f) * maxAnger;

            UpdateGrannySpeed();
            UpdateGrannySprite();

            if (angerBarUI != null)
            {
                angerBarUI.UpdateBar(anger / maxAnger);
            }

            Debug.Log($"Granny anger increased! Level: {angerLevel}, Speed: {grannySpeed}");

            // Check if anger level reached max
            if (angerLevel == 4)
            {
                if (inventory != null)
                {
                    inventory.hasAnger = true;
                    Debug.Log("Anger collectible unlocked and added to inventory!");
                }
                else
                {
                    Debug.LogWarning("PlayerInventory not found, cannot update hasAnger.");
                }
            }
        }
        else
        {
            Debug.Log($"Object destroyed. {objectsPerAngerLevel - destructionCount} left until next anger level.");
        }
    }

    void UpdateGrannySpeed()
    {
        grannySpeed = baseSpeed + (angerLevel * speedPerLevel);
    }

    void UpdateGrannySprite()
    {
        if (grannyImageUI != null && angerSprites.Length > angerLevel)
        {
            grannyImageUI.sprite = angerSprites[angerLevel];
        }
    }
}