using Player;
using UnityEngine;

namespace Core.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera mainCamera;
        [SerializeField] private Collider2D backgroundCollider;
        [SerializeField] private float edgeThreshold = 200f;
        [SerializeField] private float smoothSpeed = 0.7f;
        
        [SerializeField] private RoomSidesSwitcher roomSidesSwitcher;
        
        private Vector3 _centerPosition;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _targetPosition;
        private bool _isActive;

        private void OnEnable()
        {
            roomSidesSwitcher.RoomSideSwitched += ChangeCameraRoomSide;
        }

        private void OnDisable()
        {
            roomSidesSwitcher.RoomSideSwitched -= ChangeCameraRoomSide;
        }

        private void Start()
        {
            _centerPosition = mainCamera.transform.position;
            _targetPosition = _centerPosition;
            _isActive = true;
        }

        private void Update()
        {
            if (!_isActive) return;
            
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

        private bool IsMouseNearEdge(Vector3 mousePosition)
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            bool isNearHorizontalEdge = mousePosition.x < edgeThreshold 
                                        || mousePosition.x > screenWidth - edgeThreshold;
            
            bool isNearVerticalEdge = mousePosition.y < edgeThreshold 
                                      || mousePosition.y > screenHeight - edgeThreshold;

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
        
        private void ChangeCameraRoomSide(Collider2D newCollider, Vector3 newCenterPosition, RoomType newRoomType)
        {
            var oldCameraPosition = mainCamera.transform.position;
            
            backgroundCollider = newCollider;
            _centerPosition = new Vector3(newCenterPosition.x, newCenterPosition.y, oldCameraPosition.z);
            _targetPosition = _centerPosition;

            _velocity = Vector3.zero;

            mainCamera.transform.position = _centerPosition;

            _isActive = newRoomType is RoomType.FrontSide;
        }
    }
}
