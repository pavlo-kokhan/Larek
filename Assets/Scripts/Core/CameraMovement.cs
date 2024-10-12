using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Collider2D backgroundCollider;
        [SerializeField] private float smoothSpeed = 0.7f;
        [SerializeField] private List<PanelController> panelControllers;

        private Vector3 _velocity = Vector3.zero;

        private void Start()
        {
            Cursor.visible = true;

            foreach (var controller in panelControllers)
            {
                controller.PanelActivationChanged += status => enabled = !status;
            }
        }

        private void Update()
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = mainCamera.transform.position.z;

            Vector3 clampedPosition = ClampCamera(mousePosition);

            mainCamera.transform.position = Vector3.SmoothDamp(
                mainCamera.transform.position, 
                clampedPosition, 
                ref _velocity, 
                smoothSpeed);
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
