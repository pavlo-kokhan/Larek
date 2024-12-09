using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D), typeof(Transform))]
    public class RoomSide : MonoBehaviour
    {
        [SerializeField] private RoomType _roomType = RoomType.FrontSide;
        [SerializeField] [CanBeNull] private RoomSide _leftRoomSide;
        [SerializeField] [CanBeNull] private RoomSide _rightRoomSide;
        
        public RoomType RoomType => _roomType;
        
        public (RoomSide, RoomSide) GetRoomSides()
        {
            return (_leftRoomSide, _rightRoomSide);
        }
    }
}