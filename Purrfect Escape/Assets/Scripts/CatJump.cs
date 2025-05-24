using UnityEngine;

public class CatJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private float JumpForce = 6;
    private float JumpCooldown = 1.0f;
    private float LastJumpTime;

    void Start()
    {
        LastJumpTime = -JumpCooldown;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && Time.time >= LastJumpTime + JumpCooldown)
        {
            Jump();
        }
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        LastJumpTime = Time.time;
    }
}