using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D), typeof(Transform))]
    public class RoomSide : MonoBehaviour
    {
        [SerializeField] [CanBeNull] private RoomSide rightRoomSide;
        [SerializeField] [CanBeNull] private RoomSide leftRoomSide;
        
        public (RoomSide, RoomSide) GetRoomSides()
        {
            return (rightRoomSide, leftRoomSide);
        }
    }
}