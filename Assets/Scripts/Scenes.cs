using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Jugador = GameObject.FindWithTag("Player");

        Powers powers = Jugador.GetComponent<Powers>();

        if (powers.hasKey)
        {
            CurrentState.current.level = CurrentView.l3;
            CurrentState.current.view = CurrentState.current.level;
        }
    }
}
