using System.Collections.Generic;
using System.Linq;
using Kitchen.Products;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kitchen.Refrigerator
{
    public class RefrigeratorSlotView : MonoBehaviour
    {
        [SerializeField] private RefrigeratorSlot _refrigeratorSlot;
        [SerializeField] private Image _productImage;
        [SerializeField] private TextMeshProUGUI _counterText;
        
        private void OnEnable()
        {
            _refrigeratorSlot.ProductsChanged += UpdateView;
        }

        private void OnDisable()
        {
            _refrigeratorSlot.ProductsChanged -= UpdateView;
        }

        private void UpdateView(IReadOnlyCollection<Product> products)
        {
            var count = products.Count;
            
            _counterText.SetText(count.ToString());
            
            if (count == 0)
            {
                _productImage.sprite = null;
                _productImage.color = new Color(0, 0, 0, 0);
                return;
            }

            var sprites = products.First().InRefrigeratorSprites;
            
            count = count > sprites.Length - 1 ? sprites.Length - 1 : count;
            
            _productImage.sprite = sprites[count];
            _productImage.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}