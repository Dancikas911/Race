using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    private bool isFollowing = true;

    void Update()
    {
        if (isFollowing)
        {
            // Apskaičiuoti kryptį į žaidėją
            Vector3 direction = (player.position - transform.position).normalized;

            // Perkelti link žaidėjo
            transform.position += direction * speed * Time.deltaTime;

            // Apskaičiuoti rotaciją tik Z ašiai
            float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Pridėti 90 laipsnių, jei modelis yra sukasi 90 laipsnių nuo priekio
            transform.rotation = Quaternion.Euler(0, 0, angleZ + 90);
        }
    }

    // Sekimo stabdymas
    public void StopFollowing()
    {
        isFollowing = false;
    }

    // Sekimo atnaujinimas
    public void StartFollowing()
    {
        isFollowing = true;
    }
}
