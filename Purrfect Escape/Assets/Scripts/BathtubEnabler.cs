using UnityEngine;

public class EnableObjectsOnInteraction : MonoBehaviour
{
    public string interactableTag = "Bathtub";

    [SerializeField] private GameObject[] objectsToEnable = new GameObject[4];
    [SerializeField] private float[] enableDelays = new float[4];

    [SerializeField] private GameObject[] objectsToDisableOnToggle;
    [SerializeField] private GameObject[] objectsToKeepEnabledOnToggle;

    private bool playerInRange = false;
    private GameObject currentInteractable;
    private bool isInEnabledState = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null && currentInteractable.CompareTag(interactableTag))
            {
                if (!isInEnabledState)
                {
                    StartEnablingSequence();
                    isInEnabledState = true;
                }
                else
                {
                    HandleToggleOff();
                    isInEnabledState = false;
                }
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
        if (obj != null)
        {
            obj.SetActive(true);
        }
    }

    private void HandleToggleOff()
    {
        foreach (GameObject obj in objectsToDisableOnToggle)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        foreach (GameObject obj in objectsToKeepEnabledOnToggle)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }
}