using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    private GameObject currentTeleporter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentTeleporter != null)
            transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
    }
    public void Interact(GameObject interactor)
    {
        Debug.Log($"{interactor.name} interacted with {gameObject.name}");
    }
    private void OnTriggerEnter2D(Collider2D collision) =>
        currentTeleporter = collision.CompareTag("Teleporter") ? collision.gameObject : currentTeleporter;

    private void OnTriggerExit2D(Collider2D collision) =>
        currentTeleporter = collision.CompareTag("Teleporter") && collision.gameObject == currentTeleporter ? null : currentTeleporter;
}