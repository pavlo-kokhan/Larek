using System;
using Shelves.Animations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shelves
{
    [RequireComponent(typeof(Collider2D))]
    public class ShelfOpener : MonoBehaviour, IPointerClickHandler
    {
        public event Action ShelfOpened;
        
        [SerializeField] private ShelfAnimator _animator;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && !_animator.IsOpened)
            {
                _animator.Open();
                ShelfOpened?.Invoke();
            }
        }
    }
}