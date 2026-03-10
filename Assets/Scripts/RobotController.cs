using UnityEngine;

public class RobotController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float velocidadCaminar = 5f;
    public float velocidadCorrer = 8f;
    public float fuerzaVuelo = 7f;
    public float fuerzaSalto = 10f; // Súbele a 12 o 15 si sientes que el salto es muy bajo

    // Referencias a los componentes
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // Variables de estado
    private bool enElSuelo = true;
    private bool estaCaminando = false;
    private bool estaVolando = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ManejarMovimiento();
        ManejarVuelo();
        ManejarSalto();
        ActualizarAnimaciones();
        
        // Función extra para probar tus animaciones de impacto durante la Jam
        ProbarDañoYMuerte(); 
    }

    void ManejarMovimiento()
    {
        float movimientoX = Input.GetAxisRaw("Horizontal");
        bool estaCorriendo = Input.GetKey(KeyCode.LeftShift); 
        float velocidadActual = estaCorriendo ? velocidadCorrer : velocidadCaminar;

        // Aplicamos la física horizontal
        rb.linearVelocity = new Vector2(movimientoX * velocidadActual, rb.linearVelocity.y);

        // ==========================================
        // ¡LA MAGIA DEL FLIP UNIVERSAL!
        // Como esto está fuera de las condiciones de caminar, 
        // voltea al robot así esté saltando, volando o recibiendo daño.
        // ==========================================
        if (movimientoX > 0)
        {
            spriteRenderer.flipX = false; // Mira a la derecha (tus sprites originales)
        }
        else if (movimientoX < 0)
        {
            spriteRenderer.flipX = true;  // Lo voltea como espejo hacia la izquierda
        }

        // Lógica de Caminar (solo si está tocando el suelo y no está volando)
        if (movimientoX != 0 && enElSuelo && !estaVolando)
        {
            estaCaminando = true;
            // Si corre, la animación va a 1.5x de velocidad
            anim.SetFloat("MultiplicadorCaminata", estaCorriendo ? 1.5f : 1f);
        }
        else
        {
            estaCaminando = false;
        }
    }

    void ManejarVuelo()
    {
        // Al mantener oprimida la barra espaciadora
        if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaVuelo);
            estaVolando = true;
        }
        else
        {
            estaVolando = false;
        }
    }

    void ManejarSalto()
    {
        // Al presionar la 'W' una sola vez y estando en el piso
        if (Input.GetKeyDown(KeyCode.W) && enElSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            anim.SetTrigger("Jump"); 
            
            // LA LÍNEA MÁGICA: Corta el circuito de inmediato
            enElSuelo = false; 
        }
    }

    void ProbarDañoYMuerte()
    {
        // Presiona la tecla 'E' para simular que un enemigo te golpea
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("TakeDamage");
        }
        
        // Presiona la tecla 'R' para simular que te quedaste sin batería
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Die");
        }
    }

    void ActualizarAnimaciones()
    {
       if (rb.linearVelocity.y > 0.1f || rb.linearVelocity.y < -0.1f)
        {
            enElSuelo = false;
        }
        // -------------------------

        // Le enviamos la información actualizada al cerebro (Animator)
        anim.SetBool("isWalking", estaCaminando);
        anim.SetBool("isFlying", estaVolando);
        anim.SetBool("isGrounded", enElSuelo);
    }

    // --- Detectores del colisionador de los pies ---
    private void OnCollisionStay2D(Collision2D collision)
    {
        enElSuelo = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enElSuelo = false;
    }
}