using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlatformColorer : MonoBehaviour
{
    public Material material;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (material != null)
        {
            spriteRenderer.material = material;
        }
        else
        {
            Debug.LogWarning("Material is not assigned on " + gameObject.name);
        }
    }
}