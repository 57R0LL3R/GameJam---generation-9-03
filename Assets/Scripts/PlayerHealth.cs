using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar el nivel

public class PlayerHealth : MonoBehaviour
{
    [Header("Ajustes de Vida")]
    public int maxHealth = 5;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // --- ESTA ES LA PARTE QUE FALTABA ---
    // Detecta cuando el Player entra en un Trigger (Sierras, Pinchos, etc.)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto tiene el Tag "Trap"
        if (collision.CompareTag("Trap"))
        {
            Debug.Log("¡Tocado por trampa!");
            TakeDamage(1); // Quita 1 de vida
        }
        else if (collision.CompareTag("Sword"))
        {
            TakeDamage(2);
        }
        else if (collision.CompareTag("Enemy"))
        {
            TakeDamage(2);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Jugador recibió daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Jugador murió");
        
        // En lugar de solo desactivar el objeto, reiniciamos el nivel
        // Esto hace que el jugador reaparezca en el inicio automáticamente
        ReiniciarNivel();
    }

    void ReiniciarNivel()
    {
        // Obtiene el nombre de la escena actual y la vuelve a cargar
        string nombreEscena = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nombreEscena);
    }
}