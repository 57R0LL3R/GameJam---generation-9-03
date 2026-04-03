using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum CurrentView
{
    Menu,l1,l2,l3
}

public class CurrentState : MonoBehaviour
{
    public CurrentView view = CurrentView.Menu;
    public static CurrentState current;
    public CurrentView level = CurrentView.l1;
    void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
        }
        else
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
    }

 
    void Start()
    {
        StartCoroutine(ChangeView());
    }
    IEnumerator ChangeView()
    {
        CurrentView last = view;
        while (true)
        {
            if(last != view)
            {
                last = view;
                SceneManager.LoadScene((int)view);
            }
            yield return null;
        }
    }

}
