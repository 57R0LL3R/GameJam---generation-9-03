using UnityEngine;
using UnityEngine.SceneManagement; 

public class PortalNivel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CurrentState.current.level = CurrentView.l2;
            CurrentState.current.view = CurrentView.l2;
        }
    }
}