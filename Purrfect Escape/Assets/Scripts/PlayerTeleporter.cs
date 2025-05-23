using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    private GameObject currentTeleporter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentTeleporter != null)
        {
            Teleporter teleporter = currentTeleporter.GetComponent<Teleporter>();
            if (teleporter != null && teleporter.GetDestination() != null)
            {
                transform.position = teleporter.GetDestination().position;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
            currentTeleporter = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter") && collision.gameObject == currentTeleporter)
            currentTeleporter = null;
    }
    public void Interact(GameObject interactor, GameObject teleporterObject)
    {
        Teleporter teleporter = teleporterObject.GetComponent<Teleporter>();
        if (teleporter != null && teleporter.GetDestination() != null)
        {
            interactor.transform.position = teleporter.GetDestination().position;
        }
    }
}