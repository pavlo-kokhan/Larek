using Kitchen.Products.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.Products.OnTable
{
    [RequireComponent(typeof(Collider2D))]
    public class ProductSpawnArea : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Collider2D _collider;
        
        private ProductObjectsFactory _factory;
        private ProductHolder _productHolder;
        private IAcceptProductCondition _acceptCondition;
        //
        private ProductConfigsStorage _productConfigsStorage;
        
        public Collider2D Collider => _collider;

        [Inject]
        public void Construct(ProductObjectsFactory factory, ProductHolder productHolder, ProductConfigsStorage productConfigsStorage)
        {
            _factory = factory;
            _productHolder = productHolder;
            
            //
            _productConfigsStorage = productConfigsStorage;
            var product = new Product(_productConfigsStorage.GetConfig(ProductType.Patty, ProductCookingStage.Done,
                        ProductChoppingStage.Unchopped), ProductLocation.Refrigerator);
            _factory.Create(product, new Vector3(0, 0, 0));
            //
            
            _acceptCondition = new ProductSpawnAreaAcceptCondition();
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
            
            if (_productHolder.TryReturnProduct(_acceptCondition, out var product) == false) return;
            
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            var productObject = _factory.Create(product, new Vector3(position.x, position.y, 0f));

            if (productObject is null)
            {
                Debug.LogWarning($"{nameof(ProductSpawnArea)} {nameof(productObject)} is null!");
            }
        }
    }
}