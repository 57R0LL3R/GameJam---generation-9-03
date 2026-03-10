using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float moveSpeed = 5f;
    public float climbSpeed = 5f;

    [Header("Estado")]
    [SerializeField] private bool isOnLadder = false;
    
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private float initialGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Guardamos la gravedad inicial del Rigidbody para restaurarla al bajar de la escalera
        initialGravity = rb.gravityScale;
    }

    /// <summary>
    /// Este método debe ser vinculado en el componente "Player Input" -> Events -> Move
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        // Leemos el valor del Vector2 (WASD o Flechas) desde el nuevo Input System
        moveInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        if (isOnLadder)
        {
            // Movimiento en escalera: Controlamos X e Y, con gravedad cero
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * climbSpeed);
        }
        else
        {
            // Movimiento normal: Solo controlamos X, la gravedad hace el resto en Y
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    #region Lógica de Escaleras

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica que tus objetos de escalera tengan el Tag "Ladder"
        if (collision.CompareTag("Ladder"))
        {
            isOnLadder = true;
            rb.gravityScale = 0; // Quitamos gravedad para que no se resbale al trepar
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            ExitLadder();
        }
    }

    private void ExitLadder()
    {
        isOnLadder = false;
        rb.gravityScale = initialGravity; // Restauramos la gravedad original del Player
    }

    #endregion
}