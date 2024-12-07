using System;
using Gates.Animations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gates
{
    [RequireComponent(typeof(Collider2D))]
    public class GatesSwitcher : MonoBehaviour, IPointerClickHandler
    {
        public event Action<bool> GatesSwitched;
        
        [SerializeField] private GatesAnimator animator;
        [SerializeField] private float cooldown = 3f;
        
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
                ToggleGates();
                _timer = 0f;
            }
        }
        
        private void ToggleGates()
        {
            _isOpened = !_isOpened;
            animator.SetOpened(_isOpened);
            GatesSwitched?.Invoke(_isOpened);
        }
    }
}