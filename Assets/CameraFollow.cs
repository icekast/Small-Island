using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Объект, за которым следует камера (игрок)
    public float smoothSpeed = 0.125f; // Плавность движения
    public Vector3 offset; // Смещение камеры относительно игрока

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}