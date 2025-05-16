using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameObject itemToPickUp;

    [SerializeField] private GameObject Fish_Icon_Image;
    [SerializeField] private GameObject Key_Icon_Image;


    void Update()
    {
        if (itemToPickUp && Input.GetKeyDown(KeyCode.E))
        {
            if (itemToPickUp.CompareTag("Fish"))
            {
                if (Fish_Icon_Image != null) Fish_Icon_Image.SetActive(true);
            }
            else if (itemToPickUp.CompareTag("Key"))
            {
                GameObject door = GameObject.FindWithTag("LockedDoor");
                if (door != null) Destroy(door);
                if (Key_Icon_Image != null) Key_Icon_Image.SetActive(true);
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