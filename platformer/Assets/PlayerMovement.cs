using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private bool isDashing = false;
    public float dashForce = 7f;
    private float dashTimeLeft = 0f;
    public bool canDash = true;


    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public Animator animator;
    bool isFacingRight = true;

    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 5f;
    public int maxJumps = 2;
    int jumpsLeft;

    public Transform spawnpoint;


    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isGrounded())
        {
            canDash = true;
        }

        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        }
            
        Flip();

        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("killBlock"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = spawnpoint.position;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded())
        {
            jumpsLeft = 2;
        }

        if (context.performed && jumpsLeft > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            jumpsLeft -= 1;
            animator.SetTrigger("jump");
        }



    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing && canDash)
        {
            canDash = false;
            isDashing = true;
            dashTimeLeft = 0.15f;

            float direction = isFacingRight ? 1f : -1f;
            rb.linearVelocity = new Vector2(direction * dashForce, rb.linearVelocity.y);


        }

        
    }

    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);
    }
}
