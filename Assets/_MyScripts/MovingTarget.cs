using UnityEngine;
using System.Collections; // Потрібно для роботи корутин

public class MovingTarget : MonoBehaviour
{
    public float baseSpeed = 2f; 
    public float currentSpeed;
    public float distance = 5f;
    public float speedMultiplier = 0.2f;
    
    private Vector3 startPos;
    private float phase = 0f;

    // Посилання на компоненти, які будемо ховати
    private SpriteRenderer sprite;
    private Collider2D col;

    void Start()
    {
        startPos = transform.position;
        currentSpeed = baseSpeed;
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Рух продовжується, навіть якщо шахед невидимий (він готується до появи)
        phase += Time.deltaTime * currentSpeed;
        float newX = startPos.x + Mathf.Sin(phase) * distance;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    public void IncreaseDifficulty()
    {
        currentSpeed += speedMultiplier;
        // Запускаємо процес переродження
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        // 1. Ховаємо шахед
        sprite.enabled = false;
        col.enabled = false;

        // 2. Чекаємо, наприклад, 1.5 секунди
        yield return new WaitForSeconds(1.5f);

        // 3. З'являємо знову
        sprite.enabled = true;
        col.enabled = true;
    }
}