using UnityEngine;
using UnityEngine.SceneManagement; // <--- Línea obligatoria para cambiar de escena

public class Start_Game : MonoBehaviour
{
    [Header("Configuración de Escenas")]
    public string nombreNivel1 = "Level_01";


    public void IniciarJuego()
    {
       // SceneManager.LoadScene(nombreNivel1);
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}