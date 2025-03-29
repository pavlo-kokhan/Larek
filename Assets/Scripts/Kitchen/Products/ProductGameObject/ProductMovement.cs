using System;
using Kitchen.Table;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.Products.ProductGameObject
{
    [RequireComponent(typeof(ProductObject), typeof(Rigidbody2D))]
    public class ProductMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private SpriteRenderer _productSpriteRenderer;
        
        [Header("Movement config")]
        [SerializeField] private float _movementSmoothTime = 0.05f;
        [SerializeField] private float _maxClickTime = 0.1f;

        private float _dragTimer;
        private Vector2 _movementVelocity;
        private Vector3 _targetPosition;
        private bool _isMovable;
        public bool IsMovable => _isMovable;
        
        private ProductObject _productObject;
        private Rigidbody2D _rigidbody;
        
        private ProductHolder _productHolder;
        private ProductSpawnArea _productSpawnArea;
        
        private Product Product => _productObject.Product;
        
        [Inject]
        private void Construct(ProductHolder productHolder)
        {
            _productHolder = productHolder;
        }
        
        private void Awake()
        {
            _productObject = GetComponent<ProductObject>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            if (!_isMovable) return;

            UpdateTargetPosition();
            
            _rigidbody.MovePosition(_targetPosition);
            
            _dragTimer += Time.fixedDeltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ProductSpawnArea>(out var spawnArea))
            {
                Debug.Log("Entering product spawn area collider");

                if (_productSpawnArea is not null) return;
                
                _productSpawnArea = spawnArea;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftMouseDown();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftMouseUp();
            }
        }

        private void OnLeftMouseDown()
        {
            _dragTimer = 0f;
            
            _productSpriteRenderer.sortingOrder = ++_productSpawnArea.MaxSortingLayer;
            
            _isMovable = true;
        }

        private void OnLeftMouseUp()
        {
            _isMovable = false;

            if (_dragTimer >= _maxClickTime) return;
            
            if (!_productHolder.TryTakeNewProduct(Product)) return;
            
            Destroy(gameObject);
        }

        private void UpdateTargetPosition()
        {
            if (Camera.main is null)
            {
                Debug.LogWarning($"{nameof(ProductObject)} No Main camera. Can not move product object");
                return;
            }
            
            var cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var oldPosition = transform.position;
            
            _targetPosition = new Vector3(
                Mathf.SmoothDamp(transform.position.x, cursorPosition.x, 
                    ref _movementVelocity.x, _movementSmoothTime), 
                Mathf.SmoothDamp(transform.position.y, cursorPosition.y, 
                    ref _movementVelocity.y, _movementSmoothTime),
                oldPosition.z);
            
            ClampTargetPosition();
        }

        private void ClampTargetPosition()
        {
            var closestPoint = Physics2D.ClosestPoint(_targetPosition, _productSpawnArea.Collider);

            _targetPosition = new Vector3(closestPoint.x, closestPoint.y, _targetPosition.z);
        }
    }
}