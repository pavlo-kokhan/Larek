using System;
using Kitchen.Products;
using Kitchen.Products.Enums;
using Kitchen.Products.ProductGameObject;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.Table
{
    [RequireComponent(typeof(Collider2D))]
    public class ProductSpawnArea : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Collider2D _collider;
        public Collider2D Collider => _collider;
        
        private ProductObjectsFactory _factory;
        private ProductHolder _productHolder;
        private IAcceptProductCondition _acceptCondition;
        //
        private ProductConfigsStorage _productConfigsStorage;
        private int _maxSortingLayer = 1;

        public int MaxSortingLayer
        {
            get => _maxSortingLayer;
            set
            {
                if (value <= _maxSortingLayer || value > _maxSortingLayer + 1)
                {
                    throw new InvalidOperationException($"{nameof(MaxSortingLayer)} can be only increased by 1");
                }
                
                _maxSortingLayer = value;
            }
        }

        [Inject]
        public void Construct(ProductObjectsFactory factory, ProductHolder productHolder, ProductConfigsStorage productConfigsStorage)
        {
            _factory = factory;
            _productHolder = productHolder;
            _productConfigsStorage = productConfigsStorage;
            _acceptCondition = new ProductSpawnAreaAcceptCondition();
            
            Test();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftButtonClicked();
            }
        }

        private void OnLeftButtonClicked()
        {
            if (Camera.main is null)
            {
                Debug.LogWarning($"{nameof(ProductSpawnArea)} Main camera is missing!");
                return;
            }
            
            if (!_productHolder.TryReturnProduct(_acceptCondition, out var product)) return;
            
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            var productObject = _factory.Create(product, new Vector3(position.x, position.y, 0f));
            
            if (productObject is null)
            {
                Debug.LogWarning($"{nameof(productObject)} is null!");
                return;
            }
            
            var spriteRenderer = productObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = ++_maxSortingLayer;
        }

        private void Test()
        {
            var product = new Product(_productConfigsStorage.GetConfig(ProductType.Patty, ProductCookingStage.Raw,
                ProductChoppingStage.Unchopped));
            var test = _factory.Create(product, Vector3.zero);
            test.transform.localPosition = new Vector3(0, 0, 0);
            
            var product2 = new Product(_productConfigsStorage.GetConfig(ProductType.Patty, ProductCookingStage.Medium,
                ProductChoppingStage.Unchopped));
            var test2 = _factory.Create(product2, Vector3.zero);
            test2.transform.localPosition = new Vector3(0, 0, 5);
            
            var product3 = new Product(_productConfigsStorage.GetConfig(ProductType.Patty, ProductCookingStage.Done,
                ProductChoppingStage.Unchopped));
            var test3 = _factory.Create(product3, Vector3.zero);
            test3.transform.localPosition = new Vector3(0, 0, 10);
            
            var product4 = new Product(_productConfigsStorage.GetConfig(ProductType.Patty, ProductCookingStage.Burned,
                ProductChoppingStage.Unchopped));
            var test4 = _factory.Create(product4, Vector3.zero);
            test4.transform.localPosition = new Vector3(0, 0, 15);
        }
    }
}