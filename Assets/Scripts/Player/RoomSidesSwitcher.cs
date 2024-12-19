using System;
using System.Collections;
using Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    public class RoomSidesSwitcher : MonoBehaviour
    {
        public event Action<Collider2D, Vector3, RoomType> RoomSideSwitched;
        
        [SerializeField] private BlinkEffectAnimator _animator;
        [SerializeField] private float _roomSwitchDelay = 0.2f;
        [SerializeField] private float _roomSwitchCooldown = 1f;

        [SerializeField] [CanBeNull] private RoomSide _leftRoomSide;
        [SerializeField] [CanBeNull] private RoomSide _rightRoomSide;

        private float _roomSwitchTimer;

        private void Start()
        {
            _roomSwitchTimer = _roomSwitchCooldown;
        }

        private void Update()
        {
            _roomSwitchTimer += Time.deltaTime;
        }

        public void OnLeftSideSwitch()
        {
            if (_leftRoomSide != null && _roomSwitchTimer >= _roomSwitchCooldown)
            {
                SwitchRoomSide(_leftRoomSide);
            }
        }
        
        public void OnRightSideSwitch()
        {
            if (_rightRoomSide != null && _roomSwitchTimer >= _roomSwitchCooldown)
            {
                SwitchRoomSide(_rightRoomSide);
            }
        }

        private void SwitchRoomSide(RoomSide roomSide)
        {
            _animator.SetBlink();
            StartCoroutine(SwitchRoomSideCoroutine(roomSide));
            _roomSwitchTimer = 0f;
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