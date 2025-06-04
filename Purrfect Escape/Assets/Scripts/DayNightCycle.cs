using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayAndNightCycle : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float dayIntensity = 1.0f;
    [SerializeField] private float nightIntensity = 0.3f;
    [SerializeField] private float cycleDurationMinutes = 15f;

    private float cycleDurationSeconds;
    private float t;
    private bool reverse;

    private void Start()
    {
        if (globalLight == null)
        {
            enabled = false;
            return;
        }

        cycleDurationSeconds = cycleDurationMinutes * 60f;
    }

    private void Update()
    {
        t += (reverse ? -1 : 1) * Time.deltaTime;
        t = Mathf.Clamp(t, 0, cycleDurationSeconds);
        globalLight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, t / cycleDurationSeconds);
        if (t == 0 || t == cycleDurationSeconds) reverse = !reverse;
    }
}