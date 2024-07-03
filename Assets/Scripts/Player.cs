using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    Collider2D playerCollider;
    float gravityScale = 0f;
    float initialJumpForce = 5f;

    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbSpeed = 2.5f;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        gravityScale = playerRigidbody.gravityScale;
        initialJumpForce = jumpForce;
    }
    

    // Update is called once per frame
    void Update()
    {
        Run(); 
        RunAnimation();
        Climb();
        // ClimbAnimation();
       
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
bool IsCollidingAtXPosition(float xPosition, LayerMask layerMask)
{
    bool isTouchingGroundBySide = playerCollider.IsTouchingLayers(layerMask);

    if (isTouchingGroundBySide)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(xPosition, transform.position.y), Vector2.up, 0.1f, layerMask);
        return hit.collider != null;
    }

    return false;
}

    bool isCollidingAt2DPosition(Vector2 direction, float positionX, float positionY, LayerMask layerMask){
        bool isTouchingLayer = playerCollider.IsTouchingLayers(layerMask);

        if(isTouchingLayer){
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(positionX, positionY), direction, 0.1f, layerMask);
            return hit.collider != null;
        }
        return false;
    }

    Dictionary<string, bool> getCollisionsPositions(LayerMask layerMask){
        Dictionary<string, bool> collisions = new Dictionary<string, bool>
        {
            { "top", isCollidingAt2DPosition(Vector2.up, transform.position.x, transform.position.y + 0.5f, layerMask) },
            { "bottom", isCollidingAt2DPosition(Vector2.down, transform.position.x, transform.position.y - 0.5f, layerMask) },
            { "left", isCollidingAt2DPosition(Vector2.left, transform.position.x - 0.5f, transform.position.y, layerMask) },
            { "right", isCollidingAt2DPosition(Vector2.right, transform.position.x + 0.5f, transform.position.y, layerMask) },
        };
        return collisions;
    }

   
    // void ClimbAnimation()
    // {
    //     bool playerHasVerticalSpeed = Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;
    //     bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

    //     LayerMask ladderLayer = LayerMask.GetMask("Ladders");
    //     LayerMask groundLayer = LayerMask.GetMask("Ground");

    //     bool isTouchingLadder = playerCollider.IsTouchingLayers(ladderLayer);
    //     Dictionary<string, bool> groundCollisions = getCollisionsPositions(groundLayer);
       
    //     if(!isTouchingLadder){ // when the player is not touching the ladder
    //         playerRigidbody.gravityScale = gravityScale;
    //         playerAnimator.SetBool("isClimbing", false);
    //         return;
    //     }
        
    //     playerAnimator.SetBool("isClimbing", true);
    // }

    void Run()
    {
        float playerMoveSpeed = playerSpeed * moveInput.x;
        Vector2 playerVelocity = new Vector2(playerMoveSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
    }

    void OnMove(InputValue value) // occurs when the player moves the joystick or keyboard keys are pressed, this method is comming from the inoput
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) // occurs when the player presses the jump button, this method is comming from the inoput
    {
        bool jumped = value.isPressed;
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        bool isTouchingGround = playerCollider.IsTouchingLayers(groundLayer);
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
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        bool isTouchingLadder = playerCollider.IsTouchingLayers(ladderLayer);
        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        Vector2 playerClimbVelocity = new Vector2(playerRigidbody.velocity.x, playerClimbSpeed);

        Dictionary<string, bool> groundCollisions = getCollisionsPositions(groundLayer);
        Dictionary<string, bool> ladderCollisions = getCollisionsPositions(ladderLayer);

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
}
