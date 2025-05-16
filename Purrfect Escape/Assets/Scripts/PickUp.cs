using UnityEngine;

public class PickUp : MonoBehaviour
{
    private FishCounter fishCounter;
    private GameObject itemToPickUp;

    void Start() => fishCounter = FindAnyObjectByType<FishCounter>();

    void Update()
    {
        if (itemToPickUp && Input.GetKeyDown(KeyCode.E))
        {
            if (itemToPickUp.CompareTag("Fish"))
            {
                fishCounter.AddFish();
            }
            else if (itemToPickUp.CompareTag("Key"))
            {
                GameObject door = GameObject.FindWithTag("LockedDoor");
                if (door != null)
                {
                    Destroy(door);
                }
            }

            Destroy(itemToPickUp);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish") || other.CompareTag("Key"))
        {
            itemToPickUp = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == itemToPickUp)
        {
            itemToPickUp = null;
        }
    }
}