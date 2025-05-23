using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FloorPatrolPoints
{
    public Transform pointA;
    public Transform pointB;
    public Transform floorCenter;
}
public class GrannyAI : MonoBehaviour
{
    public FloorPatrolPoints[] patrolFloors;
    public GameObject gameOverPanel;
    public float speed, newPointAX;
    private Transform currentPoint;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float InteractionRange = 6f;
    public LayerMask teleporterLayer;
    public GrannyAnger grannyAnger;
    private int currentFloorIndex = 0;
    private bool justTeleported = false;
    [SerializeField] private float teleportCooldown = 5.0f;
    private GameObject lastTeleporter = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SetFloor(currentFloorIndex);
        // anim.SetBool("isRunning", true); when we got animation ill use this, don't delete.
    }

    void Update()
    {
        float moveDirection = currentPoint == patrolFloors[currentFloorIndex].pointB ? 1 : -1;
        //rb.linearVelocity = new Vector2(moveDirection * speed, 0);
        rb.linearVelocity = new Vector2(moveDirection * grannyAnger.grannySpeed, 0);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, InteractionRange, teleporterLayer);

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            currentPoint = currentPoint == patrolFloors[currentFloorIndex].pointB
                ? patrolFloors[currentFloorIndex].pointA
                : patrolFloors[currentFloorIndex].pointB;
            Flip(moveDirection);
        }
        if (!justTeleported)
        {
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Teleporter") && hit.gameObject != lastTeleporter)
                {
                    // 50% chance to teleport
                    if (Random.value < 0.6f)
                    {
                        PlayerTeleporter teleporterScript = FindFirstObjectByType<PlayerTeleporter>();
                        if (teleporterScript != null)
                        {
                            teleporterScript.Interact(gameObject, hit.gameObject);
                            justTeleported = true;
                            lastTeleporter = hit.gameObject; // Store the last used teleporter
                            Invoke(nameof(ResetTeleportFlag), teleportCooldown);
                            Invoke(nameof(UpdatePatrolFloor), 0.1f);
                            Debug.Log("Granny teleported!");
                            break;
                        }
                    }
                    else
                    {
                        Debug.Log("Granny saw the teleporter but chose not to use it.");
                        lastTeleporter = hit.gameObject; // Mark it even if not used to avoid re-evaluation
                        justTeleported = true;
                        Invoke(nameof(ResetTeleportFlag), teleportCooldown);
                        break;
                    }
                }
            }
        }
    }
    private void UpdatePatrolFloor()
    {
        float minDistance = float.MaxValue;
        int newFloorIndex = currentFloorIndex;

        for (int i = 0; i < patrolFloors.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, patrolFloors[i].floorCenter.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                newFloorIndex = i;
            }
        }

        if (newFloorIndex != currentFloorIndex)
        {
            currentFloorIndex = newFloorIndex;
            SetFloor(currentFloorIndex);
        }
    }
    private void SetFloor(int floorIndex)
    {
        currentPoint = patrolFloors[floorIndex].pointA;
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
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, InteractionRange, teleporterLayer);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Teleporter"))
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(transform.position, hit.transform.position);
                    Gizmos.DrawSphere(hit.transform.position, 0.15f);
                }
            }
        }
        // Draw all floor patrol routes and centers
        if (patrolFloors != null)
        {
            foreach (var floor in patrolFloors)
            {
                if (floor.pointA != null && floor.pointB != null)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(floor.pointA.position, floor.pointB.position);
                    Gizmos.DrawSphere(floor.pointA.position, 0.2f);
                    Gizmos.DrawSphere(floor.pointB.position, 0.2f);
                }

                if (floor.floorCenter != null)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(floor.floorCenter.position, 0.2f);
                }
            }
        }

        if (currentPoint != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(currentPoint.position, 0.3f);
            Gizmos.DrawLine(transform.position, currentPoint.position);
        }
    }
    private void ResetTeleportFlag()
    {
        justTeleported = false;
        lastTeleporter = null;
    }
}