using UnityEngine;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    public AudioClip explosionSound;  // Garso efektas, kai įvyksta susidūrimas
    public TextMeshProUGUI timerText;  // TMP tekstas laikmačiui atvaizduoti

    private float timeElapsed;
    private float bestTime = 0f;  // Geriausias laikas
    private bool isRunning = true;

    private void Start()
    {
        // Patikriname, ar timerText yra priskirtas
        if (timerText == null)
        {
            Debug.LogError("TimerText is not assigned in the inspector!");
        }
        else
        {
            // Panaudojame PlayerPrefs, kad įkeltume geriausią laiką iš išsaugotų duomenų
            bestTime = PlayerPrefs.GetFloat("BestTime", 0f); // Jei nėra reikšmės, naudosime 0 kaip numatytąją reikšmę
            timerText.text = "Time: 0:00\nBest Time: " + FormatTime(bestTime);  // Inicializuojame tekstą
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
        Debug.Log("Collision detected with: " + collision.collider.name);
        if (collision.collider != null)  // Patikrinama, ar Collider egzistuoja
        {
            Debug.Log("Collision detected with Collider2D");
            gameObject.SetActive(false);  // Paslėpti žaidėjo objektą

            // Sustabdykite laikmatį
            isRunning = false;

            // Atnaujinti geriausią laiką
            if (timeElapsed > bestTime)
            {
                bestTime = timeElapsed;  // Atnaujiname geriausią laiką
                PlayerPrefs.SetFloat("BestTime", bestTime);  // Išsaugome geriausią laiką PlayerPrefs
                PlayerPrefs.Save();  // Užtikriname, kad duomenys bus išsaugoti
            }

            // Reset TMP tekstas
            if (timerText != null)
            {
                timerText.text = "Time: 0:00\nBest Time: " + FormatTime(bestTime);
            }

            // Garso efektas
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }

            // Po 2 sekundžių įvyksta žaidimo pabaiga
            Invoke("GameOver", 2f);
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver function called, reloading scene...");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    // Šis metodas gali būti naudojamas, kad būtų galima atnaujinti laikmatį ir geriausią laiką po to, kai žaidimas prasideda iš naujo
    public void ResetTimer()
    {
        timeElapsed = 0f;  // Atstatome laikmatį į 0
        isRunning = true;  // Pradeda vėl veikti laikmatis
    }
}
