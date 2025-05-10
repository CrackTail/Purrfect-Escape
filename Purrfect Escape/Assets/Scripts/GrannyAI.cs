using UnityEngine;

public class GrannyAI : MonoBehaviour
{
    public GameObject pointA, pointB, gameOverPanel;
    public float speed, newPointBX;
    private bool doorMoved = false;
    private Transform currentPoint;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        // anim.SetBool("isRunning", true); when we got animation ill use this, don't delete.
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(currentPoint == pointB.transform ? speed : -speed, 0);
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            currentPoint = currentPoint == pointB.transform ? pointA.transform : pointB.transform;

        if (!doorMoved && GameObject.FindWithTag("LockedDoor") == null)
        {
            doorMoved = true;
            pointB.transform.position = new Vector3(newPointBX, pointB.transform.position.y, pointB.transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            gameOverPanel.SetActive(true);
    }
}