using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DropController : MonoBehaviour
{
    public Rigidbody2D rb;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText;
    
    public float shootForce = 15f; // Швидкість польоту стріли
    private int score = 0;
    private int cores = 3;
    private bool isFlying = false;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        rb.isKinematic = true; // Стріла "заморожена" на старті
        UpdateUI();
    }

    void Update()
    {
        // Клік = постріл
        if (!isFlying && cores > 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Shoot();
        }

        if (cores <= 0 && Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Shoot()
    {
        isFlying = true;
        rb.isKinematic = false;
        rb.linearVelocity = Vector2.down * shootForce; // Летить строго вниз
    }

    // Метод зупинки при влучанні
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) // Переконайся, що у платформи тег "Platform"
        {
            StopAndReset();
            score += 10;
            UpdateUI();
        }
    }

    void StopAndReset()
    {
        isFlying = false;
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        transform.position = startPos;
    }

    public void LoseLife()
    {
        cores--;
        UpdateUI();
        if (cores > 0) StopAndReset();
    }

    void UpdateUI()
    {
        scoreText.text = "ACCURACY: " + score + "%";
        hpText.text = "ARROWS: " + cores;
        if (cores <= 0) hpText.text = "OUT OF AMMO (R)";
    }
}