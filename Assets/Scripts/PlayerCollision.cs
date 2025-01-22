using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public AudioClip explosionSound;  // Garso efektas, kai įvyksta susidūrimas
    public TextMeshProUGUI timerText;  // TMP tekstas laikmačiui atvaizduoti
    public GameObject restartButton;  // Restart mygtukas UI
    public TextMeshProUGUI gameOverText;  // TMP tekstas Game Over pranešimui

    private float timeElapsed;
    private float bestTime = 0f;  // Geriausias laikas
    private bool isRunning = true;

    private void Start()
    {
        if (timerText == null || restartButton == null || gameOverText == null)
        {
            Debug.LogError("UI elements are not assigned in the inspector!");
        }
        else
        {
            // Panaudojame PlayerPrefs, kad įkeltume geriausią laiką iš išsaugotų duomenų
            bestTime = PlayerPrefs.GetFloat("BestTime", 0f); // Jei nėra reikšmės, naudosime 0 kaip numatytąją reikšmę
            timerText.text = "Time: 0:00\nBest Time: " + FormatTime(bestTime);  // Inicializuojame tekstą
            restartButton.SetActive(false);  // Paslėpiame Restart mygtuką, kol žaidimas nevyksta
            gameOverText.gameObject.SetActive(false);  // Paslėpiame Game Over tekstą
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
        // Rodome laiką ir geriausią laiką
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
            gameObject.SetActive(false);  // Paslėpti žaidėjo objektą
            isRunning = false;  // Sustabdyti laikmatį

            // Atnaujinti geriausią laiką
            if (timeElapsed > bestTime)
            {
                bestTime = timeElapsed;
                PlayerPrefs.SetFloat("BestTime", bestTime);
                PlayerPrefs.Save();
            }

            // Parodyti Game Over tekstą
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(true);
                gameOverText.text = "Game Over\nTime: " + FormatTime(timeElapsed);
            }

            // Parodyti Restart mygtuką
            if (restartButton != null)
            {
                restartButton.SetActive(true);
            }

            // Groti garsą
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");

        // Naudokite scenos pavadinimą vietoj build index
        SceneManager.LoadScene("MainMenu");  // Pakeiskite į savo pagrindinę sceną, kuri turi žaidimo logiką
    }
}
