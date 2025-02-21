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

        private void UpdateView(ProductConfig config, int count)
        {
            _counterText.SetText(count.ToString());

            if (config is not null && count > 0)
            {
                _productSpriteRenderer.sprite = config.InContainerSprite;
                return;
            }

            _productSpriteRenderer.sprite = null;
        }
    }
}