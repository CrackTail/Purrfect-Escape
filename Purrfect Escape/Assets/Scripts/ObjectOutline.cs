using UnityEngine;

public class SimpleOutlineToggle : MonoBehaviour
{
    SpriteRenderer mainSR;
    SpriteRenderer outlineSR;

    void Start()
    {
        mainSR = GetComponent<SpriteRenderer>();
        if (mainSR == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject.");
            enabled = false;
            return;
        }

        GameObject outlineObj = new GameObject("Outline");
        outlineObj.transform.parent = transform;
        outlineObj.transform.localPosition = Vector3.zero;
        outlineObj.transform.localRotation = Quaternion.identity;
        outlineObj.transform.localScale = Vector3.one * 1.1f;

        outlineSR = outlineObj.AddComponent<SpriteRenderer>();
        outlineSR.sprite = mainSR.sprite;
        outlineSR.color = Color.white;
        outlineSR.sortingOrder = mainSR.sortingOrder - 1;
        outlineSR.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"OnTriggerEnter2D with {other.gameObject.name}");
        if (other.CompareTag("Cat") && outlineSR != null)
        {
            Debug.Log("Cat entered trigger — enabling outline");
            outlineSR.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"OnTriggerExit2D with {other.gameObject.name}");
        if (other.CompareTag("Cat") && outlineSR != null)
        {
            Debug.Log("Cat exited trigger — disabling outline");
            outlineSR.enabled = false;
        }
    }
}