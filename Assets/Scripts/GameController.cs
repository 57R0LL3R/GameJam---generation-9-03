using UnityEngine;
public enum StateGame
{
    playing, dead,menu = -1
}
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public StateGame stateGame = StateGame.menu;
    void Awake()
    {
        if (Instance == null)
        {
            // Primer AudioManager: lo guardamos y hacemos que persista entre escenas.
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Ya había un AudioManager: evitamos duplicados destruyendo este objeto.
            Destroy(gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
