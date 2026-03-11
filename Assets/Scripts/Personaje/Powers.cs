
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Powers : MonoBehaviour
{
    public float energy;
    public float movespeed = 15f;
    public float actualSpeed = 15f;
    public float jumpForce = 80f,flyForce=80f;
    public GameObject jumpPadPrefab;
    public GameObject runIndicator;
    public GameObject jetpackPrefab;

    Rigidbody2D rb;
    PlayerInput playerInput;
    bool doubleJumpUsed = false;
    [SerializeField]bool isGrounded = true, hasJetpack = false;
    bool isWalking = false;
    bool isJumping = false;
    public bool isFlying = false;
    int energyDrain = 5;
    int energyDrainActual = 5;
    public bool hasKey = false;
    SoundManager soundManager;
    Animator anim;
    SpriteRenderer spriteRenderer;
    
    public static PlayerState player = PlayerState.life;    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        runIndicator.SetActive(false);
        soundManager = GetComponent<SoundManager>();
        StartCoroutine(nameof(AnimationDie));
    }

    IEnumerator AnimationDie()
    {
        while (true)
        {
            yield return new WaitUntil(() => player == PlayerState.die);
            anim.SetTrigger("Die");
        }
    }

    void Update()
    {
        Vector2 moveVector = playerInput.actions["move"].ReadValue<Vector2>();

        isWalking = Mathf.Abs(moveVector.x) > 0.1f && isGrounded;
        isJumping = !isGrounded;

        if (playerInput.actions["jump"].WasPressedThisFrame())
        {
            if (isGrounded && energy >= 10)
            {
                jump();
            }
            else if (!isGrounded && hasJetpack)
            {
                jetpack();
            }
            else if (!isGrounded && !doubleJumpUsed && energy >= 20 && !hasJetpack)
            {
                doubleJump();
            }
        }
        if (playerInput.actions["jumpad"].WasPressedThisFrame() && energy > 50)
        {
            Instantiate(jumpPadPrefab, transform.position, Quaternion.identity);
            energy -= 50;

        }

        if (playerInput.actions["move"].IsPressed() && energy > 0)
        {
            moving();
            soundManager.PlayWalk();
        }

        if (playerInput.actions["jump"].IsPressed() && hasJetpack)
        {
            jetpack();
            isFlying = true;
        }
        else
        {
            isFlying = false;
            soundManager.StopFly();
        }

        if (isGrounded)
        {
            doubleJumpUsed = false;
        }
        if (!playerInput.actions["move"].IsPressed())
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFlying", isFlying);
    }
    public void jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        energy -= 10;
        isGrounded = false;
        Debug.Log("Jump Used");
        soundManager.PlayJump();

    }
    public void doubleJump()
    {

       
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        energy -= 20;
        doubleJumpUsed = true;
        Debug.Log("Double Jump Used");
        soundManager.PlayJump();

    }
    public void moving()
    {
        Vector2 moveVector = playerInput.actions["move"].ReadValue<Vector2>();

        if (moveVector.x > 0)
        {
            spriteRenderer.flipX = false; // mira a la derecha
        }
        else if (moveVector.x < 0)
        {
            spriteRenderer.flipX = true; // mira a la izquierda
        }
        if (playerInput.actions["sprint"].IsPressed() && energy > 0)
        {
            runIndicator.SetActive(true);
            actualSpeed = movespeed*2;
            energyDrainActual = energyDrain * 2;
            isWalking = true;
        }
        else
        {
            runIndicator.SetActive(false);
            actualSpeed = movespeed;
            energyDrainActual = energyDrain;
            isWalking = true;
        }
        rb.linearVelocity = new Vector2(moveVector.x * actualSpeed, rb.linearVelocity.y);
        energy -= energyDrainActual * Time.deltaTime;
    }
    public void jetpack()
    {
            Debug.Log("jectpack0");
        if (!isGrounded && hasJetpack && energy > 0)
        {
            Debug.Log("jectpack1");
            Vector2 jetVector = new Vector2(0, 0.6f);
            rb.AddForce(jetVector * flyForce);
            energy -= 15 * Time.deltaTime;
            soundManager.PlayFly();
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
        }
        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            hasKey = true;
        }
    }
}

