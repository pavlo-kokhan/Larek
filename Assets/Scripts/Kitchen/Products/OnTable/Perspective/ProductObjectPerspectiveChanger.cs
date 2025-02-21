using UnityEngine;

namespace Kitchen.Products.OnTable.Perspective
{
    [RequireComponent(typeof(Collider2D))]
    public class ProductObjectPerspectiveChanger : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _productSpriteRenderer;
        [SerializeField] private SpriteRenderer _shadowSpriteRenderer;
        [SerializeField] private Collider2D _productCollider;

        private Product _product;
        private TablePerspectiveCollider _currentPerspectiveCollider;
        private bool _isInitialized;
        
        public void Initialize(Product product)
        {
            if (_isInitialized) return;
            
            _product = product;
            
            _isInitialized = true;
        }

        private void Update()
        {
            UpdatePerspectiveCollider();
            UpdateScale();
            UpdateSprite();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<TablePerspectiveCollider>(out var perspectiveCollider))
            {
                _currentPerspectiveCollider = perspectiveCollider;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (_currentPerspectiveCollider is null) return;
            
            if (_currentPerspectiveCollider.Collider == other)
            {
                _currentPerspectiveCollider = null;
            }
        }
        
        private void UpdatePerspectiveCollider()
        {
            var colliders = Physics2D.OverlapPointAll(transform.position);

            TablePerspectiveCollider bestCollider = null;
    
            foreach (var col in colliders)
            {
                if (col.TryGetComponent<TablePerspectiveCollider>(out var perspectiveCollider))
                {
                    bestCollider = perspectiveCollider;
                }
            }

            if (bestCollider is not null && bestCollider != _currentPerspectiveCollider)
            {
                _currentPerspectiveCollider = bestCollider;
            }
        }

        private void UpdateSprite()
        {
            if (_currentPerspectiveCollider is null) return;
            
            var perspectiveType = _currentPerspectiveCollider.PerspectiveType;
            var sprites = _product.ProductPerspectiveSprites[perspectiveType];

            var productSprite = sprites.ProductSprite;
            var shadowSprite = sprites.ShadowSprite;
            
            if (productSprite is null || shadowSprite is null)
            {
                Debug.LogWarning($"{nameof(ProductObjectPerspectiveChanger)} Something wrong with perspective sprites of {_product.Type}");
                return;
            }
            
            _productSpriteRenderer.sprite = productSprite;
            _shadowSpriteRenderer.sprite = shadowSprite;
        }
        
        private void UpdateScale()
        {
            if (_currentPerspectiveCollider is null) return;

            var scaleRange = _currentPerspectiveCollider.ScaleRange;
            var colliderBounds = _currentPerspectiveCollider.Bounds;

            var maxScale = scaleRange.x;
            var minScale = scaleRange.y;

            var normalizedY = Mathf.InverseLerp(colliderBounds.min.y, colliderBounds.max.y, transform.position.y);
            var newScale = Mathf.Lerp(minScale, maxScale, 1 - normalizedY);

            transform.localScale = new Vector3(newScale, newScale, 1);
        }
    }
}