using UnityEngine;

public class Jumpad : MonoBehaviour
{
    public float JumpadForce = 40;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            {
                Vector2 jumpVector = new Vector2(0, 1);
                rb.linearVelocity = jumpVector * JumpadForce;
                Debug.Log("Jump Pad Activated");
            }
        }
    }
}
