using UnityEngine;

public class CatJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private float JumpForce = 6;
    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }
}
