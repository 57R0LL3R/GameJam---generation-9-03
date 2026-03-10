using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Iniciando");
        SceneManager.LoadScene("Pruebas");
    }
    public void Quit()
    {
        Application.Quit();
    }

}
