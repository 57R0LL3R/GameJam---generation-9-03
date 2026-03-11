
using System.Collections;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D enemyRb;
    SpriteRenderer spriteRenderer;
    [SerializeField] float speedEnemy = 2,forcedash = 1;
    public GameObject sword;
    int direction = 1;
    [SerializeField]Transform player;
    [SerializeField]Vector3 distance;
    [SerializeField] bool moving = true;
    Animator animator ;
    string tipeMove = "TipeMove";
    string attack = "Attack";
    void Start()
    {
        
        animator = GetComponent<Animator>();
            DefStateAnimator(1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyRb =GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player").First().transform;
        StartCoroutine(nameof(AnimatorChange));
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    IEnumerator AnimatorChange()
    {
        while (true)
        {
            yield return new WaitUntil(() => moving == true);
            
            DefStateAnimator(1);

            yield return new WaitUntil(() => moving == false);
            DefStateAnimator(0);

        }
    }
    void MoveEnemy()
    {
        if(!moving)return;
        
        enemyRb.linearVelocity = new Vector2(speedEnemy*direction*forcedash,0);
        distance = transform.position - player.position;
        if(Mathf.Abs(distance.x)<8 &&Mathf.Abs(distance.y)<0.9)
        {
            sword.SetActive(false);
            DefStateAttackAnimator(Random.Range(0,2));
            direction = distance.x < 0 ? 1 : -1;
            spriteRenderer.flipX = direction < 0;
            forcedash = 2;
            sword.transform.position = 1.12f * direction * Vector3.right + transform.position;
        }
        else
        {   
            sword.SetActive(false);
            DefStateAttackAnimator(2);
            forcedash = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LimitGround"))
        {
            direction *= -1;
            spriteRenderer.flipX = direction < 0;
        }
        
    }
    private void OnTriggerExit2D(Collider2D c) {
        if (c.CompareTag("Ground"))
        {
            moving = false;
            enemyRb.linearVelocity = Vector2.zero;
            animator.SetInteger("Dead",2);
            Invoke(nameof(Deactive),1f);
        }
    }
    void Deactive()
    {
        gameObject.SetActive(false);
    }
    void DefStateAnimator(int state = 0)
    {
        
        animator.SetInteger(tipeMove,state);
    }
    void DefStateAttackAnimator(int sattack = 0)
    {
        
        animator.SetInteger(attack,sattack);
    }
}
