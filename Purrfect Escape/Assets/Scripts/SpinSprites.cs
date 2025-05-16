using UnityEngine;

public class FloatAndSpinIcon : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 50f;
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatHeight = 0.5f;
    [SerializeField] private float startDelayMin = 0f;
    [SerializeField] private float startDelayMax = 1f;

    private bool active = false;
    private float startY;
    private float floatOffset;

    void Start()
    {
        startY = transform.localPosition.y;
        floatOffset = Random.Range(0f, Mathf.PI * 2f);
        float delay = Random.Range(startDelayMin, startDelayMax);
        Invoke(nameof(StartEffect), delay);
    }
    void StartEffect()
    {
        active = true;
    }

    void Update()
    {
        if (!active) return;
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
        float newY = startY + Mathf.Sin(Time.time * floatSpeed + floatOffset) * floatHeight;
        Vector3 pos = transform.localPosition;
        pos.y = newY;
        transform.localPosition = pos;
    }
}