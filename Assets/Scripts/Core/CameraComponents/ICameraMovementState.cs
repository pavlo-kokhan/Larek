namespace Core.CameraComponents
{
    public interface ICameraMovementState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
    }
}