using UnityEngine;

public class CatInteraction : MonoBehaviour
{
    public GrannyAnger grannyAnger; // Drag this in Inspector!
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Breakable"))
        {
            if (grannyAnger != null)
            {
                grannyAnger.IncreaseAnger(20f); // ✅ Increase anger
            }
            else
            {
                Debug.LogWarning("GrannyAnger reference not set on CatInteraction!");
            }
        }
    }
}