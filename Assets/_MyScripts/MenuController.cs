using UnityEngine;
using UnityEngine.SceneManagement; // Обов'язково для роботи зі сценами

public class MenuController : MonoBehaviour
{
    public void StartMission()
    {
        // Завантажує наступну сцену за номером у списку Build Settings
        SceneManager.LoadScene(1); 
    }

    public void ExitGame()
    {
        Application.Quit(); // Закриває гру (працює тільки в .exe)
        Debug.Log("Game Closed");
    }
} // Final version for PR 