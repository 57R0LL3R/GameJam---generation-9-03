using UnityEngine;

public class ObstaculeController : MonoBehaviour
{
    ParticleSystem particleSystem;
    Rigidbody rb;
    float Velocity; 
    public int direction = -1;
    public int choque;
    AudioSource Aux;
    private bool outOfArea;
    bool stop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Aux = GetComponent<AudioSource>();
        Velocity=Random.Range(20f,30f);
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop)
            rb.linearVelocity = transform.right*Velocity*direction;
           
        if(transform.position.x <-10 ||transform.position.y <-10 )
        { 
            gameObject.SetActive(false);
            outOfArea = true;
        }
        if (outOfArea && gameObject.activeInHierarchy)
        {
            outOfArea = false;
            stop = false;
        }
    }
        void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(transform.right*10,ForceMode.VelocityChange);
            //rb.Sleep();
            stop = true;
            Aux.Play();

            particleSystem.Play();
            Invoke( "Desactive",4);

        }
    }
    void Desactive()
    {
            outOfArea = true;
        gameObject.SetActive(false);
            rb.linearVelocity =  Vector3.zero;
    }
}
