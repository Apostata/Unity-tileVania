using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    Collider2D playerBodyCollider;
    Collider2D playerFeetCollider;
    float gravityScale = 0f;
    float initialJumpForce = 5f;

    bool wasHitted = false;

    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbSpeed = 2.5f;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        gravityScale = playerRigidbody.gravityScale;
        initialJumpForce = jumpForce;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(wasHitted){ return;}

        Run(); 
        Climb();       
        Hitted();
        
    }

    void RunAnimation()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon; // Mathf.Epsilon is the smallest number that can be represented in the computer (0.0000001)
        if (playerHasHorizontalSpeed){
            float playerDirection = Mathf.Sign(playerRigidbody.velocity.x);
            transform.localScale = new Vector2(playerDirection, 1f);
        } 
        
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    
    void Run()
    {
        float playerMoveSpeed = playerSpeed * moveInput.x;
        Vector2 playerVelocity = new(playerMoveSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
        RunAnimation();
    }

    void OnMove(InputValue value) // occurs when the player moves the joystick or keyboard keys are pressed, this method is comming from the inoput
    {
        if(wasHitted){ return;}
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) // occurs when the player presses the jump button, this method is comming from the inoput
    {
        if(wasHitted){ return;}
        bool jumped = value.isPressed;
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        bool isTouchingGround = playerFeetCollider.IsTouchingLayers(groundLayer);
        float playerYVelocity = playerRigidbody.velocity.y;
        
        if (jumped && isTouchingGround && playerYVelocity == 0)
        {
            playerRigidbody.velocity += new Vector2(0, jumpForce);
        }
    }

    void Climb()
    {
        float playerClimbSpeed = climbSpeed * moveInput.y;
        LayerMask ladderLayer = LayerMask.GetMask("Ladders");

        bool isTouchingLadder = playerFeetCollider.IsTouchingLayers(ladderLayer);
        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;

        Vector2 playerClimbVelocity = new Vector2(playerRigidbody.velocity.x, playerClimbSpeed);


        if(!isTouchingLadder){
            playerRigidbody.gravityScale = gravityScale;
            playerAnimator.SetBool("isClimbing", false);
            playerAnimator.speed = 1.5f;
            return;
        } 
        
        playerAnimator.speed = 0.6f;
        playerRigidbody.gravityScale = 0;
        playerRigidbody.velocity = playerClimbVelocity;
        playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
       
    }

    void Hitted(){
        
        LayerMask injuryLayer = LayerMask.GetMask("Enemy", "Hazards");

        bool isTouchingEmeny = playerBodyCollider.IsTouchingLayers(injuryLayer);

        if(isTouchingEmeny){
            playerRigidbody.gravityScale = gravityScale;
            playerRigidbody.sharedMaterial = null;
            wasHitted = true;
            playerAnimator.SetTrigger("Dying");
        }
        
    }
}
