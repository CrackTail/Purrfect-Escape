using UnityEngine;

public class EnableObjectsOnInteraction : MonoBehaviour
{
    [Header("Target to interact with")]
    [Tooltip("Tag of the interactable object (e.g. 'Bathtub')")]
    public string interactableTag = "Bathtub";

    [Header("Objects to Enable")]
    [SerializeField] private GameObject[] objectsToEnable = new GameObject[3];

    [Header("Enable Timings (in seconds)")]
    [Tooltip("Specify the delay in seconds for each object to be enabled.")]
    [SerializeField] private float[] enableDelays = new float[3];

    private bool playerInRange = false;
    private GameObject currentInteractable;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null && currentInteractable.CompareTag(interactableTag))
            {
                Debug.Log("E pressed on Bathtub. Starting object enabling sequence...");
                StartEnablingSequence();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag(interactableTag))
        {
            playerInRange = true;
            currentInteractable = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            currentInteractable = null;
        }
    }

    private void StartEnablingSequence()
    {
        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            if (objectsToEnable[i] != null)
            {
                float delay = (i < enableDelays.Length) ? enableDelays[i] : 0f;
                StartCoroutine(EnableAfterDelay(objectsToEnable[i], delay));
            }
        }
    }

    private System.Collections.IEnumerator EnableAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
        Debug.Log($"Enabled {obj.name} after {delay} seconds.");
    }
}