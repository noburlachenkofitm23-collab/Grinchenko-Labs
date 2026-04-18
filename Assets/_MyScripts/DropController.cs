using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Потрібно для перезавантаження гри

public class DropController : MonoBehaviour
{
    public Rigidbody2D rb;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText; // Сюди перетягнемо текст життів
    
    private int score = 0;
    private int cores = 3;
    private bool isStopped = false;
    private Vector3 startPosition;

    void Start()
    {
        // Запам'ятовуємо, де м'яч був на початку
        startPosition = transform.position;
        UpdateUI();
    }

    void Update()
    {
        if (!isStopped && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            StopObject();
        }

        // Якщо життів 0 — натисни R для рестарту
        if (cores <= 0 && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    void StopObject()
    {
        isStopped = true;
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        
        score += 10;
        UpdateUI();
        
        // Через 1 секунду спавнимо новий м'яч (для наступної лаби), 
        // а поки просто дамо можливість скинути позицію для тесту
        Invoke("ResetBall", 1f);
    }

    public void LoseLife()
    {
        cores--;
        UpdateUI();
        if (cores > 0) ResetBall();
        else Debug.Log("GAME OVER. Press R to Restart");
    }

    void ResetBall()
    {
        isStopped = false;
        rb.isKinematic = false;
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "STABILITY: " + score + "%";
        if (hpText != null) hpText.text = "CORES: " + cores;
        
        if (cores <= 0) hpText.text = "DEAD SECTOR (Press R)";
    }

    void RestartGame()
    {
        // Перезавантажує поточну сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}