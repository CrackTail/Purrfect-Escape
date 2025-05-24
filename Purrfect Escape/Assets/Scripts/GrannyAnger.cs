//using UnityEngine;

//public class GrannyAnger : MonoBehaviour
//{
//    public float anger = 0f;
//    public int angerLevel = 0;
//    public float maxAnger = 100f;

//    public float baseSpeed = 2f;
//    public float speedPerLevel = 0.5f;
//    public float grannySpeed;

//    public AngerBarUI angerBarUI;

//    void Start()
//    {
//        UpdateAngerLevel();
//        UpdateGrannySpeed();
//    }

//    void Update()
//    {
//        anger += Time.deltaTime * 1f; // passive increase
//        anger = Mathf.Clamp(anger, 0, maxAnger);

//        int newLevel = Mathf.FloorToInt((anger / maxAnger) * 4);
//        if (newLevel != angerLevel)
//        {
//            angerLevel = newLevel;
//            UpdateGrannySpeed();
//        }

//        if (angerBarUI != null)
//        {
//            angerBarUI.UpdateBar(anger / maxAnger);
//        }
//    }

//    public void IncreaseAnger(float amount)
//    {
//        anger += amount;
//        anger = Mathf.Clamp(anger, 0, maxAnger);
//    }

//    void UpdateGrannySpeed()
//    {
//        grannySpeed = baseSpeed + (angerLevel * speedPerLevel);
//        Debug.Log("Granny Speed: " + grannySpeed);
//    }

//    void UpdateAngerLevel()
//    {
//        angerLevel = Mathf.FloorToInt((anger / maxAnger) * 4);
//    }
//}
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
    public int objectsPerAngerLevel = 3; // you can adjust this in the Inspector
    void Start()
    {
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