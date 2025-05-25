using UnityEngine;

public class CatIconStairsAbove : MonoBehaviour
{
    public GameObject Icon_Stairs;
    public GameObject Icon_Hide;
    public GameObject Icon_Cleaning;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Teleporter"))
        {
            Icon_Stairs.SetActive(true);
        }
        if (other.CompareTag("HidingObject"))
        {
            Icon_Hide.SetActive(true);
        }
        if (other.CompareTag("Cleaning"))
        {
            Icon_Cleaning.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Teleporter"))
        {
            Icon_Stairs.SetActive(false);
        }
        if (other.CompareTag("HidingObject"))
        {
            Icon_Hide.SetActive(false);
        }
        if (other.CompareTag("Cleaning"))
        {
            Icon_Cleaning.SetActive(false);
        }
    }
}