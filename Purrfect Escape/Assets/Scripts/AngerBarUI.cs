using UnityEngine;
using UnityEngine.UI;

public class AngerBarUI : MonoBehaviour
{
    public Slider angerSlider;

    void Awake()
    {
        if (angerSlider == null)
            angerSlider = GetComponent<Slider>();
    }

    public void UpdateBar(float fillAmount)
    {
        if (angerSlider != null)
            angerSlider.value = fillAmount;
    }
}
