using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public AudioClip explosionSound;  
    public TextMeshProUGUI timerText;  
    public GameObject restartButton;  
    public TextMeshProUGUI gameOverText;  

    private float timeElapsed;
    private float bestTime = 0f;  
    private bool isRunning = true;

    private void Start()
    {
        if (timerText == null || restartButton == null || gameOverText == null)
        {
            Debug.LogError("UI elements are not assigned in the inspector!");
        }
        else
        {
            
            bestTime = PlayerPrefs.GetFloat("BestTime", 0f); 
            timerText.text = "Time: 0:00\nBest Time: " + FormatTime(bestTime);  
            restartButton.SetActive(false);  
            gameOverText.gameObject.SetActive(false);  
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerText(timeElapsed, bestTime);
        }
    }

    private void UpdateTimerText(float currentTime, float bestTime)
    {
        
        timerText.text = "Time: " + FormatTime(currentTime) + "\n" +
                         "Best Time: " + FormatTime(bestTime);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            gameObject.SetActive(false);  
            isRunning = false;  

            
            if (timeElapsed > bestTime)
            {
                bestTime = timeElapsed;
                PlayerPrefs.SetFloat("BestTime", bestTime);
                PlayerPrefs.Save();
            }

            
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(true);
                gameOverText.text = "Game Over\nTime: " + FormatTime(timeElapsed);
            }

            
            if (restartButton != null)
            {
                restartButton.SetActive(true);
            }

 
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");

        SceneManager.LoadScene("MainMenu");  
    }
}
