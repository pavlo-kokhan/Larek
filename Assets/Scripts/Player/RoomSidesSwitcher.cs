using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class RoomSidesSwitcher : MonoBehaviour
    {
        public event Action<Collider2D, Vector3, RoomType> RoomSideSwitched;
        
        [SerializeField] private BlinkEffectAnimator _animator;
        [SerializeField] private float _roomSwitchDelay = 0.2f;
        [SerializeField] private float _roomSwitchCooldown = 1f;

        [Header("Buttons")] 
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _topButton;
        [SerializeField] private Button _bottomButton;
        
        [Header("Room Sides")]
        [SerializeField] [CanBeNull] private RoomSide _leftRoomSide;
        [SerializeField] [CanBeNull] private RoomSide _rightRoomSide;
        [SerializeField] [CanBeNull] private RoomSide _topRoomSide;
        [SerializeField] [CanBeNull] private RoomSide _bottomRoomSide;
        [SerializeField] [CanBeNull] private RoomSide _undergroundRoomSide;

        private float _roomSwitchTimer;

        private void Start()
        {
            _roomSwitchTimer = _roomSwitchCooldown;
            SetButtonsActiveState();
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
        
        public void OnTopSideSwitch()
        {
            if (_topRoomSide != null && _roomSwitchTimer >= _roomSwitchCooldown)
            {
                SwitchRoomSide(_topRoomSide);
            }
        }
        
        public void OnBottomSideSwitch()
        {
            if (_bottomRoomSide != null && _roomSwitchTimer >= _roomSwitchCooldown)
            {
                SwitchRoomSide(_bottomRoomSide);
            }
        }
        
        public void OnUndergroundSideSwitch()
        {
            if (_undergroundRoomSide != null && _roomSwitchTimer >= _roomSwitchCooldown)
            {
                SwitchRoomSide(_undergroundRoomSide);
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
                (_leftRoomSide, _rightRoomSide, _topRoomSide, _bottomRoomSide) = roomSide.GetRoomSides();
                SetButtonsActiveState();
                var center = roomSide.CenterForCamera.position;
                RoomSideSwitched?.Invoke(boundsCollider, center, roomSide.RoomType);
            }
        }

        private void SetButtonsActiveState()
        {
            _leftButton.gameObject.SetActive(_leftRoomSide != null);
            _rightButton.gameObject.SetActive(_rightRoomSide != null);
            _topButton.gameObject.SetActive(_topRoomSide != null);
            _bottomButton.gameObject.SetActive(_bottomRoomSide != null);
        }
    }
}