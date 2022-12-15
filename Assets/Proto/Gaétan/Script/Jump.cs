using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Jump : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField] float jumpCut;
    [SerializeField] float jumpForce;

    bool IsJumping=false;

    float LastPressedJumpTime;
    [SerializeField] float bufferTime;


    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    float LastOnGroundTime;
    bool isGrounded;
    [SerializeField] float coyoteTime;

    [SerializeField] float gravityScale;
    [SerializeField] float fallGravityMult;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        CanJump();

        if (rb.velocity.y >= 0)
            SetGravityScale(gravityScale);
        else
            SetGravityScale(gravityScale * fallGravityMult);



        if (Physics2D.OverlapBox(groundCheck.position, groundCheckRadius, 0, groundLayer))//checks if set box overlaps with ground
        {
            isGrounded = true;
            LastOnGroundTime = coyoteTime;
        }
        else
            isGrounded = false;
        


        if (IsJumping && isGrounded)
        {
            IsJumping = false;
        }

        if (CanJump() && LastPressedJumpTime > 0)
        {

            IsJumping = true;
            JumpAcction();
        }

    }



    public void JUmp(InputAction.CallbackContext ctx)
    {
        if (ctx.started && !IsJumping && isGrounded)
        {
            LastPressedJumpTime = bufferTime;

        }

        if (ctx.canceled)
        {
            JupCut();
        }

    }





    public void JumpAcction()
    {
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;
        float force = jumpForce;
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }


    public void JupCut()
    {
        rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCut), ForceMode2D.Impulse);
    }


    private bool CanJump()
    {
        return LastOnGroundTime > 0 && !IsJumping;

       
    }

    private bool CanJumpCut()
    {
        return IsJumping && rb.velocity.y > 0;
    }


    public void SetGravityScale(float scale)
    {
        rb.gravityScale = scale;
    }
}
