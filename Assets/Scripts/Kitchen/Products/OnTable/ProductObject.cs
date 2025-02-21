using JetBrains.Annotations;
using Kitchen.Products.OnHotplate;
using Kitchen.Products.OnTable.Perspective;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.Products.OnTable
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class ProductObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] [CanBeNull] private ProductObjectPerspectiveChanger _perspectiveChanger;
        [SerializeField] [CanBeNull] private CookableProductObject _cookableProductObject;
        
        [SerializeField] private Collider2D _productCollider;
        [SerializeField] private Rigidbody2D _productRigidbody;
        
        [SerializeField] private float _movementSmoothTime = 0.05f;
        [SerializeField] private Vector2 _movementVelocity = Vector2.zero;
        [SerializeField] private float _minMouseThreshold = 0.1f;
        
        private Product _product;
        private Vector2 _startDragMousePosition;
        private Vector3 _lastOnTablePosition;
        private ProductSpawnArea _productSpawnArea;
        private Vector3 _targetPosition;
        private bool _isMovable;
        private bool _isInitialized;
        
        private ProductHolder _productHolder;
        
        public Product Product => _product;

        [Inject]
        public void Construct(ProductHolder productHolder)
        {
            _productHolder = productHolder;
        }

        public void Initialize(Product product)
        {
            if (_isInitialized) return;
            
            _product = product;

            _perspectiveChanger?.Initialize(_product);
            _cookableProductObject?.Initialize(_product);

            _isInitialized = true;
        }

        private void FixedUpdate()
        {
            if (_isMovable == false) return;

            UpdateTargetPosition();
            _productRigidbody.MovePosition(_targetPosition);
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
            _startDragMousePosition = Input.mousePosition;
            _isMovable = true;
        }

        private void OnLeftMouseUp()
        {
            _isMovable = false;

            if (Vector3.Distance(_startDragMousePosition, Input.mousePosition) > _minMouseThreshold) return;
            
            if (_productHolder.TryTakeNewProduct(_product) == false) return;
            
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