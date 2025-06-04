using UnityEngine;

public class CatInteraction : MonoBehaviour
{
    public GrannyAnger grannyAnger;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Breakable"))
        {
            Destroy(col.gameObject);

            if (grannyAnger != null)
            {
                grannyAnger.RegisterObjectDestroyed();
            }
        }
    }

}