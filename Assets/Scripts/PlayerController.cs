using System;
using UnityEngine;
using UnityEngine.InputSystem; // Librería necesaria para usar el nuevo Input System de Unity

// Controlador principal del jugador
// Maneja: movimiento, salto, power-up de salto, muerte y respawn
public class PlayerController : MonoBehaviour
{
    // CONFIGURACIÓN DE MOVIMIENTO
    [Header("Movimiento")]
    public float moveSpeed = 5f; 
    // velocidad horizontal automática del jugador (tipo runner)

    // CONFIGURACIÓN DEL SALTO
    [Header("Salto")]
    public float jumpForce = 5f; 
    // fuerza base del salto

    public float jumpBoostMultiplier = 2f; 
    // multiplicador cuando el jugador tiene boost de salto

    // CONFIGURACIÓN DEL POWER-UP
    [Header("Sistema Boost")]
    public float boostDuration = 5f; 
    // duración del power-up de salto en segundos

    // CONFIGURACIÓN DEL RESPAWN
    [Header("Respawn")]
    public float respawnTime = 3f; 
    // tiempo que debe esperar el jugador antes de poder revivir

    // REFERENCIAS A COMPONENTES
    Rigidbody rb; // controla la física del jugador
    PlayerInput playerInput; // sistema de entrada del jugador

    // VARIABLES DE CONTROL
    bool canJump = true; 
    // indica si el jugador puede saltar (evita doble salto)

    bool jumpBoostActive = false; 
    // indica si el jugador tiene el power-up activo

    float boostTimer; 
    // contador del tiempo restante del boost

    float deathTimer; 
    // contador del tiempo después de morir

    // COMPONENTES ADICIONALES
    AudioSource aus; 
    // reproduce sonidos del jugador

    Animator animator; 
    // controla animaciones del personaje

    ParticleSystem particleSystem; 
    // controla partículas del personaje

    string State = "State"; 
    // nombre del parámetro del Animator

    // estado del juego (playing, dead, menu)
    public StateGame stateGame { get; set; }

    // INICIALIZACIÓN
    void Start()
    {
        // El juego inicia en estado "playing"
        stateGame = StateGame.playing;

        // Obtener referencias de los componentes del objeto Player
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        aus = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        particleSystem = GetComponent<ParticleSystem>();

        // Estado inicial de animación
        animator.SetInteger(State, 0);
    }

    // UPDATE (Cada frame)
    void Update()
    {
        // Controla duración del power-up
        ManageBoost();

        // Controla el respawn cuando el jugador muere
        ManageRespawn();
    }

    // FIXED UPDATE (FÍSICA)
    void FixedUpdate()
    {
        // Si el juego no está en estado "playing", no se ejecuta movimiento ni salto
        if (stateGame != StateGame.playing) return;

        Move(); // movimiento horizontal
        Jump(); // salto
    }

    // MOVIMIENTO AUTOMÁTICO
    void Move()
    {
        // guardamos la velocidad actual
        Vector3 velocity = rb.linearVelocity;

        // modificamos solo el eje X
        velocity.x = moveSpeed;

        // aplicamos la nueva velocidad
        rb.linearVelocity = velocity;
    }

    // SISTEMA DE SALTO
    void Jump()
    {
        // Leemos la acción Jump del Input System
        var jumping = playerInput.actions["Jump"].ReadValue<float>();

        // Si se presiona el botón y el jugador puede saltar
        if (jumping > 0.1f && canJump)
        {
            // fuerza base de salto
            float finalJumpForce = jumpForce;

            // si el boost está activo, multiplicamos la fuerza
            if (jumpBoostActive)
                finalJumpForce *= jumpBoostMultiplier;

            // aplicamos fuerza hacia arriba
            rb.AddForce(Vector3.up * finalJumpForce, ForceMode.Impulse);

            // evitamos doble salto
            canJump = false;

            // cambiar animación a salto
            ChageStateAnimationAndParticules(1, false);

            // reproducir sonido
            aus.Play();
        }
    }

    // CONTROL DEL POWER-UP
    void ManageBoost()
    {
        // si no hay boost activo, salir
        if (!jumpBoostActive) return;

        // reducimos el tiempo restante
        boostTimer -= Time.deltaTime;

        // cuando termina el tiempo se desactiva
        if (boostTimer <= 0)
        {
            jumpBoostActive = false;
        }
    }

    // CONTROL DEL RESPAWN
    void ManageRespawn()
    {
        // solo funciona si el jugador está muerto
        if (stateGame != StateGame.dead) return;

        // aumentamos contador de tiempo
        deathTimer += Time.deltaTime;

        // detectar si el jugador presiona salto
        var jumping = playerInput.actions["Jump"].ReadValue<float>();

        // si pasó el tiempo de respawn y presiona salto revive
        if (deathTimer >= respawnTime && jumping > 0.1f)
        {
            stateGame = StateGame.playing;
            deathTimer = 0;

            // volver a animación normal
            ChageStateAnimationAndParticules(0);
        }
    }

    // ACTIVAR POWER-UP
    public void ActivateJumpBoost()
    {
        // activar boost
        jumpBoostActive = true;

        // reiniciar contador
        boostTimer = boostDuration;
    }

    // DETECTAR COLISIONES
    void OnCollisionEnter(Collision collision)
    {
        // si toca el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;

            // animación normal
            ChageStateAnimationAndParticules(0);
        }

        // si choca con obstáculo
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    // cuando el jugador deja de tocar el suelo
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    // MUERTE DEL JUGADOR
    void Die()
    {
        // cambiar estado del juego
        stateGame = StateGame.dead;

        // reiniciar contador de respawn
        deathTimer = 0;

        // animación de muerte
        ChageStateAnimationAndParticules(2, false);
    }

    // CONTROL DE ANIMACIONES Y PARTÍCULAS
    void ChageStateAnimationAndParticules(int newState, bool particulesOn = true)
    {
        switch (stateGame)
        {
            case StateGame.dead:

                // detener partículas cuando el jugador muere
                particleSystem.Stop();

                break;

            case StateGame.playing:

                // cambiar estado de animación
                animator.SetInteger(State, newState);

                // activar o desactivar partículas
                if (particulesOn)
                    particleSystem.Play();
                else
                    particleSystem.Stop();

                break;
        }
    }
}