using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameObject itemToPickUp;
    private PlayerInventory inventory;

    [SerializeField] private GameObject Fish_Icon_Image;
    [SerializeField] private GameObject Key_Icon_Image;

    void Start()
    {
        inventory = FindAnyObjectByType<PlayerInventory>();
    }

    void Update()
    {
        if (itemToPickUp && Input.GetKeyDown(KeyCode.E))
        {
            if (itemToPickUp.CompareTag("Fish"))
            {
                inventory.hasFish = true;
                if (Fish_Icon_Image != null)
                {
                    Fish_Icon_Image.SetActive(true);
                    Debug.Log("Fish registered");
                }
                Destroy(itemToPickUp);
            }
            else if (itemToPickUp.CompareTag("Key"))
            {
                inventory.hasKey = true;
                if (Key_Icon_Image != null)
                {
                    Key_Icon_Image.SetActive(true);
                    Debug.Log("Key registered");
                }
                Destroy(itemToPickUp);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            itemToPickUp = other.gameObject;
        }
        else if (other.CompareTag("Key"))
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
