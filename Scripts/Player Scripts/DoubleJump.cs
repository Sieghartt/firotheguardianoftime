using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    Animator Animator;
    Rigidbody2D rb;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    bool isGrounded;
    int extraJumps;

    public float jumpForce;
    public int extraJumpsValue;

    
    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            Animator.SetBool("IsJumping", true);
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            Animator.SetBool("IsJumping", true);
        }
    }

    public void OnLanding()
    {
        Animator.SetBool("IsJumping", false);
    }
}
