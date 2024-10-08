using System;
using UnityEngine;

namespace Gates
{
    public class GatesSwitcher : MonoBehaviour
    {
        public event Action<bool> OnGatesSwitchedSound;
        
        [SerializeField] private GatesAnimator animator;
        
        private SpriteRenderer _spriteRenderer;
        private PolygonCollider2D _collider;
        private bool _isInteracting;
        private bool _isOpened;
        private float _timer = float.MaxValue;
        private readonly float _cooldown = 5f;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<PolygonCollider2D>();
        }

        private void Update()
        {
            if (IsMouseOver())
            {
                _spriteRenderer.color = new Color(1f, 0.9f, 0.9f); // todo
                
                HandleInput();
                
                _timer += Time.deltaTime;
            }
            else
            {
                _spriteRenderer.color = Color.white;
            }
        }
        
        private bool IsMouseOver()
        {
            if (Camera.main != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
                return _collider.OverlapPoint(mousePosition);
            }

            return false; // todo
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0) && _timer > _cooldown)
            {
                ToggleGates();
                _timer = 0f;
            }
        }

        private void ToggleGates()
        {
            _isOpened = !_isOpened;
            animator.SetOpened(_isOpened);
            OnGatesSwitchedSound?.Invoke(_isOpened);
        }
    }
}