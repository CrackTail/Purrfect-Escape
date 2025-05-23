using UnityEngine;

public class GrannyAI : MonoBehaviour
{
    public GameObject pointA, pointB, gameOverPanel;
    public float speed, newPointAX;
    private bool doorMoved = false;
    private Transform currentPoint;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float InteractionRange = 6f;
    public LayerMask teleporterLayer;
    public GrannyAnger grannyAnger; // Add this near the top
    private bool justTeleported = false;
    [SerializeField] private float teleportCooldown = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointA.transform;
        // anim.SetBool("isRunning", true); when we got animation ill use this, don't delete.
    }

    void Update()
    {
        float moveDirection = currentPoint == pointB.transform ? 1 : -1;
        //rb.linearVelocity = new Vector2(moveDirection * speed, 0);
        rb.linearVelocity = new Vector2(moveDirection * grannyAnger.grannySpeed, 0);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, InteractionRange,teleporterLayer);

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            currentPoint = currentPoint == pointB.transform ? pointA.transform : pointB.transform;
            Flip(moveDirection);
        }

        if (!doorMoved && GameObject.FindWithTag("LockedDoor") == null)
        {
            doorMoved = true;
            pointA.transform.position = new Vector3(newPointAX, pointA.transform.position.y, pointA.transform.position.z);
        }
        if (!justTeleported)
        {
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Teleporter"))
                {
                    PlayerTeleporter teleporterScript = FindFirstObjectByType<PlayerTeleporter>();
                    if (teleporterScript != null)
                    {
                        teleporterScript.Interact(gameObject, hit.gameObject);
                        justTeleported = true;
                        Invoke(nameof(ResetTeleportFlag), teleportCooldown);
                        break;
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            gameOverPanel.SetActive(true);
    }

    void Flip(float moveDirection)
    {
        Vector3 localScale = transform.localScale;
        localScale.x = moveDirection > 0 ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, InteractionRange);
    }
    private void ResetTeleportFlag()
    {
        justTeleported = false;
    }
}