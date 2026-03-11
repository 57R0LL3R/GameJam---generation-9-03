using UnityEngine;

public class MenuManger : MonoBehaviour
{
    public GameObject menuGameOver, menuNormal, menuGamePlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        NormalMode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayMode()
    {
        Debug.Log("clickONpLayMode");
        menuGameOver.SetActive(false);
        menuNormal.SetActive(false);
        menuGamePlay.SetActive(true);
    }
    public void OverMode()
    {
        
        menuGameOver.SetActive(false);
        menuGamePlay.SetActive(false);
        menuGameOver.SetActive(true);
    }
    public void NormalMode()
    {
        menuGameOver.SetActive(false);
        menuGamePlay.SetActive(false);
        menuNormal.SetActive(true);
    }
}
