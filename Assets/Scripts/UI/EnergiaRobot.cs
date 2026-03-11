using UnityEngine;
using UnityEngine.UI; // Esto es obligatorio para usar la barra

public class EnergiaRobot : MonoBehaviour
{
    public float energiaMaxima = 100f;
    public float energiaActual;
    public Image barraVisual; // Aquí arrastraremos el 'Relleno_Bateria'

    void Start()
    {
        energiaActual = energiaMaxima;
    }

    void Update()
    {
        // Actualizamos la barra cada frame
        barraVisual.fillAmount = energiaActual / energiaMaxima;

        // Si la energía llega a 0, podrías activar la Pantalla_GameOver
        if (energiaActual <= 0)
        {
            Debug.Log("Robot sin batería");
        }
    }

    // Esta función la usaremos para gastar energía desde otros scripts
    public void ConsumirEnergia(float cantidad)
    {
        energiaActual -= cantidad;
    }
}