using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D), typeof(Transform))]
    public class RoomSide : MonoBehaviour
    {
        [SerializeField] private RoomType roomType = RoomType.FrontSide;
        [SerializeField] [CanBeNull] private RoomSide leftRoomSide;
        [SerializeField] [CanBeNull] private RoomSide rightRoomSide;
        
        public RoomType RoomType => roomType;
        
        public (RoomSide, RoomSide) GetRoomSides()
        {
            return (leftRoomSide, rightRoomSide);
        }
    }
}