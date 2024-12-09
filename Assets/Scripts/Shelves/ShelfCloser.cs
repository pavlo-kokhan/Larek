using System;
using Shelves.Animations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shelves
{
    [RequireComponent(typeof(Collider2D))]
    public class ShelfCloser : MonoBehaviour, IPointerClickHandler
    {
        public event Action ShelfClosed;
        
        [SerializeField] private ShelfAnimator _animator;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right && _animator.IsOpened)
            {
                _animator.Close();
                ShelfClosed?.Invoke();
            }
        }
    }
}