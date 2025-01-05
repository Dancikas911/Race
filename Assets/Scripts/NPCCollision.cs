using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollision : MonoBehaviour
{
    public AudioClip explosionSound;   // Sprogimo garsas
    public Transform respawnPoint;     // NPC atgimimo taškas

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

            // Paslėpk NPC ir atgimk po 2 sekundžių
            gameObject.SetActive(false);
            Invoke("Respawn", 2f);
        }
    }

    void Respawn()
    {
        // Grąžina NPC į pradinę vietą
        transform.position = respawnPoint.position;
        gameObject.SetActive(true);
    }
}

