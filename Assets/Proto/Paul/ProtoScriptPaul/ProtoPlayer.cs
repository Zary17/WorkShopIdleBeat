using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtoPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    ProtoGameManager protoGameManager;
    ScrollingBackGround scrollingBackGround;
    AudioManager _audioManager;

    #region Jump

    //------JUMP-----//
    /*[Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTimeCounter;
    [SerializeField] bool isJumping;
    [SerializeField] float jumpTime;*/



    // A Gaetan


    [SerializeField] float jumpCut;
    [SerializeField] float jumpForce;

    bool IsJumping = false;

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



    #endregion

    #region GROUND

    //------GROUND-----//
   /* [Header("Ground")]
    [SerializeField] Transform groundCheckCollider;
    float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;*/
    #endregion

    #region Etat

    //------isEvil-----//
    [Header("Etat")]
    [SerializeField] int life;

    //Tester de retirer le public.
    public bool isEvil = false;
    #endregion

    #region Switch
    ScrollingBackGround[] scroll;
    [SerializeField] proto_UI proto_ui;
    #endregion


    [SerializeField] Camera mainCam;
    [SerializeField] ProtoPlayerInteraction interact;

     [SerializeField] Animator VFX;


    ProtoPlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();

        protoGameManager = FindObjectOfType<ProtoGameManager>();

        scroll = FindObjectsOfType<ScrollingBackGround>();
        _audioManager = FindObjectOfType<AudioManager>();

        stats = GetComponentInParent<ProtoPlayerStats>();
    }

    private void FixedUpdate()
    {
        //GroundCheck();
    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        /*if (isJumping == true)
        {
            if (jumpTime > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                //rb.AddForce(Vector2.up * jumpForce);
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;

                Debug.Log("Fin de saut");
            }
        }*/





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
            GetComponent<Animator>().SetBool("GentilleJump", false);
            IsJumping = false;
        }

        if (CanJump() && LastPressedJumpTime > 0)
        {

            IsJumping = true;
            
            JumpAcction();
        }





        //Interaction
    }
    #region JUMP
    //Jump Input
    public void Jump(InputAction.CallbackContext ctx)
    {
        /* if (!ctx.performed)
         {
             isJumping = false; return;
         }

         if (ctx.performed && isGrounded && !isEvil && isJumping == false)
         {
             isJumping = true;

             jumpTime = jumpTimeCounter;

             Debug.Log("Jump Perform !");
         }*/






        if (ctx.started && !IsJumping && isGrounded && !isEvil)
        {
            LastPressedJumpTime = bufferTime;
            GetComponent<Animator>().SetBool("GentilleJump", true);

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


    //Pour vérifier si il touche le sol.
    /* void GroundCheck()
     {
         isGrounded = false;

         Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);

         if (colliders.Length > 0)
         {
             isGrounded = true;

             Debug.Log("isGrounded");
         }
     }*/
    #endregion




    public void PostGameEffect()
    {
        mainCam.orthographicSize = 3;
        mainCam.transform.position = new Vector3(-5, -1.5f,-10);

        proto_ui.Win();
    }




    public void Switch()
    {
        isEvil = !isEvil;

        if (isEvil)
        {
            VFX.SetTrigger("GoodVersEvil");
            //GetComponentInChildren<Animator>().SetTrigger("EvilVersGood");
            _audioManager.Play("SwitchGood");
        }
        else
        {
            VFX.SetTrigger("EvilVersGood");
            //GetComponentInChildren<Animator>().SetTrigger("GoodVersEvil");
            _audioManager.Play("SwitchEvil");
        }

        proto_ui.ChangeUI(isEvil);

        foreach (ScrollingBackGround listscroll in scroll)
        {
            listscroll.ChangeBackGround(isEvil);
        }



        //Changement d'animation
        GetComponentInParent<Animator>().SetBool("isEvil", isEvil);
    }

    //A tester
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Checkpoint"))
        {
            //protoGameManager.GetInfo(collision.transform, 1f /*Changer la valeur à la main*/);

            collision.transform.GetComponent<proto_CheckPoint>().SendData();

            Debug.Log("New Checkpoint");
        }

        if (collision.transform.CompareTag("Enemy"))
        {
            life -= 1;

            collision.transform.GetComponent<Fan>().DestroyObject();

            interact.ResetCombo();

            if (life == 0)
            {
                protoGameManager.OnDeath();

                proto_ui.TakeDamage();

                int score;
                score = stats.SendScore();

                life = 3;
            }

            Debug.Log("Enemy contact");
        }

        if (collision.transform.CompareTag("Trou"))
        {
            protoGameManager.Respawn();

            Debug.Log("Perso tombé");
        }

       


        if (collision.transform.CompareTag("Arrive"))
        {
            PostGameEffect();
            proto_ui.Win();

            int score;
            score = stats.SendScore();
            proto_ui.UpdateUI(score);


            //Debug.Log("Switch");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Switch"))
        {
            Switch();

            //Debug.Log("Switch");
        }


        if (collision.transform.CompareTag("Pièce"))
        {
            //
            collision.transform.GetComponent<Pièce>().activeObject();
            stats.AddComboAndScore(1);

            Debug.Log("Pièce ramassé");
        }

    }




    //
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            //protoGameManager.GetInfo(collision.transform, 1f Changer la valeur à la main);

            collision.GetComponent<proto_CheckPoint>().SendData();

            Debug.Log("New Checkpoint");
        }

        if (collision.CompareTag("Enemy"))
        {
            life -= 1;

            if (life == 0)
            {
                protoGameManager.OnDeath();

                life = 3;
            }

            Debug.Log("Enemy contact");
        }

        if (collision.CompareTag("Trou"))
        {
            protoGameManager.Respawn();

            Debug.Log("Perso tombé");
        }

        if (collision.CompareTag("Pièce"))
        {
            //
            collision.GetComponent<Pièce>().activeObject();

            Debug.Log("Pièce ramassé");
        }

        if (collision.CompareTag("Switch"))
        {
            Switch();

            //Debug.Log("Switch");
        }
    }*/



}