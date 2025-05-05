using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    float StartTimeValue=3600f;
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float remainingTime = StartTimeValue - elapsedTime;

        if (remainingTime < 0f)remainingTime = 0f;
        int minutes=Mathf.FloorToInt(remainingTime/60);
        int seconds=Mathf.FloorToInt(remainingTime%60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes, seconds);
    }
}