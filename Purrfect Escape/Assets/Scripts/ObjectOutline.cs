using UnityEngine;

public class OutlineEffectController : MonoBehaviour
{
    public GameObject outlineChild;
    public Material whiteMaterial;

    private SpriteRenderer outlineSpriteRenderer;
    private Material originalMaterial;

    private void Start()
    {
        outlineSpriteRenderer = outlineChild.GetComponent<SpriteRenderer>();
        originalMaterial = outlineSpriteRenderer.material;
        outlineSpriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            outlineSpriteRenderer.enabled = true;
            outlineSpriteRenderer.material = whiteMaterial;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            outlineSpriteRenderer.enabled = false;
            outlineSpriteRenderer.material = originalMaterial;
        }
    }
}