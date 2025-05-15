using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public TextMeshProUGUI inventoryText;
    private List<string> items = new List<string>();

    public void AddItem(string item)
    {
        items.Add(item);
        UpdateInventoryText();
    }

    public void RemoveItem(string item)
    {
        items.Remove(item);
        UpdateInventoryText();
    }

    public bool HasItem(string item)
    {
        return items.Contains(item);
    }

    void UpdateInventoryText()
    {
        if (items.Contains("Key"))
        {
            inventoryText.text = "Inventory: Key";
        }
        else
        {
            inventoryText.text = "Inventory: " + string.Join(", ", items);
        }
    }
}