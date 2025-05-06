using TMPro;
using UnityEngine;

public class FishCounter : MonoBehaviour
{
    public TextMeshProUGUI fishText;

    public void AddFish() => fishText.text = "Inventory: Fish";
}