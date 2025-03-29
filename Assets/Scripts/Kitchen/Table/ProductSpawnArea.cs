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
        private void Construct(ProductObjectsFactory factory, ProductHolder productHolder, ProductConfigsStorage productConfigsStorage)
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
                SpawnProduct();
            }
        }

        public void SpawnProduct()
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

        // completely unsafe code
        private void Test()
        {
            var product = new Product(_productConfigsStorage.GetConfig(ProductType.Patty, ProductCookingStage.Raw,
                ProductChoppingStage.Unchopped, ProductShapeType.None, ProductAssemblyType.None));
            var test = _factory.Create(product, Vector3.zero);
            test.transform.localPosition = Vector3.zero;
            
            var product2 = new Product(_productConfigsStorage.GetConfig(ProductType.Patty, ProductCookingStage.Medium,
                ProductChoppingStage.Unchopped, ProductShapeType.None, ProductAssemblyType.None));
            var test2 = _factory.Create(product2, Vector3.zero);
            test2.transform.localPosition = Vector3.zero;
            
            var product3 = new Product(_productConfigsStorage.GetConfig(ProductType.Patty, ProductCookingStage.Done,
                ProductChoppingStage.Unchopped, ProductShapeType.None, ProductAssemblyType.None));
            var test3 = _factory.Create(product3, Vector3.zero);
            test3.transform.localPosition = Vector3.zero;
            
            var product4 = new Product(_productConfigsStorage.GetConfig(ProductType.Patty, ProductCookingStage.Burned,
                ProductChoppingStage.Unchopped, ProductShapeType.None, ProductAssemblyType.None));
            var test4 = _factory.Create(product4, Vector3.zero);
            test4.transform.localPosition = Vector3.zero;
            
            var product5 = new Product(_productConfigsStorage.GetConfig(ProductType.Bun, ProductCookingStage.Done,
                ProductChoppingStage.Unchopped, ProductShapeType.None, ProductAssemblyType.None));
            var test5 = _factory.Create(product5, Vector3.zero);
            test5.transform.localPosition = Vector3.zero;
            
            var product6 = new Product(_productConfigsStorage.GetConfig(ProductType.BunBase, ProductCookingStage.Done,
                ProductChoppingStage.Unchopped, ProductShapeType.None, ProductAssemblyType.Initializer));
            var test6 = _factory.Create(product6, Vector3.zero);
            test6.transform.localPosition = Vector3.zero;
            
            var product7 = new Product(_productConfigsStorage.GetConfig(ProductType.BunTop, ProductCookingStage.Done,
                ProductChoppingStage.Unchopped, ProductShapeType.None, ProductAssemblyType.Finalizer));
            var test7 = _factory.Create(product7, Vector3.zero);
            test7.transform.localPosition = Vector3.zero;
        }
    }
}