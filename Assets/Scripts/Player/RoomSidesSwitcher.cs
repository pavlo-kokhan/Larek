using System;
using System.Collections;
using Core;
using Core.Camera;
using JetBrains.Annotations;
using Player.Effects;
using UnityEngine;

namespace Player
{
    public class RoomSidesSwitcher : MonoBehaviour
    {
        public event Action<Collider2D, Vector3, RoomType> RoomSideSwitched;
        
        [SerializeField] private BlinkEffectAnimator _animator;
        [SerializeField] private float _roomSwitchDelay = 0.2f;

        [SerializeField] [CanBeNull] private RoomSide _leftRoomSide;
        [SerializeField] [CanBeNull] private RoomSide _rightRoomSide;
        
        public void OnLeftSideSwitch()
        {
            if (_leftRoomSide != null)
            {
                SwitchRoomSide(_leftRoomSide);
            }
        }
        
        public void OnRightSideSwitch()
        {
            if (_rightRoomSide != null)
            {
                SwitchRoomSide(_rightRoomSide);
            }
        }

        private void SwitchRoomSide(RoomSide roomSide)
        {
            _animator.SetBlink();
            StartCoroutine(SwitchRoomSideCoroutine(roomSide));
        }

        private IEnumerator SwitchRoomSideCoroutine(RoomSide roomSide)
        {
            yield return new WaitForSeconds(_roomSwitchDelay);
            
            if (roomSide.TryGetComponent<Collider2D>(out var boundsCollider))
            {
                (_leftRoomSide, _rightRoomSide) = roomSide.GetRoomSides();
                RoomSideSwitched?.Invoke(boundsCollider, roomSide.transform.position, roomSide.RoomType);
            }
        }
    }
}