using System.Runtime.CompilerServices;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 5f;
    private Vector2 Movement;
    private bool FacingRight = true;

    private void Update()
    {
        float input = Input.GetAxis("Horizontal");
        Movement.x = input * MovementSpeed * Time.deltaTime;
        transform.Translate(Movement);
        if (Movement.x > 0 && !FacingRight)
        {
            Flip();
        }
        else if (Movement.x < 0 && FacingRight)
        {
            Flip();
        }
    }
        void Flip()
        {
            FacingRight = !FacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
}
