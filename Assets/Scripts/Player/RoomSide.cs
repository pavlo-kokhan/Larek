using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D), typeof(Transform))]
    public class RoomSide : MonoBehaviour
    {
        [SerializeField] private RoomType _roomType = RoomType.FrontSide;
        [SerializeField] private Transform _centerForCamera;
        
        [SerializeField] [CanBeNull] private RoomSide _leftRoomSide;
        [SerializeField] [CanBeNull] private RoomSide _rightRoomSide;
        [SerializeField] [CanBeNull] private RoomSide _topRoomSide;
        [SerializeField] [CanBeNull] private RoomSide _bottomRoomSide;
        
        public RoomType RoomType => _roomType;
        public Transform CenterForCamera => _centerForCamera;
        
        public (RoomSide, RoomSide, RoomSide, RoomSide) GetRoomSides()
        {
            return (_leftRoomSide, _rightRoomSide, _topRoomSide, _bottomRoomSide);
        }
    }
}