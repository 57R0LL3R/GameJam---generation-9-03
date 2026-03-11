using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar el nivel
public enum StatePlayer
{
    die,life,inMenu
}
public class PlayerHealth : MonoBehaviour
{

    [Header("Ajustes de Vida")]
    Animator anim;
    Powers powers;
    bool TookDamage = false;

    public LifeBar lifeBar;
    public GameObject Bateria;
    [SerializeField] MenuManger menuManger;
    void Start()
    {
        anim = GetComponent<Animator>();
        powers = GetComponent<Powers>();
        if(Bateria!=null)
        lifeBar = Bateria.GetComponent<LifeBar>();
        else
        lifeBar = GameObject.Find("Carga_Batería").GetComponent<LifeBar>();
    }
    void Update()
    {
        anim.SetBool("TookDamage", TookDamage);
        lifeBar.amountLife = powers.energy;
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
            TakeDamage(20);


        }
        else if (collision.CompareTag("Enemy"))
        {
            TakeDamage(20);


        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
        {
            Debug.Log("¡Tocado por trampa!");
            TakeDamage(10); // Quita 10 de energia
            TookDamage = false;

        }

        else if (other.CompareTag("Sword"))
        {
            TakeDamage(20);
            TookDamage = false;

        }
        else if (other.CompareTag("Enemy"))
        {
            TakeDamage(20);
            TookDamage = false;

        }
    }

    public void TakeDamage(int damage)
    {
        powers.energy -= damage;
        Debug.Log("Jugador recibió daño. Energia restante restante: " + powers.energy);
        TookDamage = true;
        if (powers.energy <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        menuManger.OverMode();
        Debug.Log("Jugador murió");
        Powers.player = StatePlayer.die;
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