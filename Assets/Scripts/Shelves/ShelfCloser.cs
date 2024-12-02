using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shelves
{
    [RequireComponent(typeof(Collider2D))]
    public class ShelfCloser : MonoBehaviour, IPointerClickHandler
    {
        public event Action ShelfClosed;
        
        [SerializeField] private ShelfAnimator animator;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right && animator.IsOpened)
            {
                animator.Close();
                ShelfClosed?.Invoke();
            }
        }
    }
}