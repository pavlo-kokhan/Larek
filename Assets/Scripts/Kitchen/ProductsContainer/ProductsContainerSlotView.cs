using System.Collections.Generic;
using System.Linq;
using Kitchen.Products;
using TMPro;
using UnityEngine;

namespace Kitchen.ProductsContainer
{
    public class ProductsContainerSlotView : MonoBehaviour
    {
        [SerializeField] private ProductsContainerSlot _productsContainerSlot;
        [SerializeField] private SpriteRenderer _productSpriteRenderer;
        [SerializeField] private TMP_Text _counterText;

        private void OnEnable()
        {
            _productsContainerSlot.ProductsCountChanged += UpdateView;
        }

        private void OnDisable()
        {
            _productsContainerSlot.ProductsCountChanged -= UpdateView;
        }

        private void Start()
        {
            _productSpriteRenderer.sprite = null;
            _counterText.SetText("0");
        }

        private void UpdateView(IReadOnlyList<Product> products)
        {
            _counterText.SetText(products.Count.ToString());

            if (products.Count == 0)
            {
                _productSpriteRenderer.sprite = null;
                return;
            }
            
            var index = products.Count - 1;
            var countedSprite = products.First().InContainerSprites[index];
            var firstSprite = products.First().InContainerSprites[0];

            if (countedSprite is null)
            {
                _productSpriteRenderer.sprite = firstSprite;
                return;
            }
            
            _productSpriteRenderer.sprite = countedSprite;
        }
    }
}