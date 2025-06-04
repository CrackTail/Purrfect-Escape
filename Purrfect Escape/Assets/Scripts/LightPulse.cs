using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightPulse : MonoBehaviour
{
    [Header("Pulse Settings")]
    [SerializeField] private float minInnerRadius = 0f;
    [SerializeField] private float maxInnerRadius = 0.5f;
    [SerializeField] private float pulseSpeed = 2f;

    private Light2D light2D;
    private float pulseTimer;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        pulseTimer = Random.Range(0f, 2f * Mathf.PI);
    }

    private void Update()
    {
        pulseTimer += Time.deltaTime * pulseSpeed;
        float t = (Mathf.Sin(pulseTimer) + 1f) / 2f;
        light2D.pointLightInnerRadius = Mathf.Lerp(minInnerRadius, maxInnerRadius, t);
    }
}