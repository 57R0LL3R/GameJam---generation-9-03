using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Pruebas");
    }
    public void Quit()
    {
        Application.Quit();
    }

}
