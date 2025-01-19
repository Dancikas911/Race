using System.Collections;
using UnityEngine;
using TMPro;  // Import TMP namespace

public class PlayerCollision : MonoBehaviour
{
    public AudioClip explosionSound;
    public Timer timer;  // Reference to Timer script

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Wall")  // Check by name, no tag needed
        {
            gameObject.SetActive(false);

            // Reset TMP timer
            if (timer != null)
            {
                timer.ResetTimer();  // Reset Timer (TMP)
            }

            Invoke("GameOver", 2f);
        }
    }

    void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
