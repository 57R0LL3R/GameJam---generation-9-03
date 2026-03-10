using UnityEngine;
using UnityEngine.InputSystem;

public class Powers : MonoBehaviour
{
    public float energy;
    public float movespeed = 15f;
    public float jumpForce = 80f;
    public GameObject jumpPadPrefab;
    public GameObject runIndicator;
    public GameObject jetpackPrefab;
    int jumpadQuantity = 0;
    Rigidbody2D rb;
    PlayerInput playerInput;
    bool doubleJumpUsed = false;
    bool isGrounded = true;
    bool hasJetpack = false;
    int energyDrain = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        runIndicator.SetActive(false);
    }



    void Update()
    {
        if (playerInput.actions["jump"].WasPressedThisFrame())
        {
            if (isGrounded && energy >= 10)
            {
                jump();
            }
            else if (!isGrounded && !doubleJumpUsed && energy >= 20 && !hasJetpack)
            {
                doubleJump();
            }
        }
        if (playerInput.actions["jump"].IsPressed() && hasJetpack)
        {
            jetpack();
        }
        if (isGrounded)
        {
            doubleJumpUsed = false;
        }
        if (playerInput.actions["move"].IsPressed() && energy > 0)
        {
            moving();
        }

        if (playerInput.actions["jumpad"].WasPressedThisFrame() && energy > 50 && jumpadQuantity > 0)
        {
            Instantiate(jumpPadPrefab, transform.position, Quaternion.identity);
            energy -= 50;
            jumpadQuantity--;
        }
        if (!playerInput.actions["move"].IsPressed())
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        if (playerInput.actions["RemoveAll"].WasPressedThisFrame())
        {
            hasJetpack = false;
            jetpackPrefab.SetActive(false);
            Debug.Log("All Powers Removed");
        }




    }
    void jump()
    {
        Vector2 jumpVector = new Vector2(0, 1);
        rb.AddForce(jumpVector * jumpForce, ForceMode2D.Impulse);
        energy -= 10;
        isGrounded = false;
        Debug.Log("Jump Used");


    }
    void doubleJump()
    {

        Vector2 jumpVector = new Vector2(0, 1);
        rb.AddForce(jumpVector * jumpForce, ForceMode2D.Impulse);
        energy -= 20;
        doubleJumpUsed = true;
        Debug.Log("Double Jump Used");

    }
    void moving()
    {

        Vector2 moveVector = playerInput.actions["move"].ReadValue<Vector2>();
        if (playerInput.actions["sprint"].IsPressed() && energy > 0)
        {
            runIndicator.SetActive(true);
            movespeed = movespeed = 30f;
            energyDrain = 20;
        }
        else
        {
            runIndicator.SetActive(false);
            energyDrain = 10;
        }
        rb.linearVelocity = new Vector2(moveVector.x * movespeed, rb.linearVelocity.y);
        energy -= energyDrain * Time.deltaTime;
    }
    void jetpack()
    {
        if (!isGrounded && hasJetpack && energy > 0)
        {
            Vector2 jetVector = new Vector2(0, 0.6f);
            rb.AddForce(jetVector * jumpForce);
            energy -= 15 * Time.deltaTime;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {

        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
                Debug.Log("Airborne");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jetpackpickup"))
        {
            hasJetpack = true;
            Destroy(other.gameObject);
            jetpackPrefab.SetActive(true);

        }
        if (other.CompareTag("JumpPadPickup"))
        {
            Destroy(other.gameObject);
            jumpadQuantity++;
        }
    }
}

