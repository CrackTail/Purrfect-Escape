using UnityEngine;
using System.Collections;

public class CatMovement : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 5f;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private LayerMask PlatformLayer;
    [SerializeField] private float DropThroughTime = 0.5f;

    private Vector2 Movement;
    private bool FacingRight = true;
    private Animator animatormain;
    private SpriteRenderer rend;
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private bool CanHide = false;

    private void Start()
    {
        animatormain = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        float input = Input.GetAxis("Horizontal");
        Movement.x = input * MovementSpeed * Time.deltaTime;
        transform.Translate(Movement);
        animatormain.SetBool("isRunning", input != 0);

        if (Movement.x > 0 && !FacingRight)
            Flip();
        else if (Movement.x < 0 && FacingRight)
            Flip();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(DropThrough());
        }

        if (CanHide && Input.GetKey(KeyCode.R))
        {
            Physics2D.IgnoreLayerCollision(12, 13, true);
            rend.sortingOrder = 0;
        }
        else
        {
            Physics2D.IgnoreLayerCollision(12, 13, false);
            rend.sortingOrder = 12;
        }
    }

    private IEnumerator DropThrough()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), true);
        yield return new WaitForSeconds(DropThroughTime);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), false);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, PlatformLayer);
        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HidingObject"))
            CanHide = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HidingObject"))
            CanHide = false;
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}