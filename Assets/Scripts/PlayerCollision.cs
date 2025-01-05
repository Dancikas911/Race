using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public AudioClip explosionSound;   // Sprogimo garsas

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            // Paslėpk žaidėją
            gameObject.SetActive(false);

            // Rodyk pralaimėjimo ekraną
            Invoke("GameOver", 2f);
        }
    }

    void GameOver()
    {
        // Galima pridėti Game Over logiką (pvz., scenos perkrovimą)
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

}
