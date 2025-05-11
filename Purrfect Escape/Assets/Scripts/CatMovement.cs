using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class CatMovement : MonoBehaviour
{
    [SerializeField] private GameObject GameWin;
    [SerializeField] private float MovementSpeed = 5f;
    private Vector2 Movement;
    private bool FacingRight = true;
    private Animator animatormain;
    private SpriteRenderer rend;
    private bool CanHide = false;
    private void Start()
    {
        animatormain=GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        float input = Input.GetAxis("Horizontal");
        Movement.x = input * MovementSpeed * Time.deltaTime;
        transform.Translate(Movement);
        animatormain.SetBool("isRunning",input!=0);
        if (Movement.x > 0 && !FacingRight)
        {
            Flip();
        }
        else if (Movement.x < 0 && FacingRight)
        {
            Flip();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HidingObject"))
        {
            CanHide = true;
        }
        if(collision.gameObject.CompareTag("Win"))
        {
            GameWin.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HidingObject"))
        {
            CanHide = false;
        }
    }

    void Flip()
        {
            FacingRight = !FacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
}
