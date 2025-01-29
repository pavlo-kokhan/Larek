using UnityEngine;

namespace Core.CameraComponents
{
    public class BlockedCameraMovementState : ICameraMovementState
    {
        private readonly CameraMovement _cameraMovement;
        
        public BlockedCameraMovementState(CameraMovement cameraMovement)
        {
            _cameraMovement = cameraMovement;
        }
        
        public void EnterState()
        {
            Debug.Log($"Entered {nameof(BlockedCameraMovementState)}");
        }

        public void UpdateState()
        {
            //
        }

        public void ExitState()
        {
            Debug.Log($"Exited {nameof(BlockedCameraMovementState)}");
        }
    }
}