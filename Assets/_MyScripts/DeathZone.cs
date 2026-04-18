using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Шукаємо скрипт контролера на об'єкті, який впав
        DropController controller = other.GetComponent<DropController>();
        if (controller != null)
        {
            controller.LoseLife();
        }
    }
}