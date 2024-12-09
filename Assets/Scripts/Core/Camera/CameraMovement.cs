using Player;
using UnityEngine;

namespace Core.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _mainCamera;
        [SerializeField] private Collider2D _backgroundCollider;
        [SerializeField] private float _edgeThreshold = 100f;
        [SerializeField] private float _smoothSpeed = 0.5f;
        
        [SerializeField] private RoomSidesSwitcher _roomSidesSwitcher;
        
        private Vector3 _centerPosition;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _targetPosition;
        private bool _isActive;

        private void OnEnable()
        {
            _roomSidesSwitcher.RoomSideSwitched += ChangeCameraRoomSide;
        }

        private void OnDisable()
        {
            _roomSidesSwitcher.RoomSideSwitched -= ChangeCameraRoomSide;
        }

        private void Start()
        {
            _centerPosition = _mainCamera.transform.position;
            _targetPosition = _centerPosition;
            _isActive = true;
        }

        private void Update()
        {
            if (!_isActive) return;
            
            var mousePosition = Input.mousePosition;
            
            if (IsMouseNearEdge(mousePosition))
            {
                Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
                worldMousePosition.z = _centerPosition.z;
            
                _targetPosition = ClampCamera(worldMousePosition);
            }
            else
            {
                _targetPosition = _centerPosition;
            }
            
            _mainCamera.transform.position = Vector3.SmoothDamp(
                _mainCamera.transform.position, 
                _targetPosition, 
                ref _velocity, 
                _smoothSpeed);
        }

        private bool IsMouseNearEdge(Vector3 mousePosition)
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            bool isNearHorizontalEdge = mousePosition.x < _edgeThreshold 
                                        || mousePosition.x > screenWidth - _edgeThreshold;
            
            bool isNearVerticalEdge = mousePosition.y < _edgeThreshold 
                                      || mousePosition.y > screenHeight - _edgeThreshold;

            return isNearHorizontalEdge || isNearVerticalEdge;
        }
        
        private Vector3 ClampCamera(Vector3 targetPosition)
        {
            float cameraHalfHeight = _mainCamera.orthographicSize;
            float cameraHalfWidth = cameraHalfHeight * _mainCamera.aspect;

            Bounds bounds = _backgroundCollider.bounds;

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
            var oldCameraPosition = _mainCamera.transform.position;
            
            _backgroundCollider = newCollider;
            _centerPosition = new Vector3(newCenterPosition.x, newCenterPosition.y, oldCameraPosition.z);
            _targetPosition = _centerPosition;

            _velocity = Vector3.zero;

            _mainCamera.transform.position = _centerPosition;

            _isActive = newRoomType is RoomType.FrontSide;
        }
    }
}
