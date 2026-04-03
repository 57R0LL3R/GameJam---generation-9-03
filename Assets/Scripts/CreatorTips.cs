using UnityEngine;

public class CreatorTips : MonoBehaviour
{
    [SerializeField] string message = "holaS";
    [SerializeField] int number = 2;
    GameObject other;
    private void OnTriggerEnter2D(Collider2D other) {
        
        Debug.Log("entor el");
        if (other.CompareTag("Player"))
        {
            this.other=other.gameObject;
            if(other.GetComponent<Powers>().hasJetpack &&number==2)return;
            MenuManagerL1.instance.Active(number,message);
            if (gameObject.CompareTag("Tp"))
            {
                Invoke(nameof(Tp),2);
                return;
            }
            Debug.Log("player");
            
        }
    }
    void Tp()
    {
        other.transform.position = new Vector3(69,11,0);
    }

}
