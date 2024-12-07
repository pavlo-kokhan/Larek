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
        
        [SerializeField] private ShelfAnimator animator;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && !animator.IsOpened)
            {
                animator.Open();
                ShelfOpened?.Invoke();
            }
        }
    }
}