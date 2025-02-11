using Core.RoomSidesSwitcherComponents;
using UnityEngine;

namespace Core.CameraComponents
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Collider2D _backgroundCollider;
        [SerializeField] private DefaultCameraMovementConfig _defaultCameraMovementConfig;
        [SerializeField] private HorizontalCameraMovementConfig _horizontalCameraMovementConfig;
        [SerializeField] private RoomSidesSwitcher _roomSidesSwitcher;
        
        public Camera MainCamera => _mainCamera;
        public Collider2D BackgroundCollider => _backgroundCollider;
        public DefaultCameraMovementConfig DefaultCameraMovementConfig => _defaultCameraMovementConfig;
        public HorizontalCameraMovementConfig HorizontalCameraMovementConfig => _horizontalCameraMovementConfig;
        public Vector3 CenterPosition { get; private set; }

        private ICameraMovementState _currentState;

        private void OnEnable()
        {
            _roomSidesSwitcher.RoomSideSwitched += OnRoomSideSwitched;
        }

        private void OnDisable()
        {
            _roomSidesSwitcher.RoomSideSwitched -= OnRoomSideSwitched;
        }

        private void Start()
        {
            CenterPosition = transform.position;
            SetState(new DefaultCameraMovementState(this));
        }

        private void Update()
        {
            _currentState?.UpdateState();
        }

        private void OnRoomSideSwitched(Collider2D newCollider, Vector3 newCenterPosition, RoomType newRoomType)
        {
            var oldPosition = _mainCamera.transform.position;
            
            _backgroundCollider = newCollider;
            CenterPosition = new Vector3(newCenterPosition.x, newCenterPosition.y, oldPosition.z);
            _mainCamera.transform.position = CenterPosition;
            
            var newState = GetMovementState(newRoomType);
            SetState(newState);
        }

        private ICameraMovementState GetMovementState(RoomType roomType)
        {
            switch (roomType)
            {
                case RoomType.FrontSide:
                    return new DefaultCameraMovementState(this);
                case RoomType.Kitchen:
                    return new HorizontalCameraMovementState(this);
                default:
                    return new BlockedCameraMovementState(this);
            }
        }
        
        private void SetState(ICameraMovementState state)
        {
            _currentState?.ExitState();
            _currentState = state;
            _currentState?.EnterState();
        }
    }
}
