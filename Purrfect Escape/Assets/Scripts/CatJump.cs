//using UnityEngine;

//public class CatJump : MonoBehaviour
//{
//    [SerializeField] private Rigidbody2D Rigidbody2D;
//    [SerializeField] private float JumpForce = 6;
//    private float JumpCooldown = 1.0f;
//    private float LastJumpTime;

//    void Start()
//    {
//        LastJumpTime = -JumpCooldown;
//    }

//    void Update()
//    {
//        if (Input.GetButtonDown("Jump") && Time.time >= LastJumpTime + JumpCooldown)
//        {
//            Jump();
//        }
//    }
//    private void Jump()
//    {
//        Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
//        LastJumpTime = Time.time;
//    }
//}

using UnityEngine;

public class CatJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private float JumpForce = 6f;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float GroundCheckRadius = 0.1f;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask PlatformLayer; // Add this

    private bool isGrounded;

    void Update()
    {
        CheckIfGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Press S to drop through platforms
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(DisablePlatformCollisionTemporarily());
        }
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);
    }

    private void Jump()
    {
        Rigidbody2D.linearVelocity = new Vector2(Rigidbody2D.linearVelocity.x, JumpForce);
    }

    private System.Collections.IEnumerator DisablePlatformCollisionTemporarily()
    {
        // Disable collision with Platform layer (but not floor)
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMaskToLayer(PlatformLayer), true);
        yield return new WaitForSeconds(0.5f); // enough time to fall through
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMaskToLayer(PlatformLayer), false);
    }

    // Helper to get layer number from LayerMask
    private int LayerMaskToLayer(LayerMask mask)
    {
        int layer = 0;
        int layerMask = mask.value;
        while (layerMask > 1)
        {
            layerMask >>= 1;
            layer++;
        }
        return layer;
    }
}