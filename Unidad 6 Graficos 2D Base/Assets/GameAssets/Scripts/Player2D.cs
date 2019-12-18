using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    [SerializeField]
    private float movSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Transform groundDetector;
    [SerializeField]
    private float groundDetectDist;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        //utilizamos el raw para que los valores sean 1, -1 o 0, sin valores intermedios
        transform.position += Vector3.right * movSpeed * Time.deltaTime * horizontal;


        if(horizontal != 0)
        {
            transform.localScale = new Vector3(horizontal, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("IsJump");
        }

        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        anim.SetFloat("SpeedY", rb.velocity.y);
    }

    private void FixedUpdate()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundDetector.position, groundDetectDist);
        foreach(Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundDetector.position, groundDetectDist);
    }
}
