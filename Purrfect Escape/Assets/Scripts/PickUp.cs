using UnityEngine;
public class PickUp : MonoBehaviour
{
    private FishCounter fishCounter;

    void Start()
    {
        fishCounter = FindAnyObjectByType<FishCounter>();
        if (fishCounter == null)
            Debug.LogError("FishCounter not found in scene!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            fishCounter.AddFish();
            Destroy(other.gameObject);
        }
    }
}