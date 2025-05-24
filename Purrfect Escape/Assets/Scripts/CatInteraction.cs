using UnityEngine;

public class CatInteraction : MonoBehaviour
{
    public GrannyAnger grannyAnger;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Breakable"))
        {
            Destroy(col.gameObject); // cat breaks the object

            if (grannyAnger != null)
            {
                grannyAnger.RegisterObjectDestroyed(); // 🔥 level-based anger
            }
        }
    }
}