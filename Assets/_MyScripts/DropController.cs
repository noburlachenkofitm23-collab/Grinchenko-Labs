using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DropController : MonoBehaviour
{
    // --- ЗМІННІ ---
    public Rigidbody2D rb;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText;
    public GameObject[] ammoIcons; // Наші іконки ракет

    public float shootForce = 15f;
    private int score = 0;
    private int cores = 3;
    private bool isFlying = false;
    private Vector3 startPos;
    public GameObject explosionPrefab; // Сюди перетягнемо наш префаб

    // Додай це вгору до змінних
    public MovingTarget targetScript; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            // СТВОРЮЄМО ВИБУХ у точці влучання
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            StopAndReset();
            score += 10;
            UpdateUI();
            if (targetScript != null) targetScript.IncreaseDifficulty();
        }
    }

    void Start()
    {
        startPos = transform.position;
        rb.isKinematic = true; 
        UpdateUI();
    }

    void Update()
    {
        // Постріл на Пробіл або Клік
        if (!isFlying && cores > 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Shoot();
        }

        // Перезапуск на R
        if (cores <= 0 && Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Shoot()
    {
        isFlying = true;
        rb.isKinematic = false;
        rb.linearVelocity = Vector2.down * shootForce;
    }



    public void LoseLife()
    {
        cores--;
        UpdateUI();
        if (cores > 0) StopAndReset();
    }

    void StopAndReset()
    {
        isFlying = false;
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        transform.position = startPos;
    }

    void UpdateUI()
    {
        // Оновлюємо наш HUD ППО
        if (scoreText != null)
            scoreText.text = "DESTROYED: " + (score / 10).ToString("D2");

        // Керуємо видимістю іконок ракет
        for (int i = 0; i < ammoIcons.Length; i++)
        {
            if (ammoIcons[i] != null)
            {
                // Умикаємо тільки ті іконки, на які вистачає ракет (cores)
                ammoIcons[i].SetActive(i < cores);
            }
        }

        // Текст, якщо боєзапас порожній
        if (hpText != null)
        {
            hpText.text = (cores <= 0) ? "MAGAZINE EMPTY / PRESS (R) TO RELOAD" : "";
        }
    }
}