using UnityEngine;

public class Jumpad : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            {
                Vector2 jumpVector = new Vector2(0, 1);
                rb.AddForce(jumpVector * 180, ForceMode2D.Impulse);
                Debug.Log("Jump Pad Activated");
            }
        }
    }
}
