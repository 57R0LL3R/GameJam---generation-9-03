using UnityEngine;

public class Jumpad : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            {
                Vector2 jumpVector = new Vector2(0, 1);
                rb.AddForce(jumpVector * 150, ForceMode2D.Impulse);
                Debug.Log("Jump Pad Activated");
            }
        }
    }
}
