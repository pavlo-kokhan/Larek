using System.Collections.Generic;
using Kitchen.Products;
using Kitchen.Products.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kitchen.Refrigerator
{
    public class RefrigeratorSlotView : MonoBehaviour
    {
        [SerializeField] private RefrigeratorSlot _refrigeratorSlot;
        [SerializeField] private Image _productImage;
        [SerializeField] private TextMeshProUGUI _counterText;
        
        private readonly Dictionary<int, Sprite> _sprites = new();

        [Inject]
        public void Construct(ProductConfigsStorage productConfigsStorage)
        {
            var productType = _refrigeratorSlot.Type;
            var productConfig = productConfigsStorage.GetConfig(productType, ProductCookingStage.Raw, ProductChoppingStage.Unchopped);

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
            
            UpdateView(_refrigeratorSlot.CurrentProductsCount);
        }
        
        private void OnEnable()
        {
            _refrigeratorSlot.ProductsCountChanged += UpdateView;
        }

        private void OnDisable()
        {
            _refrigeratorSlot.ProductsCountChanged -= UpdateView;
        }

        private void UpdateView(int count)
        {
            _counterText.SetText(count.ToString());
            
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