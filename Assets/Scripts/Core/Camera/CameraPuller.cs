using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Camera
{
    public class CameraPuller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<Vector3> PointerEntered;
        public event Action PointerExited;

        private Vector3 _transformPosition;

        private void Start()
        {
            _transformPosition = transform.position;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEntered?.Invoke(_transformPosition);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExited?.Invoke();
        }
    }
}