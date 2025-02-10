using UnityEngine;

namespace Core.CameraComponents
{
    public class DefaultCameraMovementState : ICameraMovementState
    {
        private readonly CameraMovement _cameraMovement;
        private readonly DefaultCameraMovementConfig _defaultCameraMovementConfig;

        private Vector3 _velocity;
        private Vector3 _targetPosition;

        public DefaultCameraMovementState(CameraMovement cameraMovement)
        {
            _cameraMovement = cameraMovement;
            _defaultCameraMovementConfig = _cameraMovement.DefaultCameraMovementConfig;
        }
        
        public void EnterState()
        {
            Debug.Log($"Entered {nameof(DefaultCameraMovementState)}");
        }

        public void UpdateState()
        {
            var mainCamera = _cameraMovement.MainCamera;
            var centerPosition = _cameraMovement.CenterPosition;
            var mousePosition = Input.mousePosition;
            
            if (IsMouseNearEdge(mousePosition))
            {
                var worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
                worldMousePosition.z = centerPosition.z;
            
                _targetPosition = ClampCamera(worldMousePosition);
            }
            else
            {
                _targetPosition = centerPosition;
            }
            
            mainCamera.transform.position = Vector3.SmoothDamp(
                mainCamera.transform.position, 
                _targetPosition, 
                ref _velocity, 
                _defaultCameraMovementConfig.SmoothSpeed);
        }
        
        private bool IsMouseNearEdge(Vector3 mousePosition)
        {
            var horizontalEdgeThreshold = _defaultCameraMovementConfig.HorizontalEdgeThreshold;
            var verticalEdgeThreshold = _defaultCameraMovementConfig.VerticalEdgeThreshold;
            
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            var isNearHorizontalEdge = mousePosition.x < horizontalEdgeThreshold 
                                        || mousePosition.x > screenWidth - horizontalEdgeThreshold;
            
            var isNearVerticalEdge = mousePosition.y < verticalEdgeThreshold 
                                      || mousePosition.y > screenHeight - verticalEdgeThreshold;

            return isNearHorizontalEdge || isNearVerticalEdge;
        }
        
        private Vector3 ClampCamera(Vector3 targetPosition)
        {
            var mainCamera = _cameraMovement.MainCamera;
            var backgroundCollider = _cameraMovement.BackgroundCollider;
            
            var cameraHalfHeight = mainCamera.orthographicSize;
            var cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;

            var bounds = backgroundCollider.bounds;

            var minX = bounds.min.x + cameraHalfWidth;
            var maxX = bounds.max.x - cameraHalfWidth;
            var minY = bounds.min.y + cameraHalfHeight;
            var maxY = bounds.max.y - cameraHalfHeight;

            return new Vector3(
                Mathf.Clamp(targetPosition.x, minX, maxX),
                Mathf.Clamp(targetPosition.y, minY, maxY),
                targetPosition.z
            );
        }

        public void ExitState()
        {
            Debug.Log($"Exited {nameof(DefaultCameraMovementState)}");
        }
    }
}