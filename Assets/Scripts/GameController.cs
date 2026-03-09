using UnityEngine;
public enum StateGame
{
    playing, dead,menu = -1
}
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public StateGame stateGame = StateGame.menu;
    private PlayerController playerControl;
    private ObstaculeGenerator obstaculeControl;
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
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerController>();
        obstaculeControl = GameObject.Find("ObstaculeGenerator").GetComponent<ObstaculeGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        stateGame = playerControl.stateGame;
        switch (stateGame)
        {
            case StateGame.dead:
                obstaculeControl.SetSpawning(false);
            break;
            case StateGame.menu:

            break;
            case StateGame.playing:
                obstaculeControl.SetSpawning(true);
            break;
        }
    }
}
