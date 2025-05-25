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

    public Sprite[] angerSprites;
    public Image grannyImageUI;

    private int destructionCount = 0;
    public int objectsPerAngerLevel = 3;

    private PlayerInventory inventory;

    void Start()
    {
        inventory = FindAnyObjectByType<PlayerInventory>();
        UpdateGrannySpeed();
        UpdateGrannySprite();
    }

    public void RegisterObjectDestroyed()
    {
        destructionCount++;

        int newLevel = Mathf.FloorToInt(destructionCount / objectsPerAngerLevel);
        newLevel = Mathf.Clamp(newLevel, 0, angerSprites.Length - 1);

        if (newLevel > angerLevel)
        {
            angerLevel = newLevel;
            anger = (angerLevel / (float)(angerSprites.Length - 1)) * maxAnger;

            UpdateGrannySpeed();
            UpdateGrannySprite();

            if (angerBarUI != null)
            {
                angerBarUI.UpdateBar(anger / maxAnger);
            }

            Debug.Log($"Granny anger increased! Level: {angerLevel}, Speed: {grannySpeed}");

            if (angerLevel == 3)
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
            int remaining = objectsPerAngerLevel - (destructionCount % objectsPerAngerLevel);
            Debug.Log($"Object destroyed. {remaining} more until next anger level.");
        }
    }

    void UpdateGrannySpeed()
    {
        grannySpeed = baseSpeed + (angerLevel * speedPerLevel);
    }

    void UpdateGrannySprite()
    {
        if (grannyImageUI != null && angerLevel < angerSprites.Length)
        {
            grannyImageUI.sprite = angerSprites[angerLevel];
        }
    }
}