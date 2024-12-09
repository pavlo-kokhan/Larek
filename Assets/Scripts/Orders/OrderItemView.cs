using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Orders
{
    public class OrderItemView : MonoBehaviour
    {
        [SerializeField] private Toggle _itemToggle;
        [SerializeField] private TMP_Text _itemNameText;
        [SerializeField] private TMP_InputField _quantityInput;
        [SerializeField] private TMP_Text _priceText;
        
        private OrderItem _orderItem;

        public void Initialize(OrderItem orderItem)
        {
            _orderItem = orderItem;

            UpdateView();
            _itemToggle.onValueChanged.AddListener(OnItemToggleChanged);
            _quantityInput.onEndEdit.AddListener(OnQuantityChanged);
        }

        private void UpdateView()
        {
            _itemToggle.isOn = _orderItem.IsSelected;
            _itemNameText.text = _orderItem.Name;
            _priceText.text = $"{_orderItem.Price}";
            _quantityInput.text = $"{_orderItem.Count}";
        }
        
        private void OnItemToggleChanged(bool isSelected)
        {
            _orderItem.IsSelected = isSelected;
            _itemToggle.isOn = isSelected;
        }

        private void OnQuantityChanged(string value)
        {
            if (int.TryParse(value, out int count) && count > 0)
            {
                _orderItem.Count = count;
            }
            else
            {
                _orderItem.Count = 1;
            }
            
            _quantityInput.text = $"{_orderItem.Count}";
        }
    }
}