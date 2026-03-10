using UnityEngine;
using UnityEngine.SceneManagement; // <--- NUEVO: Necesario para reiniciar niveles

public class Start_Game : MonoBehaviour
{
    public GameObject pantallaMenu;     
    public GameObject barraEnergia;
    public GameObject pantallaGameOver; // <--- NUEVO: Para poder apagarla si es necesario

    public void IniciarJuego()
    {
        // Apagamos el menú
        pantallaMenu.SetActive(false);
        
        // Encendemos la interfaz del juego
        barraEnergia.SetActive(true);
        
        // Debug.Log("¡El juego ha empezado!");
    }

    // --- ESTA ES LA FUNCIÓN PARA EL BOTÓN DE RETRY ---
    public void ReiniciarNivel()
    {
        // Esta línea busca el nombre de la escena actual y la vuelve a cargar
        // Es la forma más limpia de resetear robots, enemigos y energía
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}