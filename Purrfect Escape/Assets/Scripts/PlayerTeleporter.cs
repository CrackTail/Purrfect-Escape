using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    private GameObject currentTeleporter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentTeleporter != null)
            transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
    }

    private void OnTriggerEnter2D(Collider2D collision) =>
        currentTeleporter = collision.CompareTag("Teleporter") ? collision.gameObject : currentTeleporter;

    private void OnTriggerExit2D(Collider2D collision) =>
        currentTeleporter = collision.CompareTag("Teleporter") && collision.gameObject == currentTeleporter ? null : currentTeleporter;
}