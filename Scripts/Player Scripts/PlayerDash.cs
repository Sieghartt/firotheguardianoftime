using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator Animator;
    private Score score;
    [SerializeField] private Collider2D[] collider;
    public Image dashIcon;
    
    public float dashSpeed;
    public int dashDamage;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public float dashCoolDown = 5f;
    float currentCoolDown;
    private float horizontalMove;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (direction == 0)
        {
            if (currentCoolDown >= dashCoolDown)
            {
                if (Input.GetButton("Dash") &&  horizontalMove < 0)
                {
                    FindObjectOfType<AudioManager>().Play("Dash");
                    direction = 1;
                    currentCoolDown = 0f;
                    collider[0].isTrigger = true;
                    collider[1].isTrigger = true;
                    rigidBody.gravityScale = 0;
                }

                if (Input.GetButton("Dash") && horizontalMove > 0)
                {
                    FindObjectOfType<AudioManager>().Play("Dash");
                    direction = 2;
                    currentCoolDown = 0f;
                    collider[0].isTrigger = true;
                    collider[1].isTrigger = true;
                    rigidBody.gravityScale = 0;
                }
            }
            
        }else
        {
            if (dashTime <= 0)
            {
                collider[0].isTrigger = false;
                collider[1].isTrigger = false;
                rigidBody.gravityScale = 4.5f;
                direction = 0;
                dashTime = startDashTime;
                rigidBody.velocity = Vector2.zero;
                Animator.SetBool("IsDashing", false);

            }else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    rigidBody.velocity = Vector2.left * dashSpeed;
                    Animator.SetBool("IsDashing", true);

                }else if (direction == 2)
                {
                    rigidBody.velocity = Vector2.right * dashSpeed;
                    Animator.SetBool("IsDashing", true);
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if(enemyHealth != null)
        {
            enemyHealth.TakeDamage(dashDamage);
            score._dashKill += 1;
        }
    }

    private void Update()
    {
        if (currentCoolDown < dashCoolDown)
        {
            currentCoolDown += Time.deltaTime;
            dashIcon.fillAmount = currentCoolDown / dashCoolDown;
        }
    }
}
