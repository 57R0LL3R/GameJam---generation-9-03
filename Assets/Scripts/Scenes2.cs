using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenes2 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Jugador = GameObject.Find("Player");

        Powers powers = Jugador.GetComponent<Powers>();

        if (powers.hasKey)
        {
            SceneManager.LoadScene("Nivel_Integración");
        }
    }
}
