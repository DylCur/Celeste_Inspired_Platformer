using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [Header("Keycodes")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode consoleJumpKey = KeyCode.Joystick1Button2;

    [Header("Movement Parameters")]
    public float playerSpeed = 12f;

    [Header("Grounded Parameters")]
    public float radius = 0.2f;
    public LayerMask groundLayers;
    public bool isGrounded;

    [Header("Jumping Parameters")]
    public float jumpForce = 10f;

    [Header("Dashing Parameters")]
    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f;

    private Vector2 dashDirection;
    private bool isDashing;
    private float dashTime;
    private bool hasDashedGrounded;

    Rigidbody2D rb;
    public float HorizontalInput;


    // Start is called before the first frame update
    void Start()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, radius, groundLayers);

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, radius, groundLayers);

        HorizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(HorizontalInput * playerSpeed, rb.velocity.y);

        if (Input.GetKeyDown(jumpKey) && isGrounded || Input.GetKeyDown(consoleJumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if ((Input.GetKeyDown(jumpKey) || Input.GetKeyDown(consoleJumpKey)) && !isDashing && !hasDashedGrounded && !isGrounded)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput != 0 || verticalInput != 0)
            {
                dashDirection = new Vector2(horizontalInput, verticalInput).normalized;
                isDashing = true;
                dashTime = dashDuration;
            }
        }

        if (isDashing)
        {
            rb.velocity = dashDirection * dashSpeed;
            dashTime -= Time.deltaTime;

            if (dashTime <= 0f)
            {
                isDashing = false;
                hasDashedGrounded = !isGrounded;
            }
        }

        if (isGrounded)
        {
            hasDashedGrounded = false;
        }
    }
}
