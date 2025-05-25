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
    [Header("Jump Settings")]
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private float JumpForce = 6f;

    [Header("Ground Detection")]
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float GroundCheckRadius = 0.1f;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask PlatformLayer;

    private bool isGrounded;

    void Update()
    {
        CheckIfGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(DisablePlatformCollisionTemporarily());
        }
    }

    private void CheckIfGrounded()
    {
        // Combine both ground and platform layers for ground check
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer | PlatformLayer);
    }

    private void Jump()
    {
        // Detect if standing on a platform
        bool onPlatform = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, PlatformLayer);
        bool onFloor = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer & ~PlatformLayer);

        // Adjust jump force if needed
        float appliedJumpForce = JumpForce;
        if (onPlatform)
        {
            appliedJumpForce += 0.5f; // tweak this value if needed
        }

        // Apply jump
        Rigidbody2D.linearVelocity = new Vector2(Rigidbody2D.linearVelocity.x, appliedJumpForce);
    }

    private System.Collections.IEnumerator DisablePlatformCollisionTemporarily()
    {
        int platformLayerIndex = LayerMaskToLayer(PlatformLayer);

        // Temporarily disable collisions with the platform layer
        Physics2D.IgnoreLayerCollision(gameObject.layer, platformLayerIndex, true);
        yield return new WaitForSeconds(0.5f); // enough time to fall through
        Physics2D.IgnoreLayerCollision(gameObject.layer, platformLayerIndex, false);
    }

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

    private void OnDrawGizmosSelected()
    {
        // Optional: visualize the GroundCheck circle in the editor
        if (GroundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
        }
    }
}