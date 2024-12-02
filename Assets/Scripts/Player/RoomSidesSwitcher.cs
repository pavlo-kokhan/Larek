using System;
using System.Collections;
using Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    public class RoomSidesSwitcher : MonoBehaviour
    {
        public event Action<Collider2D, Vector3> RoomSideSwitched;
        
        [SerializeField] private CameraMovement cameraMovement;
        [SerializeField] private BlinkEffectAnimator animator;
        [SerializeField] private float roomSwitchDelay = 0.2f;

        [SerializeField] [CanBeNull] private RoomSide rightRoomSide;
        [SerializeField] [CanBeNull] private RoomSide leftRoomSide;
        
        public void OnRightSideSwitch()
        {
            if (rightRoomSide != null)
            {
                SwitchRoomSide(rightRoomSide);
            }
        }

        public void OnLeftSideSwitch()
        {
            if (leftRoomSide != null)
            {
                SwitchRoomSide(leftRoomSide);
            }
        }

        private void SwitchRoomSide(RoomSide roomSide)
        {
            animator.SetBlink();
            StartCoroutine(SwitchRoomSideCoroutine(roomSide));
        }

        private IEnumerator SwitchRoomSideCoroutine(RoomSide roomSide)
        {
            yield return new WaitForSeconds(roomSwitchDelay);
            
            if (roomSide.TryGetComponent<Collider2D>(out var boundsCollider) &&
                roomSide.TryGetComponent<Transform>(out var roomTransform))
            {
                (rightRoomSide, leftRoomSide) = roomSide.GetRoomSides();
                RoomSideSwitched?.Invoke(boundsCollider, roomTransform.position);
            }
        }
    }
}