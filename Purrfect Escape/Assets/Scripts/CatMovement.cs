using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 5f;
    private Vector2 Movement;

    private void Update()
    {
        float input = Input.GetAxis("Horizontal");
        Movement.x = input * MovementSpeed * Time.deltaTime;
        transform.Translate(Movement);
    }
}
