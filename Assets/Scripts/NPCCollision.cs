using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollision : MonoBehaviour
{
    public ParticleSystem collisionEffect;
    public AudioClip explosionSound;
    private AudioSource audioSource;
    private Vector3 initialPosition;
    private Rigidbody2D rb;
    private NPCFollow npcFollow;  
    private bool isStopped = false;

    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();

     
        npcFollow = GetComponent<NPCFollow>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource komponentas nepriskirtas!");
        }

        
        if (collisionEffect != null)
        {
            collisionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        if (audioSource != null)
        {
            audioSource.Stop(); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            StopObjectMovement();

            
            if (collisionEffect != null)
            {
                collisionEffect.gameObject.SetActive(true); 
                collisionEffect.Play();
            }

           
            if (audioSource != null && explosionSound != null)
            {
                audioSource.PlayOneShot(explosionSound);
            }

            StartCoroutine(ReturnToStartPosition());
        }
    }

    void StopObjectMovement()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;
        isStopped = true;

        
        if (npcFollow != null)
        {
            npcFollow.StopFollowing();
        }
    }

    IEnumerator ReturnToStartPosition()
    {
        yield return new WaitForSeconds(4);

        transform.position = initialPosition;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.simulated = true;
        isStopped = false;

        
        if (npcFollow != null)
        {
            npcFollow.StartFollowing();
        }
    }

    void FixedUpdate()
    {
        if (isStopped)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
