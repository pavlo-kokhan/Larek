using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shelves
{
    public class ShelfOpener : MonoBehaviour, IPointerClickHandler
    {
        public event Action<bool> ShelfSwitched;
        
        [SerializeField] private ShelfAnimator animator;
        [SerializeField] private float cooldown;

        private float _timer;
        private bool _isOpened;

        private void Start()
        {
            _timer = cooldown;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (_timer > cooldown)
            {
                ToggleShelf();
                _timer = 0;
            }
        }

        private void ToggleShelf()
        {
            _isOpened = !_isOpened;
            animator.SetOpened(_isOpened);
            ShelfSwitched?.Invoke(_isOpened);
        }
    }
}