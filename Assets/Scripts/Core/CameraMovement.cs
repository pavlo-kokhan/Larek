using UnityEngine;

namespace Core
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Collider2D backgroundCollider;
        [SerializeField] private float edgeThreshold = 200f;
        [SerializeField] private float smoothSpeed = 0.7f;
        
        private Vector3 _centerPosition;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _targetPosition;
        private CameraPuller[] _cameraPullers;

        private void OnEnable()
        {
            _cameraPullers = FindObjectsOfType<CameraPuller>();
            Debug.Log(_cameraPullers.Length);
            foreach (var cameraPuller in _cameraPullers)
            {
                // cameraPuller.PointerEntered += OnPullCameraToObject;
                // cameraPuller.PointerExited += OnStopPullingToObject;
            }
        }

        private void OnDisable()
        {
            foreach (var cameraPuller in _cameraPullers)
            {
                // cameraPuller.PointerEntered -= OnPullCameraToObject;
                // cameraPuller.PointerExited -= OnStopPullingToObject;
            }
        }

        private void Start()
        {
            Cursor.visible = true;
            _centerPosition = mainCamera.transform.position;
            _targetPosition = _centerPosition;
        }

        private void Update()
        {
            var mousePosition = Input.mousePosition;
            
            if (IsMouseNearEdge(mousePosition))
            {
                Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
                worldMousePosition.z = _centerPosition.z;
            
                _targetPosition = ClampCamera(worldMousePosition);
            }
            else
            {
                _targetPosition = _centerPosition;
            }
            
            mainCamera.transform.position = Vector3.SmoothDamp(
                mainCamera.transform.position, 
                _targetPosition, 
                ref _velocity, 
                smoothSpeed);
        }

        private void OnPullCameraToObject(Vector3 objectPosition)
        {
            _targetPosition = ClampCamera(objectPosition);
            _targetPosition.z = _centerPosition.z;
        }

        private void OnStopPullingToObject()
        {
            _targetPosition = _centerPosition;
        }
        
        private bool IsMouseNearEdge(Vector3 mousePosition)
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            bool isNearHorizontalEdge = mousePosition.x < edgeThreshold || mousePosition.x > screenWidth - edgeThreshold;
            bool isNearVerticalEdge = mousePosition.y < edgeThreshold || mousePosition.y > screenHeight - edgeThreshold;

            return isNearHorizontalEdge || isNearVerticalEdge;
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
