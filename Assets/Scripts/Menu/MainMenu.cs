using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Funkcija, kuri bus iškviečiama paspaudus mygtuką "Start"
    public void StartGame()
    {
        // Įkelia kitą sceną, pavyzdžiui, "GameScene"
        SceneManager.LoadScene("Pursuit");
    }
}
