using UnityEngine;

public class AutoDeactivate : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterDelay(1.5f));
    }

    private System.Collections.IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}