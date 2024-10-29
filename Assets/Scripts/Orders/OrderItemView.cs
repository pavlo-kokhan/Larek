using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Orders
{
    public class OrderItemView : MonoBehaviour
    {
        [SerializeField] private Toggle itemToggle;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_InputField quantityInput;
        [SerializeField] private TMP_Text priceText;
        
        private OrderItem _orderItem;

        public void Initialize(OrderItem orderItem)
        {
            _orderItem = orderItem;

            UpdateView();
            itemToggle.onValueChanged.AddListener(OnItemToggleChanged);
            quantityInput.onEndEdit.AddListener(OnQuantityChanged);
        }

        private void UpdateView()
        {
            itemToggle.isOn = _orderItem.IsSelected;
            itemNameText.text = _orderItem.Name;
            priceText.text = $"{_orderItem.Price}";
            quantityInput.text = $"{_orderItem.Count}";
        }
        
        private void OnItemToggleChanged(bool isSelected)
        {
            _orderItem.IsSelected = isSelected;
            itemToggle.isOn = isSelected;
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
            
            quantityInput.text = $"{_orderItem.Count}";
        }
    }
}