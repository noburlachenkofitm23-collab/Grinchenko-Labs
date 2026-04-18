using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public float speed = 3f; // Швидкість руху
    public float distance = 5f; // На яку відстань від'їжджає
    private Vector3 startPos;

    void Start() => startPos = transform.position;

    void Update()
    {
        // Рух вліво-вправо за синусоїдою (пінг-понг)
        float newX = startPos.x + Mathf.Sin(Time.time * speed) * distance;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}