using Core;
using Kitchen.Table.Perspective;
using UnityEngine;

namespace Kitchen.Products.ProductGameObject
{
    [RequireComponent(typeof(ProductObject), typeof(DynamicSpriteCollider))]
    public class ProductPerspective : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _productSpriteRenderer;
        [SerializeField] private SpriteRenderer _shadowSpriteRenderer;
        [SerializeField] private SpriteRenderer _juiceSpriteRenderer;
        
        private ProductObject _productObject;
        private DynamicSpriteCollider _dynamicSpriteCollider;
        private TablePerspectiveCollider _currentPerspectiveCollider;
        
        private Product Product => _productObject.Product;

        private void Awake()
        {
            _productObject = GetComponent<ProductObject>();
            _dynamicSpriteCollider = GetComponent<DynamicSpriteCollider>();
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
            var sprites = Product.ProductPerspectiveSprites[perspectiveType];

            _dynamicSpriteCollider.SetSprite(sprites.ProductSprite);
            _shadowSpriteRenderer.sprite = sprites.ShadowSprite;
            _juiceSpriteRenderer.sprite = sprites.JuiceSprite;
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