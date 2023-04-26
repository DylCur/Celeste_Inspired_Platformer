using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{




    [Header("Movement Parameters")]

    public float playerSpeed = 12f;
    public bool canMove = true;


    [Header("Jumping Parameters")]

    public bool isGrounded;
    public float jumpForce = 12f;
    public LayerMask groundMask;
    public float circleRadius = 0.1f;


    [Header("Keycodes")]

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;
    

    [Header("Dashing Parameters")]

    public bool canDash;
    public bool isDashing;
    public float dashForce;


    float horizontalInput;
    float verticalInput;


    Rigidbody2D rb;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        isGrounded = Physics2D.OverlapCircle(transform.position, circleRadius, groundMask);
        
        if(Input.GetKeyDown(dashKey) && !isDashing){
            dash();
        }
        movement(canMove); // Left Right and Jumping
    }

    public void movement(bool canMove){

        if(canMove){
            rb.velocity = new Vector2(horizontalInput * playerSpeed, rb.velocity.y);
        }

        else{
            rb.velocity = Vector2.zero;
        }

        if(Input.GetKeyDown(jumpKey) && isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
    }

    public void dash(){

        isDashing = true;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // freezePlayer(0, Vector2.zero);
        Debug.Log("Hello");

        // if(horizontalInput > 0){ // If right
        //     Debug.Log($"Horizontal: {horizontalInput}");
        //     if(verticalInput > 0){ // If up-right
        //         rb.velocity = new Vector2(dashForce, dashForce);
        //     }

        //     else if(verticalInput < 0){
        //         rb.velocity = new Vector2(dashForce, -dashForce);
        //     }

        //     else{
        //         rb.velocity = new Vector2(dashForce, 0f);
        //     }

            
        // }
        
        // else if(horizontalInput < 0){
        //     if(verticalInput > 0){ // If up-right
        //         rb.velocity = new Vector2(-dashForce, dashForce);
        //     }

        //     else if(verticalInput < 0){
        //         rb.velocity = new Vector2(-dashForce, -dashForce);
        //     }

        //     else{
        //         rb.velocity = new Vector2(-dashForce, 0f);
        //     }
        // }

        // else{
        //     if(verticalInput > 0){ // If up-right
        //         rb.velocity = new Vector2(0, dashForce);
        //     }

        //     else if(verticalInput < 0){
        //         rb.velocity = new Vector2(0, -dashForce);
        //     }

        //     else{
        //         rb.velocity = new Vector2(dashForce, 0f);
        //     }
        // }

    Vector2 dashDirection = new Vector2(horizontalInput, verticalInput).normalized;

    // Set the player's velocity to the dash direction multiplied by the dash force
    rb.velocity = dashDirection * dashForce;

    isDashing = false;


    }

    void freezePlayer(float gravity, Vector2 newVelocity){

        

        
    }
}
