using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraDimControl : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float dimAmount = 0.5f;

    private GameObject dimOverlay;
    private bool isDimActive = false;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        dimOverlay = new GameObject("DimOverlay");
        dimOverlay.transform.SetParent(transform);
        dimOverlay.transform.localPosition = new Vector3(0, 0, 1);
        var sr = dimOverlay.AddComponent<SpriteRenderer>();
        sr.sprite = CreateSprite();
        sr.color = new Color(0, 0, 0, dimAmount);
        sr.sortingOrder = 1000;
        float h = cam.orthographicSize * 2;
        float w = h * cam.aspect;
        dimOverlay.transform.localScale = new Vector3(w * 4, h * 4, 1);
        dimOverlay.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isDimActive = !isDimActive;
            dimOverlay.SetActive(isDimActive);
        }

        if (dimOverlay.activeSelf)
        {
            var r = dimOverlay.GetComponent<SpriteRenderer>();
            r.color = new Color(0, 0, 0, dimAmount);
        }
    }

    Sprite CreateSprite()
    {
        var tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
    }
}