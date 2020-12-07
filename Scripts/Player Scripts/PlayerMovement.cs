using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D Controller;
    private Animator Animator;

    float horizontalMove;
    public float runSpeed;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        Animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -9f, 9f),
           Mathf.Clamp(transform.position.y, -6f, 4.8f));
    }

    void FixedUpdate()
    {
        Controller.Move(horizontalMove * Time.deltaTime, false);
    }
}