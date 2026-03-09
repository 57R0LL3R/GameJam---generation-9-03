using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float forceJump = 1f,
    timer , inter= 30;
    PlayerInput playerInput;
    bool canJump=true;
    AudioSource aus;
    Animator animator;
    string State = "State";
    ParticleSystem particleSystem;
    
    public StateGame stateGame {get;set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateGame = StateGame.playing;
        aus = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetInteger(State,0);
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        var jumping = playerInput.actions["Jump"].ReadValue<float>();
        if(timer > inter && jumping > 0 && stateGame == StateGame.dead)
        {
            timer=0;
            stateGame = StateGame.playing;
            ChageStateAnimationAndParticules(0);
        }
        if(canJump && StateGame.playing==stateGame)
        {
            rb.AddForce(transform.up*forceJump*jumping,ForceMode.Impulse);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ChageStateAnimationAndParticules(1,false);
            aus.Play();
            canJump = false;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ChageStateAnimationAndParticules(0);
            canJump = true;
            
        }
        else
        {
            timer=0;
            ChageStateAnimationAndParticules(2,false);
            stateGame = StateGame.dead;
        }
    }
    void ChageStateAnimationAndParticules(int newState,bool particulesOn = true)
    {
        switch (stateGame)
        {
            
            case StateGame.dead:
                particleSystem.Stop();
            break;
            case StateGame.menu:

            break;
            case StateGame.playing:
                animator.SetInteger(State,newState);
                if(particulesOn)
                particleSystem.Play();
                else
                particleSystem.Stop();
            break;
        }
            
        
        
    }
}
