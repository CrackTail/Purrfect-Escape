using UnityEngine;

public class CatStairsInteraction : MonoBehaviour
{
    public GameObject Icon_Stairs;
    private bool isNearStairs = false;

    void Update()
    {
        if (isNearStairs && Input.GetKeyDown(KeyCode.E))
        {
            UseStairs();
        }
    }

    void UseStairs()
    {
        Debug.Log("Using stairs!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Teleporter"))
        {
            isNearStairs = true;
            Icon_Stairs.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Teleporter"))
        {
            isNearStairs = false;
            Icon_Stairs.SetActive(false);
        }
    }
}