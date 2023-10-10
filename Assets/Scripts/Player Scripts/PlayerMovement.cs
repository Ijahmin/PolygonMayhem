using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private float gravityScale = 3;
    [SerializeField] private int numJumps = 2;
    private int jumpsLeft;
    private float dirX;
    private bool facingRight = true;
    private bool isJumping = false;

    [SerializeField] private LayerMask terrain;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(.985f, .01f);

    [SerializeField] private Transform frontWallCheckPoint;
    [SerializeField] private Transform backWallCheckPoint;
    [SerializeField] private Vector2 wallCheckSize = new Vector2(.03f, .5f);
    [SerializeField] private float wallSlideSpeed = 0.2f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        jumpsLeft = numJumps;
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            jumpsLeft--;
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (isGrounded() && jumpsLeft != numJumps && rb.velocity.y < .01f)
        {
            isJumping = false;
            jumpsLeft = numJumps;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }

        if (rb.velocity.y < -0.01f && isWallSliding())
        {
            rb.gravityScale = gravityScale * wallSlideSpeed;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX < 0)
        {
            sprite.flipX = true;
            facingRight = false;
        }
        else if (dirX > 0)
        {
            sprite.flipX = false;
            facingRight = true;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, terrain);
    }

    private bool isWallSliding()
    {
        bool frontWallCheck = (Physics2D.OverlapBox(frontWallCheckPoint.position, wallCheckSize, 0, terrain) && dirX > 0.01f);

        bool backWallCheck = (Physics2D.OverlapBox(backWallCheckPoint.position, wallCheckSize, 0, terrain) && dirX < -0.01f);

        return frontWallCheck || backWallCheck;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
        Gizmos.DrawWireCube(backWallCheckPoint.position, wallCheckSize);
        Gizmos.DrawWireCube(frontWallCheckPoint.position, wallCheckSize);
    }
}
