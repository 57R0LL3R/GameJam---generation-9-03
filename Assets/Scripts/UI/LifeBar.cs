using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public float amountLife = 1000;
    public float Quantity = 1000;
    [SerializeField]float porcentual ;
    bool DeadB = true;
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
        barLife.fillAmount = porcentual ;
        if (porcentual < 0.01f)
        {
            var animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
            animator.SetInteger("Dead",1);
            animator.SetBool("isWalking",false);
            animator.SetBool("isJumping",false);
            animator.SetBool("TookDamage",false);
            animator.SetBool("DeadB",DeadB);
            DeadB=false;
            Debug.Log("|||||||||||||||||||||||||||||||||||||||||||");
            Debug.Log("Murio");
            GameController.Instance.stateGame  = StateGame.dead;
            Invoke(nameof(Dead),2);
        }
    }
    void Dead()
    {
        CurrentState.current.view = CurrentView.Menu;
    }
}
