using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostExposureFade : MonoBehaviour
{
    public Volume volume;
    ColorAdjustments ca;
    float duration = 1800f;
    float t;
    bool reverse;

    void Start()
    {
        volume ??= GetComponent<Volume>();
        if (!volume.profile.TryGet(out ca)) enabled = false;
    }

    void Update()
    {
        t += (reverse ? -1 : 1) * Time.deltaTime;
        t = Mathf.Clamp(t, 0, duration);
        ca.postExposure.Override(Mathf.Lerp(0, -4, t / duration));
        if (t == 0 || t == duration) reverse = !reverse;
    }
}