using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar el nivel

public class PlayerHealth : MonoBehaviour
{

    [Header("Ajustes de Vida")]

    Powers powers;

    void Start()
    {
        powers = GetComponent<Powers>();
    }


    // Detecta cuando el Player entra en un Trigger (Sierras, Pinchos, etc.)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto tiene el Tag "Trap"
        if (collision.CompareTag("Trap"))
        {
            Debug.Log("¡Tocado por trampa!");
            TakeDamage(10); // Quita 10 de energia
        }

        else if (collision.CompareTag("Sword"))
        {
            
            Die();
            TakeDamage(2);
        }
        else if (collision.CompareTag("Enemy"))
        {
            TakeDamage(2);
        }
    }

    public void TakeDamage(int damage)
    {
        powers.energy -= damage;
        Debug.Log("Jugador recibió daño. Energia restante restante: " + powers.energy);

        if (powers.energy <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Jugador murió");
        Powers.player = PlayerState.die;
        // En lugar de solo desactivar el objeto, reiniciamos el nivel
        // Esto hace que el jugador reaparezca en el inicio automáticamente
        //ReiniciarNivel();
    }

    void ReiniciarNivel()
    {
        // Obtiene el nombre de la escena actual y la vuelve a cargar
        string nombreEscena = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nombreEscena);
    }
}