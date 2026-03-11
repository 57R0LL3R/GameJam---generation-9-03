using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip jumpSound;
    public AudioClip walkSound;
    public AudioClip flySound;
    public AudioClip menuMusic;
    Powers powers;

    float walkCooldown = 0.35f;
    float lastWalkTime;

    bool flying = false;
    void Start()
    {
        powers = GetComponent<Powers>();
    }
    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayWalk()
    {
        if (Time.time - lastWalkTime > walkCooldown)
        {
            audioSource.PlayOneShot(walkSound);
            lastWalkTime = Time.time;
        }
    }

    public void PlayFly()
    {
        if (!flying)
        {
            audioSource.clip = flySound;
            audioSource.loop = true;
            audioSource.Play();
            flying = true;
        }
    }

    public void StopFly()
    {
        if (flying)
        {
            audioSource.Stop();
            flying = false;
        }
    }


}