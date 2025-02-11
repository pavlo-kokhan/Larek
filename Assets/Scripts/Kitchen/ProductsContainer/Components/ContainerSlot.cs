using System;
using Core.Cursors;
using Kitchen.Products;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.ProductsContainer.Components
{
    [RequireComponent(typeof(Collider2D))]
    public class ContainerSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer _productSpriteRenderer;

        [Inject] private CursorView _cursorView;
        [Inject] private ProductHolder _productHolder;
        
        private Product _currentProduct;

        private void Start()
        {
            // find out if there are products saved
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftButtonClicked();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightButtonClicked();
            }
        }
        
        private void OnLeftButtonClicked()
        {
            // Не нарезанные продукты занимают весь контейнер.
            // Нарезанные можно положить в количестве трёх штук.
            // Продукты разных видов можно смешивать создавая уникальные комбинации.
        }

        private void OnRightButtonClicked()
        {
            //
        }
    }
}