using UnityEngine;
using Unity;
public class Movement : MonoBehaviour
{
    [SerializeField]private float speed = 5f;
    private float horizontalInput;
    [SerializeField]private float JumpingPower = 16f;
    private bool isFacingRight = true;
    [SerializeField]private float DownForce = 10f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform grounCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpingPower);
        }

        if (Input.GetButtonUp("Jump")&& rb.linearVelocity.y > 0.5f)
        {

        }

        if (Input.GetButtonDown("Fall") &&  !isGrounded())
            {
            rb.gravityScale = DownForce;
                
            }
        if (isGrounded())
        {
            rb.gravityScale = 3.0f;
        }
        flip();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            Debug.Log("Running");
            anim.SetBool("isRunning", true);
        }
        else
        {
            Debug.Log("Idle");
            anim.SetBool("isRunning", false);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(grounCheck.position, 0.2f, groundLayer);
    }

    private void flip()
    {
        if (isFacingRight && horizontalInput < 0f ||  !isFacingRight && horizontalInput > 0f )
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }

}
