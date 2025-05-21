using UnityEngine;

public class GrannyAnger : MonoBehaviour
{
    public float anger = 0f;
    public int angerLevel = 0;
    public float maxAnger = 100f;

    public float baseSpeed = 2f;
    public float speedPerLevel = 0.5f;
    public float grannySpeed;

    public AngerBarUI angerBarUI;

    void Start()
    {
        UpdateAngerLevel();
        UpdateGrannySpeed();
    }

    void Update()
    {
        anger += Time.deltaTime * 1f; // passive increase
        anger = Mathf.Clamp(anger, 0, maxAnger);

        int newLevel = Mathf.FloorToInt((anger / maxAnger) * 4);
        if (newLevel != angerLevel)
        {
            angerLevel = newLevel;
            UpdateGrannySpeed();
        }

        if (angerBarUI != null)
        {
            angerBarUI.UpdateBar(anger / maxAnger);
        }
    }

    public void IncreaseAnger(float amount)
    {
        anger += amount;
        anger = Mathf.Clamp(anger, 0, maxAnger);
    }

    void UpdateGrannySpeed()
    {
        grannySpeed = baseSpeed + (angerLevel * speedPerLevel);
        Debug.Log("Granny Speed: " + grannySpeed);
    }

    void UpdateAngerLevel()
    {
        angerLevel = Mathf.FloorToInt((anger / maxAnger) * 4);
    }
}