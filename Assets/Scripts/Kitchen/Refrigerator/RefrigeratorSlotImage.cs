using System.Collections.Generic;
using Kitchen.Products;
using Kitchen.Products.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kitchen.Refrigerator
{
    public class RefrigeratorSlotImage : MonoBehaviour
    {
        [SerializeField] private RefrigeratorSlot _refrigeratorSlot;
        [SerializeField] private Image _productImage;
        
        private readonly Dictionary<int, Sprite> _sprites = new();

        [Inject]
        public void Construct(ProductConfigsStorage productConfigsStorage)
        {
            var productType = _refrigeratorSlot.Type;
            var productConfig = productConfigsStorage.GetConfigOfProductType(productType, ProductFryingStage.Raw);

            var refrigeratorSprites = productConfig.InRefrigeratorSprites;
            
            if (refrigeratorSprites == null || refrigeratorSprites.Length == 0)
            {
                Debug.LogError($"No refrigerator sprites found in config of product type: {productType}");
                return;
            }

            for (int i = 0; i < refrigeratorSprites.Length; i++)
            {
                _sprites[i] = refrigeratorSprites[i];
            }
            
            UpdateSlotImage(_refrigeratorSlot.CurrentProductsCount);
        }
        
        private void OnEnable()
        {
            _refrigeratorSlot.ProductsCountChanged += UpdateSlotImage;
        }

        private void OnDisable()
        {
            _refrigeratorSlot.ProductsCountChanged -= UpdateSlotImage;
        }

        private void UpdateSlotImage(int count)
        {
            if (count == 0)
            {
                _productImage.sprite = null;
                _productImage.color = new Color(0, 0, 0, 0);
                return;
            }
            
            count = count > _sprites.Count - 1 ? _sprites.Count - 1 : count;
            
            _productImage.sprite = _sprites[count];
            _productImage.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}