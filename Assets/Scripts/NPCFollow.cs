using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    private bool isFollowing = true;  // Valdo sekimą

    void Update()
    {
        if (isFollowing)
        {
            // NPC juda link žaidėjo
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Metodas sekimui sustabdyti
    public void StopFollowing()
    {
        isFollowing = false;
    }

    // Metodas sekimui pradėti iš naujo
    public void StartFollowing()
    {
        isFollowing = true;
    }
}