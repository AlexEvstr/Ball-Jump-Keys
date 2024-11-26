using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Персонаж
    private float followSpeed = 2f; // Скорость следования камеры
    public Vector3 offset; // Смещение камеры

    private Vector3 velocity = Vector3.zero; // Для сглаживания движения

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Желаемая позиция камеры
            Vector3 desiredPosition = target.position + offset;

            // Ограничиваем высоту камеры (минимум 0 по оси Y)
            desiredPosition.y = Mathf.Max(desiredPosition.y, 0f);

            // Плавное следование камеры
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / followSpeed);
        }
    }
}
