using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GrannyAI:MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Animator anim;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;

    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        currentPoint=pointB.transform;
        //anim.SetBool("isRunning", true);

    }
    private void Update()
    {
        Vector2 point=currentPoint.position-transform.position;
        if (currentPoint== pointB.transform){
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position)<0.5f && currentPoint==pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
}
}