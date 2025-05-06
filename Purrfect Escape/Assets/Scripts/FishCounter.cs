using TMPro;
using UnityEngine;

public class FishCounter : MonoBehaviour
{
    public TextMeshProUGUI fishText;
    private int fishCount = 0;

    public void AddFish()
    {
        fishCount++;
        fishText.text = "Fish: " + fishCount;
    }
}