using UnityEngine;
using UnityEngine.SceneManagement; 

public class PortalNivel : MonoBehaviour
{
   
    public string nombreSiguienteNivel = "Nivel2"; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Nivel Completado! Cargando: " + nombreSiguienteNivel);
            SceneManager.LoadScene(nombreSiguienteNivel);
        }
    }
}