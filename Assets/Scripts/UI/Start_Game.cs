using UnityEngine;
using UnityEngine.SceneManagement; // <--- Línea obligatoria para cambiar de escena

public class Start_Game : MonoBehaviour
{


    public void IniciarJuego()
    {
        CurrentState.current.view = CurrentState.current.level;
        GameController.Instance.stateGame = StateGame.playing;

    }

    public void ReiniciarNivel()
    {
        IniciarJuego();
    }
}