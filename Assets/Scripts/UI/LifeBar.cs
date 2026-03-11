using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public float amountLife = 1;
    public float Quantity = 1000;
    [SerializeField]float porcentual ;
    Image barLife;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barLife = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        porcentual=amountLife/Quantity;
        barLife.fillAmount =porcentual ;
    }
}
