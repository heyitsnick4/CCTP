using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement2D : MonoBehaviour {
    
    private float horizontal;
    private bool isFacingRight = true;

    PlayerControls controls;

    //Jumping
    public float speed = 8f;
    public float jumpingPower = 16f;

    //Dashing
    private bool CanDash = true;
    private bool IsDashing;
    private float DashingPower = 24f;
    private float DashingTime = 0.2f;
    private float DashingCooldown = 1f;

    //Wall sliding
    private bool IsWallSliding;
    private float WallSlidingSpeed = 2f;

    //Wall jumping
    private bool IsWallJumping;
    private float WallJumpingDirection;
    private float WallJumpingTime = 0.2f;
    private float WallJumpingCounter;
    private float WallJumpingDuration = 0.4f;
    private Vector2 WallJumpingPower = new Vector2(8f, 16f);

    //Parry jump
    private bool CanParry = false;

    //Dash Jumping
    private bool CanDashJump = false;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform WallCheck;
    [SerializeField] private LayerMask WallLayer;
    [SerializeField] private GameObject CamFollowPoint;

    bool flip = false;


    void Awake()
    {
        controls = new PlayerControls();

        controls.Dash.Dash.performed += ctx => StartCoroutine(Dash());
    }

    void Update()
    {
        if (IsDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded() || Input.GetKeyDown(KeyCode.Space) && CanParry || Input.GetKeyDown(KeyCode.Space) && CanDashJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            CanParry = false;
        }
        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash)
        {
            StartCoroutine(Dash());
        }

        WallSlide();
        WallJump();

        if(!IsWallJumping)
        {
            Flip();
        }

        if(rb.velocity.y < -20)
        {
            CamFollowPoint.transform.localPosition = new Vector2(0, -4);
        }
        else
        {
            CamFollowPoint.transform.localPosition = new Vector2(0, 4);
        }

        if(isGrounded() || IsWallSliding )
        {
            CanDashJump = false;
        }
    }

    private void FixedUpdate() 
    {
        if (!IsWallJumping)
        {
            if (IsDashing)
            {
                return;
            }

            if (flip)
            {
                rb.velocity = new Vector2(horizontal * -speed, rb.velocity.y);
            }
            if (!flip)
            {
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            }
        }
       
    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);
    }

    private IEnumerator Dash()
    {
        CanDash = false;
        IsDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * DashingPower, 0f);
        yield return new WaitForSeconds(DashingTime);
        rb.gravityScale = originalGravity;
        IsDashing = false;
        CanDashJump = true;
        yield return new WaitForSeconds(DashingCooldown);
        CanDash = true;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.2f, WallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !isGrounded() && horizontal != 0f)
        {
            IsWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -WallSlidingSpeed, float.MaxValue));
        }
        else
        {
            IsWallSliding = false;
        }
    }

    private void WallJump()
    {
        if(IsWallSliding)
        {
            IsWallJumping = false;
            WallJumpingDirection = -transform.localScale.x;
            WallJumpingCounter = WallJumpingTime;

            //CancleInvoke(nameof(StopWalljumping));
        }
        else
        {
            WallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && WallJumpingCounter > 0f)
        {
            IsWallJumping = true;
            rb.velocity = new Vector2(WallJumpingDirection * WallJumpingPower.x, WallJumpingPower.y);
            WallJumpingCounter = 0f;

            if(transform.localScale.x != WallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWalljumping), WallJumpingDuration);
        }

    }

    private void StopWalljumping()
    {
        IsWallJumping = false;
    }

    public void StartParryJump()
    {
        CanParry = true;
    }

    public void EndParryJump()
    {
        CanParry = false;
    }

    void OnEnable()
    {
        controls.Dash.Enable();
    }

    void OnDisable()
    {
        controls.Dash.Disable();
    }
}