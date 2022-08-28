using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   private Rigidbody2D rb;
   private Animator anim;
   private BoxCollider2D coll;
   private SpriteRenderer sprite;
   [SerializeField] private LayerMask jumpableGround;
   private float dirX = 0f;

    [SerializeField] private float moveSpeed = 5f;
   [SerializeField] private float jumpForce = 10f;

    private enum MovementState { Idle, Run, Jump, Fall }
    

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        // here is the code for movements

        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        // here is the animation code

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state; 
        if (dirX > 0f)
        {
            state = MovementState.Run;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.Run;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.Idle;
        }


        if (rb.velocity.y > .1f)
        {
            state = MovementState.Jump;
        }

        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.Fall;
        }

        
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
