using UnityEngine;

public class GrannyAI : MonoBehaviour
{
    public GameObject pointA, pointB, gameOverPanel;
    public float speed, newPointAX;
    private bool doorMoved = false;
    private Transform currentPoint;
    private Rigidbody2D rb;
    private Animator anim;

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
        rb.linearVelocity = new Vector2(moveDirection * speed, 0);

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
}