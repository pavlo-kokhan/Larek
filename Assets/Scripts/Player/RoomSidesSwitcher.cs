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
        
        [SerializeField] private CameraMovement cameraMovement;
        [SerializeField] private BlinkEffectAnimator animator;
        [SerializeField] private float roomSwitchDelay = 0.2f;

        [SerializeField] [CanBeNull] private RoomSide leftRoomSide;
        [SerializeField] [CanBeNull] private RoomSide rightRoomSide;
        
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
            
            if (roomSide.TryGetComponent<Collider2D>(out var boundsCollider))
            {
                (leftRoomSide, rightRoomSide) = roomSide.GetRoomSides();
                RoomSideSwitched?.Invoke(boundsCollider, roomSide.transform.position, roomSide.RoomType);
            }
        }
    }
}