using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManagerL1 : MonoBehaviour
{
    [SerializeField]GameObject[] menus;
    [SerializeField]TextMeshProUGUI tiptext;
    PlayerInput input;
    public static MenuManagerL1 instance;
    void Awake()
    {
        if(instance!=null)
        Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input  = gameObject.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            foreach(var i in menus)
            {
                i.SetActive(false);
            }
        }
    }
    public void Active(int i,string tip = "")
    {
        menus[i].SetActive(true);
        if(tip!="")
        tiptext.text =  tip;
        if(i==2)
        Invoke(nameof(Deactive),2);
    }
    void Deactive()
    {
            foreach(var i in menus)
            {
                i.SetActive(false);
            }
    }
}
