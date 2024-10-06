using System;
using UnityEngine;

namespace Core
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Collider2D backgroundCollider;
        [SerializeField] private float smoothSpeed = 0.7f;

        private Vector3 _velocity = Vector3.zero;

        private void Awake()
        {
            Cursor.visible = true;
        }

        private void Update()
        {
            // Отримуємо світову позицію курсора
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = mainCamera.transform.position.z; // Зберігаємо поточну позицію по осі Z

            // Обмежуємо рух камери межами колайдера
            Vector3 clampedPosition = ClampCamera(mousePosition);

            // Плавно рухаємо камеру до нової позиції з використанням SmoothDamp для більшого згладження
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, clampedPosition, ref _velocity, smoothSpeed);
        }

        private Vector3 ClampCamera(Vector3 targetPosition)
        {
            float cameraHalfHeight = mainCamera.orthographicSize;
            float cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;

            Bounds bounds = backgroundCollider.bounds;

            float minX = bounds.min.x + cameraHalfWidth;
            float maxX = bounds.max.x - cameraHalfWidth;
            float minY = bounds.min.y + cameraHalfHeight;
            float maxY = bounds.max.y - cameraHalfHeight;

            return new Vector3(
                Mathf.Clamp(targetPosition.x, minX, maxX),
                Mathf.Clamp(targetPosition.y, minY, maxY),
                targetPosition.z
            );
        }
    }
}
