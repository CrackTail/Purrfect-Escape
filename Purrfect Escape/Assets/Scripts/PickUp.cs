using UnityEngine;

public class PickUp : MonoBehaviour
{
    private FishCounter fishCounter;
    private GameObject fishToDestroy;
    void Start() => fishCounter = FindAnyObjectByType<FishCounter>();
    void Update()
    {
        if (fishToDestroy && Input.GetKeyDown(KeyCode.E))
        {
            fishCounter.AddFish();
            Destroy(fishToDestroy);
            GameObject door = GameObject.FindWithTag("LockedDoor");
            if (door != null)
            {
                Destroy(door);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) => fishToDestroy = other.CompareTag("Fish") ? other.gameObject : null;
    void OnTriggerExit2D(Collider2D other) => fishToDestroy = other.CompareTag("Fish") ? null : fishToDestroy;
}