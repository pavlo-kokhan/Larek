using UnityEngine;

namespace Core.CameraComponents
{
    public class HorizontalCameraMovementState : ICameraMovementState
    {
        private readonly CameraMovement _cameraMovement;
        private readonly HorizontalCameraMovementConfig _cameraMovementConfig;
        
        private Vector3 _velocity;
        private Vector3 _targetPosition;
        
        public HorizontalCameraMovementState(CameraMovement cameraMovement)
        {
            _cameraMovement = cameraMovement;
            _cameraMovementConfig = _cameraMovement.HorizontalCameraMovementConfig;
        }
        
        public void EnterState()
        {
            Debug.Log($"Entered {nameof(HorizontalCameraMovementState)}");
        }

        public void UpdateState()
        {
            var mainCamera = _cameraMovement.MainCamera;
            var centerPosition = _cameraMovement.CenterPosition;
            var mousePosition = Input.mousePosition;
            
            if (IsMouseNearEdge(mousePosition))
            {
                var worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
                worldMousePosition.y = centerPosition.y;
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
                _cameraMovementConfig.SmoothSpeed);
        }
        
        private bool IsMouseNearEdge(Vector3 mousePosition)
        {
            var edgeThreshold = _cameraMovementConfig.EdgeThreshold;
            
            float screenWidth = Screen.width;

            var isNearHorizontalEdge = mousePosition.x < edgeThreshold 
                                       || mousePosition.x > screenWidth - edgeThreshold;

            return isNearHorizontalEdge;
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
            Debug.Log($"Exited {nameof(HorizontalCameraMovementState)}");
        }
    }
}